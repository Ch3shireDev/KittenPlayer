using System;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class YouTubeForm : Form
    {
        public YouTubeForm()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Download_Button_Click(object sender, EventArgs e)
        {
            DownloadTrack(LinkBox.Text);
            this.Close();
        }

        public void DownloadTrack(String URL)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl --extract-audio --audio-format mp3 " + URL;
            //startInfo.Arguments = "/C youtube-dl " + URL;
            process.StartInfo = startInfo;
            process.Start();
        }
        

    }
}
