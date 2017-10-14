using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace KittenPlayer
{

    public partial class MainWindow : Form
    {
        Timer trackbarTimer = new Timer();

        public void InitializeTrackbarTimer()
        {
            trackbarTimer.Tick += new EventHandler(trackbarTimer_Tick);

            trackbarTimer.Interval = 500;
            trackbarTimer.Enabled = true;
            trackbarTimer.Start();


        }

        void trackbarTimer_Tick(object sender, EventArgs e)
        {
            if (IsHolding) return;

            if (musicPlayer.IsPlaying)
            {
                SetTrackbarTime();
            }
        }

        public void SetTrackbarTime()
        {
            int min = trackBar.Minimum;
            int max = trackBar.Maximum;
            double alpha = musicPlayer.GetAlpha();
            trackBar.Value = (int) Math.Floor(min + alpha * (max - min));
        }
        
        private void trackBar_Scroll(object sender, EventArgs e)
        {

        }
        

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        bool IsHolding = false;

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            IsHolding = true;
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            IsHolding = false;
            if (!musicPlayer.IsPlaying) return;

            int min = trackBar.Minimum;
            int max = trackBar.Maximum;
            int val = trackBar.Value;

            double alpha = (double)(val - min) / (max - min);

            musicPlayer.SetAlpha(alpha);
        }
    }
}
