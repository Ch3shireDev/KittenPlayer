using System;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

        public void Play(int Index)
        {
            if (Index >= Tracks.Count) return;
            Track track = Tracks[Index];
            if(track.Type == TrackType.Online)
            {
                track.Download();
                track.Type = TrackType.Local;
            }
            
            musicPlayer.CurrentTab = this;
            musicPlayer.CurrentTrack = track;
            musicPlayer.Stop();
            musicPlayer.Load(track);
            musicPlayer.Play();
        }

        public void PlayNext()
        {
            int Index = PlaylistView.SelectedIndices[0];
            Index++;
            if (Index < Tracks.Count)
            {
                PlaylistView.SelectedIndices.Clear();
                PlaylistView.SelectedIndices.Add(Index);
                Play(Index);
            }
        }

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
