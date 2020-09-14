using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

namespace KittenPlayer
{
    public class FFmpeg
    {

        static string ffmpegDir = "ffmpeg.exe";

        static void CheckBinary()
        {
            string version = "ffmpeg-3.4-win32-static";
            string dir = Path.GetTempPath() + version + "/bin/ffmpeg.exe";
            if (File.Exists("ffmpeg.exe"))
            {
                ffmpegDir = "ffmpeg.exe";
                return;
            }
            ffmpegDir = dir;
            if (File.Exists(Path.GetTempPath() + version + "/bin/ffmpeg.exe")) return;
            string file = version + ".zip";
            string url = "http://ffmpeg.zeranoe.com/builds/win32/static/" + file;
            var client = new WebClient();
            client.DownloadFile(url, Path.GetTempPath());
            
            ZipFile.ExtractToDirectory(Path.GetTempPath() + file, Path.GetTempPath());
            if (!File.Exists(dir)) return;
            ffmpegDir = dir;
            File.Move(dir, "./ffmpeg.exe");
            if (!File.Exists("ffmpeg.exe")) return;
            ffmpegDir = "ffmpeg.exe";
        }


#if DEBUG

        public static void ConvertToMp3(Track track)
#else
        public static async Task ConvertToMp3(Track track)
#endif
        {
            CheckBinary();
            var filePath = track.filePath;
            if (string.IsNullOrWhiteSpace(filePath)) return;
            if (!File.Exists(filePath)) return;
            if (!string.Equals(Path.GetExtension(filePath), ".m4a", StringComparison.OrdinalIgnoreCase)) return;
            var f = TagLib.File.Create(filePath);
            var TotalDuration = f.Properties.Duration.TotalSeconds;

            var TemporaryOutput = Path.GetTempFileName();
            TemporaryOutput = Path.ChangeExtension(TemporaryOutput, ".mp3");

            var progressBar = YoutubeDL.CreateProgressBar(track);

            var process = new Process();
            var startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = ffmpegDir;
            startInfo.Arguments = "-i \"" + track.filePath + "\"";
            startInfo.Arguments += " -acodec libmp3lame -ab 128k -y ";
            startInfo.Arguments += "\"" + TemporaryOutput + "\"";
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            var reader = process.StandardError;

            while (!process.HasExited)
            {
#if DEBUG
                String str = reader.ReadLine();
#else
                var str = await reader.ReadLineAsync();
#endif
                if (string.IsNullOrWhiteSpace(str)) continue;
                var match = Regex.Match(str, @"time=(\d\d):(\d\d):(\d\d)");
                if (match.Success && match.Groups.Count == 4)
                {
                    var Hours = match.Groups[1].ToString();
                    var Minutes = match.Groups[2].ToString();
                    var Seconds = match.Groups[3].ToString();

                    var Duration = int.Parse(Hours) * 3600 + int.Parse(Minutes) * 60 + int.Parse(Seconds);
                    Debug.WriteLine(Duration + " " + TotalDuration);
                    var Percent = (int)(Duration * 100 / TotalDuration);
                    //YoutubeDL.UpdateProgressBar(track, Percent);
                    Debug.WriteLine("{0} {1} {2}", Hours, Minutes, Seconds);
                }
            }
            YoutubeDL.RemoveProgressBar(track);
            if (File.Exists(TemporaryOutput))
            {
                var finalOutput = Path.GetDirectoryName(track.filePath) + "\\" +
                                  Path.GetFileNameWithoutExtension(track.filePath) + ".mp3";
                if (File.Exists(finalOutput)) File.Delete(finalOutput);
                File.Move(TemporaryOutput, finalOutput);
                if (File.Exists(finalOutput))
                {
                    track.filePath = finalOutput;
                    track.UpdateItem();
                }
            }

            MainWindow.SavePlaylists();
        }
    }
}