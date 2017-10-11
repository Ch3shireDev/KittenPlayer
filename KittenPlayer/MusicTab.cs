using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
        public List<Track> Tracks = new List<Track>();
        MusicPlayer musicPlayer = MusicPlayer.GetInstance();

        public MusicTab()
        {
            InitializeComponent();
        }

        public void Play(Track track)
        {
            musicPlayer.CurrentTab = this;
            musicPlayer.CurrentTrack = track;
            musicPlayer.Play();
        }

        public void PlaySelected()
        {
            if (PlaylistView.SelectedIndices.Count > 0)
            {
                musicPlayer.CurrentTab = this;
                int Index = PlaylistView.SelectedIndices[0];
                Track track = Tracks[Index];
                musicPlayer.CurrentTrack = track;
                musicPlayer.Play();
            }
        }

        public void SelectTrack(Track track)
        {
            PlaylistView.SelectedIndices.Clear();
            int Index = Tracks.IndexOf(track);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index))
            {
                PlaylistView.SelectedIndices.Add(Index);
            }
        }

        public Track GetNextTrack(Track Current)
        {
            int Index = Tracks.IndexOf(Current);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index + 1))
            {
                Index++;
                return Tracks[Index];
            }
            else
            {
                return null;
            }
        }

        public String GetSelectedTrackPath()
        {
            if (PlaylistView.SelectedIndices.Count == 0)
            {
                return "";
            }
            else
            {
                int Index = PlaylistView.SelectedIndices[0];
                return Tracks[Index].filePath;
            }
        }
        
        private void PlaylistView_Click(object sender, EventArgs e)
        {

        }
        
        private void PlaylistView_DoubleClick(object sender, EventArgs e)
        {
            PlaySelected();
        }
        
        public void PlaySelectedTrack()
        {
            PlaySelected();
        }

        private void PlaylistView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = new List<ListViewItem>();
            items.Add((ListViewItem)e.Item);
            foreach (ListViewItem lvi in PlaylistView.SelectedItems)
            {
                if (!items.Contains(lvi))
                {
                    items.Add(lvi);
                }
            }
            PlaylistView.DoDragDrop(items, DragDropEffects.Move);
        }

        int prevItem = -1;

        private void PlaylistView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        
        private void MusicTab_DoubleClick(object sender, EventArgs e)
        {
            if(PlaylistView.TabIndex >= 0 && PlaylistView.TabIndex < PlaylistView.Items.Count)
            {
                musicPlayer.Play(Tracks[PlaylistView.TabIndex].filePath);
            }
        }

        private void MusicTab_Click(object sender, EventArgs e)
        {
        }

        private void PlaylistView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PlaylistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlaySelectedTrack();
            }
            else if (e.KeyChar == (char)Keys.Space)
            {
                MusicPlayer player = MusicPlayer.GetInstance();
                if (MusicPlayer.IsPlaying)
                {
                    MusicPlayer.GetInstance().Pause();
                }
            }

            if (e.KeyChar == (char)Keys.Down)
            {
                RemoveSelectedTracks();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlaylistView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                DropDownMenu.Show(Cursor.Position);
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            PlaySelectedTrack();
            DropDownMenu.Hide();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.GetInstance().Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.GetInstance().Stop();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedTracks();
        }

        private void PlaylistView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                RemoveSelectedTracks();
            }
        }

        public void RefreshPlaylistView()
        {
            for(int i = 0; i < PlaylistView.Items.Count; i++)
            {
                PlaylistView.Items[i].Text = "" + (i + 1);
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            RefreshPlaylistView();
            if (Application.OpenForms.Count == 0) return;
            MainWindow Window = Application.OpenForms[0] as MainWindow;
            Window.SavePlaylists();
            
        }
        
        public void SelectAll()
        {
            foreach(ListViewItem Item in PlaylistView.Items)
            {
                Item.Selected = true;
            }
        }

        private void PlaylistView_Leave(object sender, EventArgs e)
        {
            foreach(ListViewItem Item in PlaylistView.Items)
            {
                Item.Selected = false;
            }
        }
    }
}