using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

        public void Play(int Index)
        {
            if (Index >= Tracks.Count || Index < 0) return;
            Track track = Tracks[Index];
            if(track.IsOnline)
            {
                track.Download();
                MainWindow window = Application.OpenForms[0] as MainWindow;
                window.SavePlaylists();
            }
            if (track.IsValid())
            {
                musicPlayer.CurrentTab = this;
                musicPlayer.CurrentTrack = track;
                musicPlayer.Stop();
                musicPlayer.Load(track, this);
                musicPlayer.Play();
            }
            else
            {
                RemoveTrack(Index);
            }
        }

        //public void PlayNext()
        //{
        //    int Index = Tracks.IndexOf(musicPlayer.CurrentTrack);
        //    Index++;
        //    if (Index >= 0 && Index < Tracks.Count)
        //    {
        //        PlaylistView.SelectedIndices.Clear();
        //        PlaylistView.SelectedIndices.Add(Index);
        //        Play(Index);
        //    }
        //    //if (PlaylistView.SelectedIndices.Count == 0) return;
        //    //int Index = PlaylistView.SelectedIndices[0];
        //    //Index++;
        //    //if (Index < Tracks.Count)
        //    //{
        //    //    PlaylistView.SelectedIndices.Clear();
        //    //    PlaylistView.SelectedIndices.Add(Index);
        //    //    Play(Index);
        //    //}
        //}

        public void PlaySelected()
        {
            if (PlaylistView.SelectedIndices.Count == 0) return;
            musicPlayer.CurrentTab = this;
            int Index = PlaylistView.SelectedIndices[0];
            Play(Index);
        }
        
        private void PlaylistView_DoubleClick(object sender, EventArgs e) =>
            PlaySelected();

        public void PlaySelectedTrack() =>
            PlaySelected();
        
        private void MusicTab_DoubleClick(object sender, EventArgs e) =>
            Play(PlaylistView.TabIndex);
    }
}
