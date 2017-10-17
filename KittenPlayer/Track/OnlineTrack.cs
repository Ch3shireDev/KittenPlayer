using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace KittenPlayer
{
    class OnlineTrack : Track
    {
        public String YoutubeID = "";

        public override void Play()
        {
            Download();
        }

        public OnlineTrack(String ID, String Title = "")
        {
            base.Title = Title;
            base.fileName = Title;
            this.YoutubeID = ID;
        }

        public String GetOnlineFilename()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a --get-filename " + @"https://www.youtube.com/watch?v=" + YoutubeID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();

            var groups = Regex.Match(output, @"(.*)\s*$").Groups;
            return groups[1].Value;
        }

        public Track Download()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a " + @"https://www.youtube.com/watch?v=" + YoutubeID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();
            Debug.WriteLine(output);

            Track track = new Track(GetOnlineFilename(), this.fileName);
            track.Album = Album;
            track.Artist = Artist;
            track.Number = Number;
            track.Title = Title;

            return track;
        }
    }
}
