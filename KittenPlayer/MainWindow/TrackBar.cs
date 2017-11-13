using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainWindow : Form
    {
        private readonly Timer trackbarTimer = new Timer();
        private bool IsHolding;

        public void InitializeTrackbarTimer()
        {
            trackbarTimer.Tick += trackbarTimer_Tick;

            trackbarTimer.Interval = 500;
            trackbarTimer.Enabled = true;
            trackbarTimer.Start();
        }

        private void trackbarTimer_Tick(object sender, EventArgs e)
        {
            if (IsHolding) return;

            if (musicPlayer.IsPlaying)
                SetTrackbarTime();
        }

        public void SetTrackbarTime()
        {
            var min = trackBar.Minimum;
            var max = trackBar.Maximum;
            var alpha = musicPlayer.Progress;
            if (alpha < 0 || alpha > 1) return;
            if (double.IsNaN(alpha)) return;
            trackBar.Value = (int) Math.Floor(min + alpha * (max - min));
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
        }

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            IsHolding = true;
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            IsHolding = false;
            if (!musicPlayer.IsPlaying) return;

            var min = trackBar.Minimum;
            var max = trackBar.Maximum;
            var val = trackBar.Value;

            var valMouse = e.X / 2;
            //trackBar.

            Debug.WriteLine("Values: " + val + " " + valMouse);

            var alpha = (double) (val - min) / (max - min);

            musicPlayer.Progress = alpha;
        }
    }
}