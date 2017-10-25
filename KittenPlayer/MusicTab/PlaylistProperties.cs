using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

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

        void ChangeProperty(int ItemIndex, int SubItemIndex)
        {
            if (ItemIndex < PlaylistView.Items.Count) {
                ListViewItem Item = PlaylistView.Items[ItemIndex];
                if (Item.SubItems.Count == 0) return;
                else if (SubItemIndex == 0)
                {
                    Item.BeginEdit();
                }
                else if (SubItemIndex < Item.SubItems.Count)
                {
                    ListViewItem.ListViewSubItem subItem = Item.SubItems[SubItemIndex];
                    if (subItem == null) return;
                    new RenameBox(PlaylistView, Item, subItem);
                }
            }
        }

        void ChangeSelectedProperty(int SubItemIndex)
        {
            if (PlaylistView.SelectedIndices.Count == 0) return;
            
            int Index = PlaylistView.SelectedIndices[0];
            ChangeProperty(Index, SubItemIndex);
            
        }

        private void ChangeTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSelectedProperty(0);
        }

        private void ChangeArtistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSelectedProperty(1);
        }

        private void ChangeAlbumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSelectedProperty(2);
        }

        private void ChangeTrackNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSelectedProperty(3);
        }
        
        public void AfterSubItemEdit(ListViewItem Item)
        {
            int Index = PlaylistView.Items.IndexOf(Item);
            if (Index < 0 || Index >= Tracks.Count) return;
            Track track = Tracks[Index];
            track.SetMetadata(Item);
        }

        private void PlaylistView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if(sender is ListViewItem)
            {
                AfterSubItemEdit(sender as ListViewItem);
            }
        }
        
        private void DropDownMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(PlaylistView.SelectedIndices.Count == 0)
            {
                DropDownMenu.Hide();
            }
            else
            {
                int Index = PlaylistView.SelectedIndices[0];
                foreach (ToolStripItem Item in DropDownMenu.Items)
                {
                    if(Item.Text == "Properties")
                        Item.Enabled = Tracks[Index].Writeable;
                }
            }
        }

    }
}
