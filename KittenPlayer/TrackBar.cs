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

        //bool IsUpdating = false;

        //public async void UpdateTrackbar()
        //{
        //    if (IsUpdating) return;

        //}

    }
}
