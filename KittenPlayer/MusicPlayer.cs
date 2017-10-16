using System;
using System.IO;
using NAudio.Wave;
using System.Diagnostics;

namespace KittenPlayer
{
    abstract class Player
    {
        public Track CurrentTrack = null;

        public abstract void Load(String fileName);
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract void Resume();

        public abstract double Volume { get; set; }
        public abstract double Progress { get; set; }
        public abstract int TotalMilliseconds { get; }
        public abstract bool IsPlaying { get; set; }
        public abstract bool IsPaused { get; set; }
    }

    class MFPlayer : Player
    {
        WaveOut player = new WaveOut();

        public override void Load(string fileName)
        {
            MediaFoundationReader reader = new MediaFoundationReader(fileName);
            totalMilliseconds = (int)reader.TotalTime.TotalMilliseconds;
            player.Init(reader);
        }

        public override void Pause() => player.Pause();
        public override void Play() => player.Play();
        public override void Stop() => player.Stop();

        public override void Resume()
        {
            if (IsPaused)
            {
                Play();
                IsPaused = false;
            }
        }

        public override double Volume { get => player.Volume; set => player.Volume = (float)value; }
        public override double Progress { get => 0; set => throw new NotImplementedException(); }

        int totalMilliseconds;
        public override int TotalMilliseconds => totalMilliseconds;

        bool isPlaying;
        public override bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        bool isPaused;
        public override bool IsPaused { get => isPaused; set => isPaused = value; }


    }

    class WMPlayer : Player
    {
        public System.Windows.Media.MediaPlayer player = new System.Windows.Media.MediaPlayer();

        public WMPlayer() => player.MediaEnded += LoadNextTrack;

        void LoadNextTrack(object sender, EventArgs e)
        {
            bool exists = false;
            if (CurrentTrack == null) return;
            //if (CurrentTab == null) return;
            Track nextTrack = CurrentTrack;
            while (!exists)
            {
                if (nextTrack == null) return;
                //nextTrack = CurrentTab.GetNextTrack(nextTrack);
                exists = File.Exists(nextTrack.filePath);
            }
            CurrentTrack = nextTrack;
            //CurrentTab.SelectTrack(CurrentTrack);
            //Play();
        }

        public override void Load(String fileName) => player.Open(new Uri(fileName));

        public override void Play()
        {
            player.Play();
            IsPlaying = true;
        }

        public override void Pause()
        {
            player.Pause();
            IsPaused = true;
        }

        public override void Stop() => player.Stop();

        public override void Resume()
        {
            if (IsPaused)
            {
                IsPaused = false;
                player.Play();
            }
        }

        public override double Volume
        {
            get; set;
        }

        public override double Progress
        {
            get {
                if (!(IsPlaying || IsPaused)) return 0;
                if (!player.NaturalDuration.HasTimeSpan) return 0;
                double ms = player.Position.TotalMilliseconds;
                double total = player.NaturalDuration.TimeSpan.TotalMilliseconds;
                if (ms >= 0 && ms <= total)
                    return ms / total;
                else return 0;
            }
            set
            {
                if (!(IsPlaying || IsPaused || player.NaturalDuration.HasTimeSpan)) return;
                player.Position = new TimeSpan((long)Math.Floor(value * player.NaturalDuration.TimeSpan.Ticks));
            }
        }

        public override int TotalMilliseconds => throw new NotImplementedException();
        bool isPaused = false;
        public override bool IsPaused
        {
            get => isPaused;
            set => isPaused = value;
        }
        bool isPlaying = false;
        public override bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }
    }

    public partial class MusicPlayer
    {
        private static MusicPlayer Instance = null;
        WMPlayer player = new WMPlayer();

        private MusicPlayer(){}

        public Track CurrentTrack = null;
        public MusicTab CurrentTab = null;

        public double Progress
        {
            get { return player.Progress; }
            set { player.Progress = value; }
        }

        public double Volume
        {
            get { return player.Volume; }
            set { player.Volume = value; }
        }
      
        String GetTime()
        {
            if (player.IsPlaying)
            {
                int seconds = player.TotalMilliseconds / 1000 % 60;
                int minutes = player.TotalMilliseconds / 1000 / 60 % 60;
                int hours = player.TotalMilliseconds / 1000 / 60 / 60;
                
                if (hours > 0)
                {
                    return String.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);
                }
                else
                {
                    return String.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
            else
            {
                return "0:00";
            }
        }
        
        public static MusicPlayer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MusicPlayer();
            }
            return Instance;
        }


        public bool IsPlaying { get => player.IsPlaying; }
        public bool IsPaused { get => player.IsPaused; }
        
        public void Play(String File)
        {
            player.Load(File);
            player.Play();
        }

        public void Play(Track track) => Play(track.filePath);
        public void Play() => player.Play();
        public void Pause() => player.Pause();
        public void Stop() => player.Stop();

        public void Next() { }
        public void Previous() { }
    }
}
