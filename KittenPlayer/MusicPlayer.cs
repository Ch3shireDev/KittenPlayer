using System;
using System.IO;
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

        public Track CurrentTrack = null;
        public MusicTab CurrentTab = null;

        void LoadNextTrack(object sender, EventArgs e)
        {
            bool exists = false;
            if (CurrentTrack == null) return;
            if (CurrentTab == null) return;
            Track nextTrack = CurrentTrack;
            while (!exists)
            {
                if (nextTrack == null) return;
                nextTrack = CurrentTab.GetNextTrack(nextTrack);
                exists = File.Exists(nextTrack.filePath);
            }
            CurrentTrack = nextTrack;
            CurrentTab.SelectTrack(CurrentTrack);
            Play();
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

        public void Play(Track track)
        {

        }

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
                String File = CurrentTrack.filePath;
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
