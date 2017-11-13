using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace KittenPlayer
{
    public class FFmpeg
    {
#if DEBUG
        public static void ConvertToMp3(Track track)
#else

        public static async Task ConvertToMp3(Track track)
#endif
        {
            if (String.IsNullOrWhiteSpace(track.filePath)) return;
            if (!File.Exists(track.filePath)) return;
            if (!String.Equals(Path.GetExtension(track.filePath), ".m4a", StringComparison.OrdinalIgnoreCase)) return;
            TagLib.File f = TagLib.File.Create(track.filePath);
            double TotalDuration = f.Properties.Duration.TotalSeconds;

            String TemporaryOutput = Path.GetTempFileName();
            TemporaryOutput = Path.ChangeExtension(TemporaryOutput, ".mp3");

            ProgressBar progressBar = YoutubeDL.CreateProgressBar(track);

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "ffmpeg.exe";
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

            StreamReader reader = process.StandardError;

            while (!process.HasExited)
            {
#if DEBUG
                String str = reader.ReadLine();
#else
                String str = await reader.ReadLineAsync();
#endif
                if (String.IsNullOrWhiteSpace(str)) continue;
                Match match = Regex.Match(str, @"time=(\d\d):(\d\d):(\d\d)");
                if (match.Success && match.Groups.Count == 4)
                {
                    String Hours = match.Groups[1].ToString();
                    String Minutes = match.Groups[2].ToString();
                    String Seconds = match.Groups[3].ToString();

                    int Duration = int.Parse(Hours) * 3600 + int.Parse(Minutes) * 60 + int.Parse(Seconds);
                    Debug.WriteLine(Duration + " " + TotalDuration);
                    int Percent = (int)(Duration * 100 / TotalDuration);
                    YoutubeDL.UpdateProgressBar(track, Percent);
                    Debug.WriteLine("{0} {1} {2}", Hours, Minutes, Seconds);
                }
            }
            YoutubeDL.RemoveProgressBar(track);
            if (File.Exists(TemporaryOutput))
            {
                String FinalOutput = Path.GetDirectoryName(track.filePath) + "\\" + Path.GetFileNameWithoutExtension(track.filePath) + ".mp3";
                if (File.Exists(FinalOutput)) File.Delete(FinalOutput);
                File.Move(TemporaryOutput, FinalOutput);
                if (File.Exists(FinalOutput))
                {
                    track.filePath = FinalOutput;
                    track.UpdateItem();
                }
            }

            MainWindow.SavePlaylists();
        }
    }

    public class YoutubeDL
    {
#if DEBUG
        public static String GetOnlineTitle(Track track)
#else

        public static async Task<String> GetOnlineTitle(Track track)
#endif
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "youtube-dl.exe";
            startInfo.Arguments = "--get-title ";
            if (track.ID[0] == '-') startInfo.Arguments += "-- ";
            startInfo.Arguments += track.ID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
#if DEBUG
            String output = reader.ReadToEnd();
#else
            String output = await reader.ReadToEndAsync();
#endif
            output = output.Split('\n')[0];
            if (output != null)
            {
                var match = Regex.Match(output, @"(.*)\s*$");
                if (match.Success) return match.Groups[1].Value;
            }
            return "";
        }

        public static ProgressBar CreateProgressBar(Track track)
        {
            ListViewEx PlaylistView = track.MusicTab.PlaylistView as ListViewEx;
            Rectangle rect = track.Item.SubItems[5].Bounds;
            ProgressBar progressBar = new ProgressBar { Bounds = rect };
            track.progressBar = progressBar;
            int Index = PlaylistView.Items.IndexOf(track.Item);
            PlaylistView.AddEmbeddedControl(progressBar, 5, Index);
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

        public static void UpdateProgressBar(Track track, int Percent)
        {
            if (track.progressBar == null) return;
            track.progressBar.Value = Percent;
        }

#if DEBUG
        public static void DownloadTrack(Track track)
#else

        public static async Task DownloadTrack(Track track)
#endif
        {
            if (!track.IsOnline) return;

#if !DEBUG
            DownloadManager.Counter++;
#endif

            ProgressBar progressBar = CreateProgressBar(track);

            if (File.Exists(track.ID + ".m4a")) File.Delete(track.ID + ".m4a");
            ProcessStart(track, "-o " + track.ID + ".m4a", out Process process);
            StreamReader reader = process.StandardOutput;
            while (!process.HasExited)
            {
#if DEBUG
                String output = reader.ReadLine();
#else
                String output = await reader.ReadLineAsync();
#endif
                if (String.IsNullOrWhiteSpace(output)) continue;
                Debug.WriteLine(output);
                Regex r = new Regex(@"\[download]\s*([0-9.]*)%", RegexOptions.IgnoreCase);
                Match m = r.Match(output);
                if (m.Success)
                {
                    Group g = m.Groups[1];
                    double Percent = double.Parse(g.ToString());
                    UpdateProgressBar(track, Convert.ToInt32(Percent));
                }
            }

            ProcessStart(track, "--get-filename", out Process process2);

            String Name;
            reader = process2.StandardOutput;
            {
#if DEBUG
                String output = reader.ReadToEnd();
#else
                String output = await reader.ReadToEndAsync();
#endif
                string[] str = output.Split('\n');
                Debug.WriteLine(str[0]);
                Name = str[0];
            }

            RemoveProgressBar(track);

            if (File.Exists(track.ID + ".m4a"))
            {
                track.filePath = track.ID + ".m4a";
                String OutputDir = MainWindow.Instance.Options.DefaultDirectory + "\\" + Name;
                if (File.Exists(OutputDir))
                {
                    try
                    {
                        File.Delete(OutputDir);
                    }
                    catch { }
                }
                File.Move(track.ID + ".m4a", OutputDir);
                if (File.Exists(OutputDir))
                {
                    track.filePath = OutputDir;
                }
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

        private String URL;

        public ProgressBar progressBar;

        private ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "youtube-dl.exe",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        private Process process = new Process();

        public YoutubeDL(String URL) => this.URL = URL;

        private StreamReader Start(String Arguments)
        {
            process.StartInfo = startInfo;
            startInfo.Arguments = URL;
            process.StartInfo.Arguments += " " + Arguments;
            process.Start();
            return process.StandardOutput;
        }

        private class TrackData
        {
            public String url;
            public String title;
        }

        public List<Track> GetData()
        {
            String output = Start("-j --flat-playlist").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Track> Tracks = new List<Track>();
            foreach (String line in Lines)
            {
                if (String.IsNullOrWhiteSpace(line)) continue;
                try
                {
                    var Deserializer = new JavaScriptSerializer();
                    TrackData Data = Deserializer.Deserialize<TrackData>(line);
                    if (Data == null) continue;
                    if (Data.title == null || Data.url == null) continue;
                    String Title = Data.title;
                    if (Title == "[Deleted video]") continue;
                    if (Title == "[Private video]") continue;

                    Track track = new Track("", Data.url)
                    {
                        Title = Title
                    };
                    Tracks.Add(track);
                }
                catch
                {
                    continue;
                }
            }
            return Tracks;
        }

        internal static void ProcessStart(Track track, string arg, out Process process)
        {
            process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "youtube-dl.exe";
            startInfo.Arguments = "-f m4a " + arg + " ";
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
    }

    public class SearchResult
    {
        public static async Task<String> Download(String name)
        {
            HttpWebRequest request = WebRequest.Create(@"https://www.youtube.com/results?search_query=" + name) as HttpWebRequest;
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            String stream = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            return stream;
        }

        private String Name;

        public SearchResult(String Name)
        {
            this.Name = Name;
        }

        public async Task<List<Result>> GetResults()
        {
            String data = await Download(Name);
            string[] lines = Regex.Split(data, @"\n");
            List<Result> Tracks = new List<Result>();
            foreach (string str in lines)
            {
                Result track = new Result(str);
                if (track.Type != EType.None) Tracks.Add(track);
            }
            return Tracks;
        }
    }

    public enum EType
    {
        None, Track, Playlist
    };

    public class Result
    {
        public String ID;
        public String Title;
        public String Playlist;
        public EType Type = EType.None;

        public static bool IsMatch(String str)
        {
            return Regex.IsMatch(str, "yt-lockup-content");
        }

        public Result(String str)
        {
            if (!IsMatch(str)) return;
            Match mWatch = Regex.Match(str, "watch\\?v=([^\"&]*)");
            if (mWatch.Success)
            {
                ID = mWatch.Groups[1].ToString();
                Match mTitle = Regex.Match(str, "title=\"([^\"]*)");
                if (mTitle.Success) Title = mTitle.Groups[1].ToString();
                Match mPlaylist = Regex.Match(str, "list=([^\"]*)");
                if (mPlaylist.Success)
                {
                    Playlist = mPlaylist.Groups[1].ToString();
                    Type = EType.Playlist;
                }
                else Type = EType.Track;
            }
            else Type = EType.None;
        }
    }

    internal class DownloadManager
    {
        public static void JustDownload(List<Track> tracks)
        {
            downloadAgain = false;
            AddToDownload(tracks);
        }

        public static void DownloadAgain(List<Track> tracks)
        {
            downloadAgain = true;
            AddToDownload(tracks);
        }

        private static bool downloadAgain = false;
        private static DownloadManager Instance = null;
        private List<Track> TracksToDownload;
        public static int ActiveDownloads = 0;

        public static void CallDownloadStarted()
        {
            ActiveDownloads++;
        }

        public static bool RequestDownloadStart()
        {
            return true;
            //return ActiveDownloads < 1;
        }

        public static void DownloadEnded()
        {
            ActiveDownloads--;
        }

        private DownloadManager()
        { }

        private static void AddToDownload(List<Track> tracks)
        {
            if (Instance == null)
                Instance = new DownloadManager();
            if (Instance.TracksToDownload == null)
                Instance.TracksToDownload = new List<Track>();
            Instance.TracksToDownload.AddRange(tracks);
            Instance.Download();
        }

        public static int Counter = 0;

#if DEBUG
        private void Download()
#else

        private async Task Download()
#endif
        {
            while (TracksToDownload.Count > 0)
            {
                Track track = TracksToDownload[0];
                if (downloadAgain) { track.filePath = ""; track.UpdateItem(); }
#if DEBUG
                YoutubeDL.DownloadTrack(track);
#else
                if (Counter < 3)
                {
                    YoutubeDL.DownloadTrack(track);
                }
                else
                {
                    await YoutubeDL.DownloadTrack(track);
                }
#endif
                TracksToDownload.Remove(track);
            }
        }

        internal static async void PlayAfterDownload(List<Track> tracks)
        {
            if (tracks.Count == 0) return;
            Track track = tracks[0];
#if DEBUG
            YoutubeDL.DownloadTrack(track);
#else
            await YoutubeDL.DownloadTrack(track);
#endif
            track.MusicTab.Play(track);
            while (!MusicPlayer.Instance.IsPlaying)
            {
                //await Task.Delay(200);
                Thread.Sleep(200);
            }
            AddToDownload(tracks.GetRange(1, tracks.Count - 1));
        }
    }
}