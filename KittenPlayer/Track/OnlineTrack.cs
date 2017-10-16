using System;
using System.IO;
using System.Diagnostics;

namespace KittenPlayer
{
    class OnlineTrack : Track
    {
        public String YoutubeID = "";

        public override void Play()
        {
            DownloadTrack();
        }

        public OnlineTrack(String ID, String Title = "")
        {
            base.Title = Title;
            base.fileName = Title;
            this.YoutubeID = ID;
        }

        public void DownloadTrack()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f bestaudio " + @"https://www.youtube.com/watch?v=" + YoutubeID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();
            Debug.WriteLine(output);
            //string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        }
    }
}
