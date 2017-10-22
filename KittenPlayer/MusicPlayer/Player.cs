using System;
using System.IO;


namespace KittenPlayer
{
    abstract public class Player
    {
        public Track CurrentTrack = null;
        public MusicTab CurrentTab = null;

        public abstract void Load(Track track, MusicTab tab = null);
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract void Resume();

        public void Play(Track track, MusicTab tab)
        {
            if (track == null) return;
            Stop();
            track.Download();
            CurrentTrack = track;
            CurrentTab = tab;
            Load(track, tab);
            Play();
        }

        public void Next()
        {
            if (CurrentTrack == null) return;
            Track track = CurrentTab?.GetNextTrack(CurrentTrack);
            Play(track, CurrentTab);
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
