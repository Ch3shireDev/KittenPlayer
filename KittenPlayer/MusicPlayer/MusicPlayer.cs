using System;
using System.Diagnostics;
using NAudio.Wave;


namespace KittenPlayer
{
    public class MusicPlayer
    {
        public static readonly MusicPlayer Instance = new MusicPlayer();
        public MusicTab CurrentTab = null;

        public Track CurrentTrack = null;
        private readonly Player player;

        private MusicPlayer()
        {
            player = new NAPlayer();
            //OperatingSystem OSVersion = Environment.OSVersion;
            //if (OSVersion.Version.Major < 6)
            //    player = new WMPlayer();
            //else
            //    player = new MFPlayer();
            
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
            MainWindow.Instance.SetDefaultTitle();
        }

        private string GetTime()
        {
            if (player.IsPlaying)
            {
                var seconds = (int) player.TotalMilliseconds / 1000 % 60;
                var minutes = (int) player.TotalMilliseconds / 1000 / 60 % 60;
                var hours = (int) player.TotalMilliseconds / 1000 / 60 / 60;

                if (hours > 0)
                    return $"{hours}:{minutes:00}:{seconds:00}";
                return $"{minutes:00}:{seconds:00}";
            }
            return "";
        }

        public void Load(Track track)
        {
            player.Load(track);
        }

        public void Play(Track track)
        {
            Load(track);
            Play();
        }

        public void Play()
        {
            if (!string.IsNullOrWhiteSpace(CurrentTrack.Title))
                MainWindow.Instance.Text = CurrentTrack.Title;
            player.Play();
        }

        public void Pause() => player.Pause();

        public void Stop() => player.Stop();

        public void Next() => player.Next();

        public void Previous() => player.Previous();

        public void PlayAutomatic()
        {
            var tab = MainWindow.ActiveTab;
            var index = tab.PlaylistView.SelectedIndices[0];
            Play(tab.Tracks[index]);
        }
    }
}