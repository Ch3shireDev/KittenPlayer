using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace KittenPlayer
{
    public class NAPlayer : Player
    {
        private WaveOut wave;

        public override double Volume { get; set; }
        public override double Progress { get; set; }
        public override double TotalMilliseconds { get; }
        public override bool IsPlaying { get; set; }
        public override bool IsPaused { get; set; }
        public override void Load(Track track)
        {
            if (!File.Exists(track.filePath)) return;

            var fileReader = new AudioFileReader(track.filePath);
            wave = new WaveOut();
            wave.PlaybackStopped += (x, y) => { Next(); };
            wave.Init(fileReader);

        }

        public override void Play()
        {
            wave?.Play();
        }

        public override void Pause()
        {
            wave?.Pause();
        }

        public override void Stop()
        {
            wave?.Stop();
        }

        public override void Resume()
        {
            wave?.Play();
        }

        public override event EventHandler OnTrackEnded;
    }
}
