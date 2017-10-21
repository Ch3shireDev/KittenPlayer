using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace KittenPlayer
{
    public partial class AddPlaylistForm : Form
    {
        String PlaylistURL = @"PLWuGFckoU4Tw6BgCYL7PWN5B2q5XpuX1V";

        public AddPlaylistForm()
        {
            InitializeComponent();
            textBox1.Text = PlaylistURL;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlaylistURL = textBox1.Text;
            GetPlaylist(PlaylistURL);
            MainWindow.SavePlaylists();
        }
        
        public List<Track> Tracks = new List<Track>();

        void GetPlaylist(String URL)
        {
            YoutubeDL process = new YoutubeDL(URL);
            Tracks.AddRange(process.GetData());
            this.Close();
        }
    }
}
