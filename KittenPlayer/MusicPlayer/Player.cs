using System;

namespace KittenPlayer
{
    abstract public class Player
    {
        public Track CurrentTrack = null;
        public MusicTab CurrentTab = null;

        public abstract void Load(Track track);
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract void Resume();

        public void Play(Track track, MusicTab tab)
        {
            if (track == null) return;
            Stop();
            CurrentTrack = track;
            CurrentTab = tab;
            Load(track);
            Play();
        }

        public void Next()
        {
            if (CurrentTrack == null) return;
            Track track = CurrentTab?.GetNextTrack(CurrentTrack);
            CurrentTab?.Play(track);
        }

        public void Previous()
        {
            if (CurrentTrack == null) return;
            Track track = CurrentTab?.GetPreviousTrack(CurrentTrack);
            Play(track, CurrentTab);
        }

        public abstract double Volume { get; set; }
        public abstract double Progress { get; set; }
        public abstract double TotalMilliseconds { get; }
        public abstract bool IsPlaying { get; set; }
        public abstract bool IsPaused { get; set; }

        public abstract event EventHandler OnTrackEnded;
    }
}
