using System;
using NAudio.Wave;

namespace KittenPlayer
{
    class MFPlayer : Player
    {
        WaveOut player = new WaveOut();

        public override void Load(string fileName)
        {
            MediaFoundationReader reader = new MediaFoundationReader(fileName);
            totalMilliseconds = reader.TotalTime.TotalMilliseconds;
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

        double totalMilliseconds;
        public override double TotalMilliseconds => totalMilliseconds;

        bool isPlaying;
        bool isPaused;
        public override bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        public override bool IsPaused { get => isPaused; set => isPaused = value; }

    }

}
