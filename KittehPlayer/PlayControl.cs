using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittehPlayer
{
    public partial class PlayControl : UserControl
    {
        MusicPlayer musicPlayer = MusicPlayer.NewMusicPlayer();

        public PlayControl()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {

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

        }

        private void NextButton_Click(object sender, EventArgs e)
        {

        }
    }
}
