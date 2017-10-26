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
        MusicPlayer musicPlayer = MusicPlayer.Instance;

        public MusicTab()
        {
            InitializeComponent();
            InitPlaylistProperties();
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

        public Track GetPreviousTrack(Track Current)
        {
            int Index = Tracks.IndexOf(Current);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index - 1))
            {
                Index--;
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
                return Tracks[Index].path;
            }
        }

        


        int prevItem = -1;

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
                MusicPlayer player = MusicPlayer.Instance;
                if (player.IsPlaying)
                {
                    MusicPlayer.Instance.Pause();
                }
            }
        }

        private void PlayClick(object sender, EventArgs e)
        {
            PlaySelectedTrack();
            DropDownMenu.Hide();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.Instance.Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.Instance.Stop();
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
            else if(e.KeyCode == Keys.F1)
            {
                PlaylistProperties.Show(Cursor.Position);
                PlaylistProperties.Focus();
            }
        }

        public void RefreshPlaylistView()
        {
 
        }

        public override void Refresh()
        {
            base.Refresh();
            RefreshPlaylistView();
            if (Application.OpenForms.Count == 0) return;
            MainWindow.SavePlaylists();
            
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

        private void PlaylistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }
        
        private void PlaylistView_MouseMove(object sender, MouseEventArgs e)
        {
        }



        private void PlaylistView_MouseEnter(object sender, EventArgs e)
        {

        }

        List<ToolStripMenuItem> MenuItems = new List<ToolStripMenuItem>()
        {
            { new ToolStripMenuItem("Play", null, (sender, e) => { }) },
            { new ToolStripMenuItem("Pause", null, (sender, e) => { }) },
            { new ToolStripMenuItem("Stop", null, (sender, e) => { }) },
            { new ToolStripMenuItem("Just Download", null, (sender, e) => { }) },
            { new ToolStripMenuItem("Download again", null, (sender, e) => { }) },
            { new ToolStripMenuItem("Download and Play", null, (sender, e) => { }) },
        };


        private void PlaylistView_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if (PlaylistView.SelectedIndices.Count == 0) return;
                int Index = PlaylistView.SelectedIndices[0];
                Track track = Tracks[Index];

                var Items = new Dictionary<String, ToolStripItem>();
                foreach(var item in MenuItems)
                {
                    DropDownMenu.Items.Remove(item);
                    Items.Add(item.Text, item);
                }

                if(track.Status == Track.StatusType.Local)
                {
                    DropDownMenu.Items.Insert(0, Items["Play"]);
                    DropDownMenu.Items.Insert(1, Items["Pause"]);
                    DropDownMenu.Items.Insert(2, Items["Stop"]);
                }
                else if(track.Status == Track.StatusType.Offline)
                {
                    DropDownMenu.Items.Insert(0, Items["Play"]);
                    DropDownMenu.Items.Insert(1, Items["Download again"]);
                }
                else if(track.Status == Track.StatusType.Online)
                {
                    DropDownMenu.Items.Insert(0, Items["Download and Play"]);
                    DropDownMenu.Items.Insert(1, Items["Just Download"]);
                }

                var font = DropDownMenu.Items[0].Font;
                DropDownMenu.Items[0].Font = new System.Drawing.Font(font.FontFamily, font.Size, System.Drawing.FontStyle.Bold);
                DropDownMenu.Show(PlaylistView.PointToScreen(e.Location));
            }
        }

        private void changePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void downloadAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



    }


}