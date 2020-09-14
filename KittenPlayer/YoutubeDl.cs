using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace KittenPlayer
{
    public class YoutubeDL
    {
        private static string ydlDirectory = "youtube-dl.exe";

        private readonly Process process = new Process();

        private readonly ProcessStartInfo startInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        private readonly string URL;

        public ProgressBar progressBar;

        public YoutubeDL(string URL)
        {
            this.URL = URL;
        }

        private static void CheckBinary()
        {
            if (File.Exists(ydlDirectory)) return;
            ydlDirectory = Path.GetTempPath() + "youtube-dl.exe";
            if (File.Exists(ydlDirectory)) return;
            while (!File.Exists(ydlDirectory))
            {
                var client = new WebClient();
                client.DownloadFile(@"https://yt-dl.org/latest/youtube-dl.exe", ydlDirectory);
            }

            MainWindow.Instance.ShowMesssage(
                "youtube-dl.exe was missing from your Kitten Player installation folder. It could be either because of installation error or your firewall being too officious at it's job. Consider whitelisting youtube-dl.exe or temporary turning your firewall off.");
        }


        private static string TemporaryPath(Track track)
        {
            return Path.GetTempPath() + track.ID + ".m4a";
        }

#if DEBUG

        public static string GetOnlineTitle(Track track)
#else
        public static async Task<string> GetOnlineTitle(Track track)
#endif
        {
            CheckBinary();

            var process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = ydlDirectory,
                Arguments = "--get-title "
            };
            if (track.ID[0] == '-') startInfo.Arguments += "-- ";
            startInfo.Arguments += track.ID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            var reader = process.StandardOutput;
#if DEBUG
            var output = reader.ReadToEnd();
#else
            var output = await reader.ReadToEndAsync();
#endif
            output = output.Split('\n')[0];
            if (output == null) return "";
            var match = Regex.Match(output, @"(.*)\s*$");
            return match.Success ? match.Groups[1].Value : "";
        }

        public static ProgressBar CreateProgressBar(Track track)
        {
            var playlistView = track.MusicTab.PlaylistView;
            var rect = track.Item.SubItems[5].Bounds;
            var progressBar = new ProgressBar {Bounds = rect};
            track.progressBar = progressBar;
            var index = playlistView.Items.IndexOf(track.Item);
            playlistView.AddEmbeddedControl(progressBar, 5, index);
            progressBar.Show();
            progressBar.Focus();
            return progressBar;
        }

        public static void RemoveProgressBar(Track track)
        {
            track.progressBar.Value = 100;
            track.progressBar.Hide();
            track.MusicTab.PlaylistView.RemoveEmbeddedControl(track.progressBar);
        }

        private static void UpdateProgressBar(Track track, int percent)
        {
            if (track.progressBar == null) return;
            track.progressBar.Value = percent;
        }

#if DEBUG

        public static void DownloadTrack(Track track)
#else
        public static async Task DownloadTrack(Track track)
#endif
        {
            if (track == null) return;
            if (!track.IsOnline) return;

#if !DEBUG
            DownloadManager.Counter++;
#endif

            CreateProgressBar(track);

            if (File.Exists(TemporaryPath(track))) File.Delete(TemporaryPath(track));
            ProcessStart(track, "-o " + TemporaryPath(track), out var process);
            var reader = process.StandardOutput;
            while (!process.HasExited)
            {
#if DEBUG
                var output = reader.ReadLine();
#else
                var output = await reader.ReadLineAsync();
#endif
                if (string.IsNullOrWhiteSpace(output)) continue;
                Debug.WriteLine(output);
                var r = new Regex(@"\[download]\s*([0-9.]*)%", RegexOptions.IgnoreCase);
                var m = r.Match(output);
                if (m.Success)
                {
                    var g = m.Groups[1].ToString();
                    var flag = double.TryParse(g, out var result);
                    if (flag) UpdateProgressBar(track, Convert.ToInt32(result));
                }
            }

            ProcessStart(track, "--get-filename", out var process2);

            string name;
            reader = process2.StandardOutput;
            {
#if DEBUG
                var output = reader.ReadToEnd();
#else
                var output = await reader.ReadToEndAsync();
#endif
                var str = output.Split('\n');
                name = str[0];
            }

            RemoveProgressBar(track);

            if (File.Exists(TemporaryPath(track)))
            {
                track.filePath = TemporaryPath(track);
                var outputDir = MainWindow.Instance.Options.DefaultDirectory + "\\" + name;
                if (File.Exists(outputDir))
                    try
                    {
                        File.Delete(outputDir);
                    }
                    catch
                    {
                        // ignored
                    }

                File.Move(TemporaryPath(track), outputDir);
                if (File.Exists(outputDir))
                    track.filePath = outputDir;
                track.OfflineToLocalData();
                track.UpdateItem();
            }
            else
            {
                track.MusicTab.PlaylistView.Items.Remove(track.Item);
                track.MusicTab.Tracks.Remove(track);
            }

#if !DEBUG
            DownloadManager.Counter--;
#endif
            MainWindow.SavePlaylists();
        }

        private StreamReader Start(string Arguments)
        {
            CheckBinary();

            process.StartInfo = startInfo;
            process.StartInfo.FileName = ydlDirectory;
            startInfo.Arguments = URL;
            process.StartInfo.Arguments += " " + Arguments;
            process.Start();
            return process.StandardOutput;
        }

        public List<Track> GetData()
        {
            var output = Start("-j --flat-playlist").ReadToEnd();
            var Lines = output.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

            var Tracks = new List<Track>();
            foreach (var line in Lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var Deserializer = new JavaScriptSerializer();
                    var Data = Deserializer.Deserialize<TrackData>(line);
                    if (Data == null) continue;
                    if (Data.title == null || Data.url == null) continue;
                    var Title = Data.title;
                    if (Title == "[Deleted video]") continue;
                    if (Title == "[Private video]") continue;

                    var track = new Track("", Data.url)
                    {
                        Title = Title
                    };
                    Tracks.Add(track);
                }
                catch
                {
                }
            }

            return Tracks;
        }

        private static void ProcessStart(Track track, string arg, out Process process)
        {
            CheckBinary();

            process = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = ydlDirectory,
                Arguments = "-f m4a " + arg + " "
            };
            if (track.ID[0] == '-') startInfo.Arguments += "-- ";
            startInfo.Arguments += track.ID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private class TrackData
        {
            public string title;
            public string url;
        }
    }
}