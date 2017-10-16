using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace KittenPlayer
{
    public partial class YouTubePlaylistForm : Form
    {
        String PlaylistURL = @"https://www.youtube.com/playlist?list=PLMhEO-BZNPqSKOPI0hV6WR_3j1BvmyoUD";

        public YouTubePlaylistForm()
        {
            InitializeComponent();
            textBox1.Text = PlaylistURL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetPlaylist(PlaylistURL);
        }
        
        public List<Track> Tracks = new List<Track>();

        void GetPlaylist(String URL)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -j --flat-playlist " + URL;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach(String line in Lines)
            {
                JObject jObject = JObject.Parse(line);
                JToken titleValue, urlValue;
                jObject.TryGetValue("title", out titleValue);
                jObject.TryGetValue("url", out urlValue);
                OnlineTrack track = new OnlineTrack(urlValue.ToString(), titleValue.ToString());
                Tracks.Add(track);
            }

            this.Close();
        }
    }
}
