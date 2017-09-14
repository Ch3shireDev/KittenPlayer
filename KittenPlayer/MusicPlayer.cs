using System;
//using WMPLib;
//using AxWMPLib;
using System.Windows.Media;
using System.Windows.Forms;
using System.Diagnostics;


namespace KittenPlayer
{
    public partial class MusicPlayer
    {
        private static MusicPlayer Instance = null;
        System.Windows.Media.MediaPlayer player = new System.Windows.Media.MediaPlayer();

        private MusicPlayer()
        {
            player.MediaEnded += LoadNextTrack;
        }

        public int CurrentTrack = -1;
        public MusicTab CurrentTab = null;

        void LoadNextTrack(object sender, EventArgs e)
        {
            //Debug.WriteLine(CurrentTrack);
            //if (CurrentTrack == -1 || CurrentTab == null) return;
            //if (CurrentTrack + 1 < CurrentTab.Tracks.Count)
            //{
            //    CurrentTrack++;
            //    Play(CurrentTab.Tracks[CurrentTrack].filePath);
            //}
        }

        /// <summary> 
        /// Static method for instancing a new MusicPlayer object.
        /// </summary> 

        public static MusicPlayer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MusicPlayer();
            }
            return Instance;
        }


        public static bool IsPlaying = false;
        

        /// <summary> 
        /// Starts playing new file, automatically stops the old file.
        /// </summary>

        public static bool IsPaused = false;

        public void Play(String File)
        {
            if (!IsPaused)
            {
                Stop();
                player.Open(new System.Uri(File));
                IsPlaying = true;
            }
            Play();
        }

        public void Play()
        {
            if (!IsPaused)
            {
                MainWindow Window = Application.OpenForms[0] as MainWindow;
                String File = Window.GetSelectedTrackPath();
                if (File == "") return;
                player.Open(new System.Uri(File));
            }
            player.Play();

        }
        

        /// <summary> 
        /// Pauses the file.
        /// </summary> 

        public void Pause()
        {
            if (!IsPaused)
            {
                player.Pause();
                IsPaused = true;
            }
            else
            {
                player.Play();
                IsPaused = false;
            }
        }
        
        /// <summary> 
        /// Stops playing the file.
        /// </summary> 

        public void Stop()
        {
            player.Stop();
        }
    }
}
