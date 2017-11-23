using NAudio.Wave;
using System;
using System.Diagnostics;
using System.IO;

namespace KittenPlayer
{
    public class NAPlayer : Player
    {
        private WaveOut _wave;
        private AudioFileReader _fileReader;

        public override event EventHandler OnTrackEnded;

        public override double Volume
        {
            get => _wave?.Volume ?? 0;
            set
            {
                if (_wave != null) _wave.Volume = (float)value;
            }
        }

        public override double Progress
        {
            get
            {
                if (_fileReader == null) return 0;
                if (!IsPlaying && !IsPaused) return 0;
                double pos = _fileReader.Position * 1.0 / _fileReader.Length;
                if (pos >= 0 && pos < 1) return pos;
                if (pos >= 1) return 1;
                else return 0;
            }
            set
            {
                Debug.WriteLine("Value: " + value);
                if (value > 1) value = 1;
                if (value < 0) value = 0;
                _fileReader.Position = (int)(value * (_fileReader.Length - 1));
            }
        }

        public override double TotalMilliseconds => _fileReader.TotalTime.TotalMilliseconds;
        public override bool IsPlaying { get; set; }
        public override bool IsPaused { get; set; }

        public override void Load(Track track)
        {
            if (track == null) return;
            if (!File.Exists(track.filePath)) return;
            _fileReader?.Close();
            _fileReader = new AudioFileReader(track.filePath);
            _wave = new WaveOut();
            _wave.Init(_fileReader);
            CurrentTab = track.MusicTab;
            CurrentTrack = track;
            _wave.PlaybackStopped += (x, y) =>
            {
                if (_wave.PlaybackState != PlaybackState.Stopped || !IsPlaying) return;
                IsPlaying = false;
                Next();
            };
        }

        public override void Play()
        {
            _wave?.Play();
            if (_wave?.PlaybackState != PlaybackState.Playing) return;
            IsPlaying = true;
            IsPaused = false;
        }

        public override void Pause()
        {
            _wave?.Pause();
            if (_wave?.PlaybackState == PlaybackState.Paused) IsPaused = true;
        }

        public override void Stop()
        {
            _wave?.Stop();
            if (_wave?.PlaybackState == PlaybackState.Stopped)
            {
                IsPlaying = false;
                IsPaused = false;
            }
        }

        public override void Resume()
        {
            Play();
        }
    }
}