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

        public double GetAlpha()
        {
            if (!(IsPlaying || IsPaused)) return 0;
            if (!player.NaturalDuration.HasTimeSpan) return 0;
            double ms = player.Position.TotalMilliseconds;
            double total = player.NaturalDuration.TimeSpan.TotalMilliseconds;
            if (ms >= 0 && ms <= total)
                return ms / total;
            else return 0;
        }

        public void SetAlpha(double alpha)
        {
            if (!(IsPlaying || IsPaused || player.NaturalDuration.HasTimeSpan))return;
            player.Position = new TimeSpan((long)Math.Floor(alpha * player.NaturalDuration.TimeSpan.Ticks));
        }

        public void SetVolume(double Volume)
        {
            player.Volume = Volume;
        }

        String GetTime()
        {
            if (!IsPlaying) return "0:00";
            else
            {
                int seconds = player.Position.Seconds % 60;
                int minutes = player.Position.Minutes % 60;
                int hours = player.Position.Hours;

                String str;
                if (hours != 0) {
                    str = String.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);
                }
                else
                {
                    str = String.Format("{0:00}:{1:00}", minutes, seconds);
                }
                return str;
            }
        }

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


        public bool IsPlaying = false;
        

        /// <summary> 
        /// Starts playing new file, automatically stops the old file.
        /// </summary>

        public static bool IsPaused = false;

        public void Play(Track track)
        {
            Play(track.filePath);
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
            if (!IsPaused && CurrentTrack != null)
            {
                String File = CurrentTrack.filePath;
                if (File == "") return;
                player.Open(new System.Uri(File));
            }
            player.Play();
            IsPlaying = true;

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
