using System;
using System.IO;

namespace KittenPlayer
{


    class WMPlayer : Player
    {
        public System.Windows.Media.MediaPlayer player = new System.Windows.Media.MediaPlayer();

        public WMPlayer() => player.MediaEnded += LoadNextTrack;

        void LoadNextTrack(object sender, EventArgs e)
        {
            //bool exists = false;
            //if (CurrentTrack == null) return;
            ////if (CurrentTab == null) return;
            //Track nextTrack = CurrentTrack;
            //while (!exists)
            //{
            //    if (nextTrack == null) return;
            //    //nextTrack = CurrentTab.GetNextTrack(nextTrack);
            //    exists = File.Exists(nextTrack.filePath);
            //}
            //CurrentTrack = nextTrack;
            ////CurrentTab.SelectTrack(CurrentTrack);
            ////Play();
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
            get
            {
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

        public override double TotalMilliseconds => player.NaturalDuration.TimeSpan.TotalMilliseconds;
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
}
