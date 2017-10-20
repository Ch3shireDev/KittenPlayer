using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
        List<String> Parameters = new List<String>{
            "Artist",
            "Album",
            "Title"
        };
        
        void InitPlaylistProperties()
        {
            PlaylistProperties.Items.Clear();
            foreach(String Parameter in Parameters)
            {
                PlaylistProperties.Items.Add(Parameter);
            }
            foreach(ToolStripMenuItem Item in PlaylistProperties.Items)
            {
                Item.CheckOnClick = true;
                Item.CheckedChanged += OnChecked;
            }
        }

        private void PlaylistProperties_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PlaylistProperties.AutoClose = true;
                PlaylistProperties.Hide();
            }
        }

        private void PlaylistProperties_Opened(object sender, EventArgs e)
        {
            PlaylistProperties.AutoClose = false;
        }
        
        private void OnChecked(object sender, EventArgs e)
        {

        }

        ListViewItem GetListViewItem(Track track)
        {
            ListViewItem item = new ListViewItem();
            item.Text = track.name;



            foreach (ColumnHeader Column in PlaylistView.Columns)
            {
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                //subItem.Name = Column.Name;
                //subItem.Text = track.GetValue(Column.Text);
                //item.SubItems.Insert(Column.Index, subItem);
            }

            item.SubItems.Add(track.Artist);
            item.SubItems.Add(track.Album);
            item.SubItems.Add(track.Number);

            return item;
        }

        private void ChangeArtistToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ChangeAlbumToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ChangeTrackNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ChangeTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
