using System;
using System.Diagnostics;

namespace KittenPlayer
{

    public partial class MusicPlayer
    {
        private static MusicPlayer Instance = null;
        WMPlayer player = new WMPlayer();

        private MusicPlayer(){}

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
        
        public static MusicPlayer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new MusicPlayer();
            }
            return Instance;
        }


        public bool IsPlaying { get => player.IsPlaying; }
        public bool IsPaused { get => player.IsPaused; }
        
        public void Play(String File)
        {
            player.Load(File);
            player.Play();
        }

        public void Play(Track track) => Play(track.filePath);
        public void Play() => player.Play();
        public void Pause() => player.Pause();
        public void Stop() => player.Stop();

        public void Next() { }
        public void Previous() { }
    }
}
