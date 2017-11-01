using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

        public async Task HideBar(Track track)
        {
            ListViewEx listViewEx = PlaylistView as ListViewEx;

            ProgressBar bar = track.progressBar;

            while (bar.Value != 100)
            {
                await Task.Delay(25);
            }

            bar.Hide();
            listViewEx.RemoveEmbeddedControl(bar);
        }

        public async Task<bool> Download(Track track)
        {
            Rectangle rect = track.Item.SubItems[5].Bounds;
            ListViewEx listViewEx = PlaylistView as ListViewEx;
            ProgressBar bar = new ProgressBar
            {
                Bounds = rect
            };

            int Index = PlaylistView.Items.IndexOf(track.Item);
            listViewEx.AddEmbeddedControl(bar, 5, Index);
            bar.Show();
            bar.Focus();

            track.progressBar = bar;
            bool Success = await track.Download();
            PlaylistView.FocusedItem = track.Item;
            HideBar(track);
            if (Success)
            {
                PlaylistView.Items[Index] = track.GetListViewItem(PlaylistView);
                track.Item = PlaylistView.Items[Index];
                track.OfflineToLocalData();
                track.SetMetadata();
                track.SaveMetadata();
            }

            //this one should be outside download function

            //else
            //{
            //    PlaylistView.Items.Remove(track.Item);
            //    Tracks.Remove(track);
            //}

            return Success;
        }

        public async Task Play(int Index)
        {
            if (Index >= Tracks.Count || Index < 0) return;
            Track track = Tracks[Index];
            if(track.IsOnline)
            {
                bool Success = await Download(track);
                if (!Success)
                {
                    Tracks.RemoveAt(Index);
                    PlaylistView.Items.RemoveAt(Index);
                    MainWindow.SavePlaylists();
                    return;
                }
                MainWindow.SavePlaylists();
            }
            musicPlayer.CurrentTab = this;
            musicPlayer.CurrentTrack = track;
            musicPlayer.Stop();
            musicPlayer.Load(track, this);
            musicPlayer.Play();
                //RemoveTrack(Index);
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
