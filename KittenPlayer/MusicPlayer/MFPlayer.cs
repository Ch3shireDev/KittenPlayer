using System;
using System.IO;
using System.Diagnostics;
using NAudio.Wave;


namespace KittenPlayer
{
    class MFPlayer : Player
    {
        WaveOut player;
        MediaFoundationReader reader;

        public override void Load(string fileName)
        {
            if (fileName == null) return;
            try
            {
                reader = new MediaFoundationReader(fileName);
            }
            catch
            {
                return;
            }
            if (reader == null) return;
            totalMilliseconds = reader.TotalTime.TotalMilliseconds;
            player = new WaveOut();
            player.Init(reader);
        
        }

        public override void Pause() => player.Pause();
        public override void Play() {
            if (player == null) return;
            player.Play();
            isPlaying = true;

            player.PlaybackStopped += OnPlaybackStopped;
        }

        void OnPlaybackStopped(object sender, EventArgs e)
        {
            if (player.PlaybackState == PlaybackState.Stopped)
            {
                OnTrackEnded?.Invoke(sender, e);
            }
            else
            {
                OnTrackAborted?.Invoke(sender, e);
            }
   
        }

        public event EventHandler OnTrackEnded;
        public event EventHandler OnTrackAborted;


        public override void Stop() {
            if (player != null)
            {
                player.Stop();
            }
            player = null;
            reader = null;
        }

        public override void Resume()
        {
            if (IsPaused)
            {
                Play();
                IsPaused = false;
            }
        }

        public override double Volume { get => player.Volume; set => player.Volume = (float)value; }
        public override double Progress { get => 0; set { return; } }

        double totalMilliseconds;
        public override double TotalMilliseconds => totalMilliseconds;

        bool isPlaying;
        bool isPaused;
        public override bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public override bool IsPaused { get => isPaused; set => isPaused = value; }

    }

}
