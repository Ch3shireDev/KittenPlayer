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

        public override void Load(Track track, MusicTab tab = null)
        {
            if (track == null) return;
            //if (track.path == null) return;
            //if (track.path == "") return;
            try
            {
                reader = new MediaFoundationReader(track.path);
            }
            catch
            {
                return;
            }
            if (reader == null) return;
            totalMilliseconds = reader.TotalTime.TotalMilliseconds;
            player = new WaveOut();
            player.Init(reader);
            CurrentTrack = track;
            CurrentTab = tab;
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
            if (player != null && player.PlaybackState == PlaybackState.Stopped)
            {
                OnTrackEnded?.Invoke(sender, e);
            }
            else
            {
                OnTrackAborted?.Invoke(sender, e);
            }

        }

        public override event EventHandler OnTrackEnded;
        public event EventHandler OnTrackAborted;


        public override void Stop() {
            player?.Stop();
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

        double GetProgress()
        {
            if (reader == null) return 0;
            if (player == null) return 0;
            long total = reader.Length;
            long current = reader.Position;
            if (current > total) return 1;
            return (double)current / total;
        }

        void SetProgress(double alpha)
        {
            long total = reader.Length;
            long current = Convert.ToInt64(alpha * total);
            reader.Position = current;
        }

        public override double Volume { get => player.Volume; set => player.Volume = (float)value; }
        public override double Progress { get => GetProgress(); set { SetProgress(value); } }

        double totalMilliseconds;
        public override double TotalMilliseconds => totalMilliseconds;

        bool isPlaying;
        bool isPaused;
        public override bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public override bool IsPaused { get => isPaused; set => isPaused = value; }

    }

}
