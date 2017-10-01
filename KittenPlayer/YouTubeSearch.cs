using System;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class YouTubeSearch : Form
    {
        public YouTubeSearch()
        {
            InitializeComponent();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                    GetSearchResults(textBox1.Text);
            }
        }

        public void GetSearchResults(String InText)
        {
            OnlineTracks tracks = new OnlineTracks(InText);
            listBox1.Items.Clear();

            foreach(var track in tracks.Tracks)
            {
                listBox1.Items.Add(track.title);
            }
            listBox1.AutoSize = true;
        }
    }
}
