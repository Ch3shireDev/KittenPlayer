using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Mat

namespace KittenPlayer
{

    public partial class MainWindow : Form
    {
        public void SetTrackbarTime()
        {
            int min = trackBar1.Minimum;
            int max = trackBar1.Maximum;
            float alpha = musicPlayer.GetAlpha();
            trackBar1.Value = (int) Math.Floor(min + alpha * (max - min));
        }

        //bool IsUpdating = false;

        //public async void UpdateTrackbar()
        //{
        //    if (IsUpdating) return;

        //}

    }
}
