using NAudio.Wave;
using System;

namespace KittenPlayer
{
    internal class MFPlayer : Player
    {
        private bool isPlaying;
        private WaveOut player;
        private MediaFoundationReader reader;

        private double totalMilliseconds;

        public override double Volume
        {
            get => player.Volume;
            set => player.Volume = (float)value;
        }

        public override double Progress
        {
            get => GetProgress();
            set => SetProgress(value);
        }

        public override double TotalMilliseconds => totalMilliseconds;

        public override bool IsPlaying
        {
            get => isPlaying;
            set => isPlaying = value;
        }

        public override bool IsPaused { get; set; }

        public override void Load(Track track)
        {
            if (string.IsNullOrWhiteSpace(track?.filePath)) return;
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

        public override void Pause()
        {
            player.Pause();
        }

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
                if (CurrentTrack == null) return;
                var track = MusicPlayer.Instance.CurrentTab.GetNextTrack(CurrentTrack);
                MusicPlayer.Instance.CurrentTab.Play(track);
            }
            else
                OnTrackAborted?.Invoke(sender, e);
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
            if (!IsPaused) return;
            Play();
            IsPaused = false;
        }

        private double GetProgress()
        {
            if (reader == null || player == null) return 0;
            var total = reader.Length;
            var current = reader.Position;
            if (current > total) return 1;
            return (double)current / total;
        }

        private void SetProgress(double alpha)
        {
            var total = reader.Length;
            var current = Convert.ToInt64(alpha * total);
            reader.Position = current;
        }
    }
}