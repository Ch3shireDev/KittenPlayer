using NAudio.Wave;
using System;

namespace KittenPlayer
{
    internal class MFPlayer : Player
    {
        private WaveOut player;
        private MediaFoundationReader reader;

        public override void Load(Track track)
        {
            if (track == null) return;
            if (String.IsNullOrWhiteSpace(track.filePath)) return;
            try
            {
                reader = new MediaFoundationReader(track.filePath);
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
            CurrentTab = track.MusicTab;
        }

        public override void Pause() => player.Pause();

        public override void Play()
        {
            if (player == null) return;
            player.Play();
            isPlaying = true;

            player.PlaybackStopped += OnPlaybackStopped;
        }

        private void OnPlaybackStopped(object sender, EventArgs e)
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

        public override void Stop()
        {
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

        private double GetProgress()
        {
            if (reader == null || player == null) return 0;
            long total = reader.Length;
            long current = reader.Position;
            if (current > total) return 1;
            return (double)current / total;
        }

        private void SetProgress(double alpha)
        {
            long total = reader.Length;
            long current = Convert.ToInt64(alpha * total);
            reader.Position = current;
        }

        public override double Volume { get => player.Volume; set => player.Volume = (float)value; }
        public override double Progress { get => GetProgress(); set { SetProgress(value); } }

        private double totalMilliseconds;
        public override double TotalMilliseconds => totalMilliseconds;

        private bool isPlaying;
        private bool isPaused;
        public override bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public override bool IsPaused { get => isPaused; set => isPaused = value; }
    }
}