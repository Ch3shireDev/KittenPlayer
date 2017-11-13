using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainWindow : Form
    {
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private void volumeBar_ValueChanged(object sender, EventArgs e)
        {
            musicPlayer.Volume = volumeBar.Value * 1.0 / volumeBar.Maximum;
        }
    }
}