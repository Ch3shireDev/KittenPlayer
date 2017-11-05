using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Drawing;

namespace KittenPlayer
{
    public class YoutubeDL
    {

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

            ListViewEx PlaylistView = track.MusicTab.PlaylistView as ListViewEx;
            Rectangle rect = track.Item.SubItems[5].Bounds;
            ProgressBar progressBar = new ProgressBar { Bounds = rect };
            int Index = PlaylistView.Items.IndexOf(track.Item);
            PlaylistView.AddEmbeddedControl(progressBar, 5, Index);
            progressBar.Show();
            progressBar.Focus();

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
                    progressBar.Value = Convert.ToInt32(Percent);
                }
            }

            YoutubeDL.ProcessStart(track, "--get-filename", out Process process2);


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

            progressBar.Hide();
            PlaylistView.RemoveEmbeddedControl(progressBar);

            if (File.Exists(track.ID + ".m4a"))
            {
                String OutputDir = MainWindow.Instance.Options.DefaultDirectory + "\\" + Name;
                if (File.Exists(OutputDir)) File.Delete(OutputDir);
                File.Move(track.ID + ".m4a", OutputDir);
                track.filePath = OutputDir;
                track.OfflineToLocalData();
                track.UpdateItem();
            }
            else
            {
                PlaylistView.Items.Remove(track.Item);
                track.MusicTab.Tracks.Remove(track);
            }


#if !DEBUG
            DownloadManager.Counter--;
#endif
            MainWindow.SavePlaylists();
        }

        String URL;

        public ProgressBar progressBar;

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "youtube-dl.exe",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        Process process = new Process();

        public YoutubeDL(String URL) => this.URL = URL;

        StreamReader Start(String Arguments)
        {
            process.StartInfo = startInfo;
            startInfo.Arguments = URL;
            process.StartInfo.Arguments += " " + Arguments;
            process.Start();
            return process.StandardOutput;
        }

        public List<Track> GetData()
        {
            String output = Start("-j --flat-playlist").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Track> Tracks = new List<Track>();
            foreach (String line in Lines)
            {
                Debug.WriteLine(line);
                JObject jObject;
                try
                {
                    jObject = JObject.Parse(line);
                }
                catch
                {
                    continue;
                }
                Debug.WriteLine("Success!");
                jObject.TryGetValue("title", out JToken title);
                jObject.TryGetValue("url", out JToken URL);
                if (URL == null) continue;
                String Title = "";
                if (title != null) Title = title.ToString();

                if (Title == "[Deleted video]") continue;
                if (Title == "[Private video]") continue;

                if (URL != null)
                {
                    Track track = new Track("", URL.ToString())
                    {
                        Title = Title
                    };
                    Tracks.Add(track);
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
            if (track.ID[0] == '-')
            {
                startInfo.Arguments += "-- ";
            }
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

        String Name;

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



    class DownloadManager
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

        static bool downloadAgain = false;
        static DownloadManager Instance = null;
        List<Track> TracksToDownload;
        public static int ActiveDownloads = 0;

        public static void CallDownloadStarted() { ActiveDownloads++; }
        public static bool RequestDownloadStart()
        {
            return true;
            //return ActiveDownloads < 1;
        }
        public static void DownloadEnded() { ActiveDownloads--; }

        DownloadManager() { }

        static void AddToDownload(List<Track> tracks)
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
                await Task.Delay(200);
            }
            AddToDownload(tracks.GetRange(1, tracks.Count - 1));
        }
        
    }
}
