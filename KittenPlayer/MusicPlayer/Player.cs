using System;
using System.IO;


namespace KittenPlayer
{
    abstract public class Player
    {
        public Track CurrentTrack = null;

        public abstract void Load(String fileName);
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract void Resume();

        public abstract double Volume { get; set; }
        public abstract double Progress { get; set; }
        public abstract double TotalMilliseconds { get; }
        public abstract bool IsPlaying { get; set; }
        public abstract bool IsPaused { get; set; }
    }
}
