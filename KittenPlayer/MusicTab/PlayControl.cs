using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class PlayControl : UserControl
    {
        MusicPlayer musicPlayer = MusicPlayer.Instance;

        public PlayControl()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            musicPlayer.Play();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            musicPlayer.Pause();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            musicPlayer.Stop();
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            musicPlayer.Previous();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            musicPlayer.Next();
        }
    }
}
