using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class AddPlaylistForm : Form
    {
        private string PlaylistURL = @"PLWuGFckoU4Tw6BgCYL7PWN5B2q5XpuX1V";

        public List<Track> Tracks = new List<Track>();

        public AddPlaylistForm()
        {
            InitializeComponent();
            textBox1.Text = PlaylistURL;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PlaylistURL = textBox1.Text;
            GetPlaylist(PlaylistURL);
            MainWindow.SavePlaylists();
        }

        private void GetPlaylist(string URL)
        {
            var process = new YoutubeDL(URL);
            Tracks.AddRange(process.GetData());
            Close();
        }
    }
}