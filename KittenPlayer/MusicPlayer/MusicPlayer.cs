using System;
using System.Diagnostics;

namespace KittenPlayer
{

    public partial class MusicPlayer
    {
        private static MusicPlayer instance = null;
        public static MusicPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MusicPlayer();
                }
                return instance;
            }
        }
        Player player = new MFPlayer();

        private MusicPlayer()
        {
            player.OnTrackEnded += OnTrackEnd;
        }

        void OnTrackEnd(object sender, EventArgs args)
        {
            Track track = CurrentTab.GetNextTrack(player.CurrentTrack);
            player.Play(track, CurrentTab);
        }

        public Track CurrentTrack = null;
        public MusicTab CurrentTab = null;

        public double Progress
        {
            get { return player.Progress; }
            set { player.Progress = value; }
        }

        public double Volume
        {
            get { return player.Volume; }
            set { player.Volume = value; }
        }
      
        String GetTime()
        {
            if (player.IsPlaying)
            {
                int seconds = (int)player.TotalMilliseconds / 1000 % 60;
                int minutes = (int)player.TotalMilliseconds / 1000 / 60 % 60;
                int hours = (int)player.TotalMilliseconds / 1000 / 60 / 60;
                
                if (hours > 0)
                {
                    return String.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);
                }
                else
                {
                    return String.Format("{0:00}:{1:00}", minutes, seconds);
                }
            }
            else
            {
                return "0:00";
            }
        }

        public bool IsPlaying { get => player.IsPlaying; }
        public bool IsPaused { get => player.IsPaused; }
        
        public void Load(Track track, MusicTab tab)
        {
            player.Load(track, tab);
        }

        public void Play(Track track, MusicTab tab)
        {
            Load(track, tab);
            player.Play();
        }

        public void Play() => player.Play();
        public void Pause() => player.Pause();
        public void Stop() => player.Stop();

        public void Next() => player.Next();
        public void Previous() => player.Previous();
    }
}
