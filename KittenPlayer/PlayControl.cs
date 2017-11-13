using KittenPlayer.Properties;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class PlayControl : UserControl
    {
        public enum ERepeatType
        {
            RepeatNone,
            RepeatOne,
            RepeatAll
        }

        public static ERepeatType RepeatType = ERepeatType.RepeatNone;
        private readonly MusicPlayer musicPlayer = MusicPlayer.Instance;

        public PlayControl()
        {
            InitializeComponent();
            RefreshRepeatButton();
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

        private void RepeatButton_ChangeUICues(object sender, UICuesEventArgs e)
        {
        }

        private void RefreshRepeatButton()
        {
            var list = new[] { Resources.RepeatNone, Resources.RepeatOne, Resources.RepeatAll };

            RepeatButton.BackgroundImage = list[(int)RepeatType];
        }

        private void RepeatButton_Click(object sender, EventArgs e)
        {
            RepeatType++;
            RepeatType = (ERepeatType)((int)RepeatType % 3);
            RefreshRepeatButton();
            Debug.WriteLine(RepeatType + " " + (int)RepeatType);
        }
    }
}