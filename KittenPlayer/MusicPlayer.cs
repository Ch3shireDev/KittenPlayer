using System;
using System.IO;
using NAudio.Wave;
using System.Windows.Forms;
using System.Diagnostics;


//MediaFoundationReader reader = new MediaFoundationReader(fileName);
//WaveOut player = new WaveOut();
//player.Init(reader);
//player.Play();

namespace KittenPlayer
{
    abstract class Player
    {
        public abstract void Load(String fileName);
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();

        public abstract double Volume { get; set; }
        public abstract double Progress { get; set; }
        public abstract int TotalMilliseconds { get; }
        public abstract bool IsPlaying { get; }
        public abstract bool IsPaused { get; }
    }

    class WMPlayer : Player
    {
        public System.Windows.Media.MediaPlayer player = new System.Windows.Media.MediaPlayer();

        public override void Load(String fileName)
        {

        }

        public override void Play()
        {

        }

        public override void Pause()
        {

        }

        public override void Stop()
        {

        }

        public override double Volume
        {
            get; set;
        }

        public override double Progress
        {
            get; set;
        }

        public override int TotalMilliseconds => throw new NotImplementedException();
        public override bool IsPaused => throw new NotImplementedException();
        public override bool IsPlaying => throw new NotImplementedException();
    }

    public partial class MusicPlayer
    {
        private static MusicPlayer Instance = null;
        WMPlayer player = new WMPlayer();

        private MusicPlayer()
        {
            player.player.MediaEnded += LoadNextTrack;
        }

        public Track CurrentTrack = null;
        public MusicTab CurrentTab = null;

        public double GetProgress()
        {
            if (!(IsPlaying || IsPaused)) return 0;
            if (!player.player.NaturalDuration.HasTimeSpan) return 0;
            double ms = player.player.Position.TotalMilliseconds;
            double total = player.player.NaturalDuration.TimeSpan.TotalMilliseconds;
            if (ms >= 0 && ms <= total)
                return ms / total;
            else return 0;

            return player.Progress;
        }

        public void SetProgress(double alpha)
        {
            if (!(IsPlaying || IsPaused || player.player.NaturalDuration.HasTimeSpan)) return;
            player.player.Position = new TimeSpan((long)Math.Floor(alpha * player.player.NaturalDuration.TimeSpan.Ticks));
            player.Progress = alpha;
        }

        public void SetVolume(double Volume)
        {
            player.player.Volume = Volume;
            player.Volume = Volume;
        }

        String GetTime()
        {
            if (!IsPlaying) return "0:00";
            else
            {
                int seconds = player.player.Position.Seconds % 60;
                int minutes = player.player.Position.Minutes % 60;
                int hours = player.player.Position.Hours;

                String str;
                if (hours != 0)
                {
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
                player.player.Open(new System.Uri(File));
                IsPlaying = true;
            }
            Play();

            player.Load(File);
            player.Play();
        }

        public void Play()
        {
            if (!IsPaused && CurrentTrack != null)
            {
                String File = CurrentTrack.filePath;
                if (File == "") return;
                player.player.Open(new System.Uri(File));
            }
            player.player.Play();
            IsPlaying = true;

            player.Play();
        }

        /// <summary> 
        /// Pauses the file.
        /// </summary> 

        public void Pause()
        {
            if (!IsPaused)
            {
                player.player.Pause();
                IsPaused = true;
            }
            else
            {
                player.player.Play();
                IsPaused = false;
            }

            player.Pause();
        }

        /// <summary> 
        /// Stops playing the file.
        /// </summary> 

        public void Stop()
        {
            player.player.Stop();
            player.Stop();
        }
    }
}
