using System;

namespace KittenPlayer
{
    public class MusicPlayer
    {
        public static MusicPlayer Instance = new MusicPlayer();
        public MusicTab CurrentTab = null;

        public Track CurrentTrack = null;
        public Player player;

        private MusicPlayer()
        {
            //OperatingSystem OSVersion = Environment.OSVersion;
            //OSVersion = new OperatingSystem(PlatformID.Win32NT, new Version(5, 1));
            //if (OSVersion.Version.Major < 6)
            player = new WMPlayer();
            //else player = new MFPlayer();

            player.OnTrackEnded += OnTrackEnd;
        }

        public double Progress
        {
            get => player.Progress;
            set => player.Progress = value;
        }

        public double Volume
        {
            get => player.Volume;
            set => player.Volume = value;
        }

        public bool IsPlaying => player.IsPlaying;
        public bool IsPaused => player.IsPaused;

        private void OnTrackEnd(object sender, EventArgs args)
        {
            //Track track = CurrentTab.GetNextTrack(player.CurrentTrack);
            //CurrentTab.Play(CurrentTab.Tracks.IndexOf(track));
        }

        private string GetTime()
        {
            if (player.IsPlaying)
            {
                var seconds = (int)player.TotalMilliseconds / 1000 % 60;
                var minutes = (int)player.TotalMilliseconds / 1000 / 60 % 60;
                var hours = (int)player.TotalMilliseconds / 1000 / 60 / 60;

                if (hours > 0)
                    return string.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);
                return string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            return "0:00";
        }

        public void Load(Track track)
        {
            player.Load(track);
        }

        public void Play(Track track)
        {
            Load(track);
            player.Play();
        }

        public void Play()
        {
            player.Play();
        }

        public void Pause()
        {
            player.Pause();
        }

        public void Stop()
        {
            player.Stop();
        }

        public void Next()
        {
            player.Next();
        }

        public void Previous()
        {
            player.Previous();
        }

        public void PlayAutomatic()
        {
            var tab = MainWindow.ActiveTab;
            var Index = tab.PlaylistView.SelectedIndices[0];
            Play(tab.Tracks[Index]);
        }
    }
}