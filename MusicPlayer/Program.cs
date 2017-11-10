using System.Threading;
using WMPLib;

namespace MusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var Player = new WindowsMediaPlayer()
            {
                URL = "a.m4a"
            };
            Player.controls.play();
            while (true) Thread.Sleep(200);
        }
    }
}
