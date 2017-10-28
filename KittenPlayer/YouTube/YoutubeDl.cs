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

namespace KittenPlayer
{
    class TrackObject
    {
        public String ID;
        public String Title;
    }

    class YoutubeDL
    {
        String URL;

        public ProgressBar progressBar;

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        Process process = new Process();

        public YoutubeDL(String URL) => this.URL = URL;

        StreamReader Start(String Arguments)
        {
            process.StartInfo = startInfo;
            startInfo.Arguments = "/C youtube-dl " + URL;
            process.StartInfo.Arguments += " "+ Arguments;
            process.Start();
            return process.StandardOutput; 
        }

        public async Task<String> Download(String args)
        {

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a " + args + " " + URL;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            
            while (!process.HasExited)
            {
                var task = reader.ReadLineAsync();
                String output = await task;
                if (String.IsNullOrWhiteSpace(output)) continue;
                
                Regex r = new Regex(@"\[download]\s*([0-9.]*)%", RegexOptions.IgnoreCase);
                Match m = r.Match(output);
                if (m.Success)
                {
                    Group g = m.Groups[1];
                    double Percent = double.Parse(g.ToString());
                    progressBar.Value = Convert.ToInt32(Percent);
                }
            }
            
            startInfo.Arguments = "/C youtube-dl --get-title " + URL;
            process.Start();
            reader = process.StandardOutput;
            String title = await reader.ReadLineAsync();

            return title;
        }

        public List<Track> GetData()
        {
            String output = Start("-j --flat-playlist").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Track> Tracks = new List<Track>();
            foreach (String line in Lines)
            {
                JObject jObject = JObject.Parse(line);
                jObject.TryGetValue("title", out JToken title);
                jObject.TryGetValue("url", out JToken URL);
                if (title != null && URL != null)
                {
                    if (title.ToString() == "[Deleted video]") continue;
                    if (title.ToString() == "[Private video]") continue;
                    Track track = new Track("", URL.ToString());
                    track.Title = title.ToString();
                    Tracks.Add(track);
                }
            }
            return Tracks;
        }
        
        public List<TrackObject> Search(String str)
        {
            String output = Start("-j --flat-playlist").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<TrackObject> Out = new List<TrackObject>();

            foreach (String line in Lines)
            {
                JObject jObject = JObject.Parse(line);
                jObject.TryGetValue("id", out JToken URL);
                if (URL != null)
                {
                    String ID = URL.ToString();
                    YoutubeDL yt = new YoutubeDL(ID);
                    String Title = yt.GetTitle();
                    TrackObject track = new TrackObject() { ID = ID, Title = Title };
                    Out.Add(track);
                }
            }
            return Out;
        }

        public String GetTitle()
        {
            String output = Start("-e").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (Lines.Length == 0) return "";
            else return Lines[0];
        }
    }

    class SearchResult
    {
        static String Download(String name = "Dead Can Dance")
        {
            HttpWebRequest request = WebRequest.Create(@"https://www.youtube.com/results?search_query=" + name) as HttpWebRequest;
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            String stream = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            return stream;
        }
        
        public SearchResult(String Name)
        {
            String data = Download(Name);
            string[] lines = Regex.Split(data, @"\n");
            foreach (string str in lines)
            {
                Result track = new Result(str);
                if (track.Type != EType.None) Tracks.Add(track);
            }
        }

        public List<Result> Tracks = new List<Result>();
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
}
