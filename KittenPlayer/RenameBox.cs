using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{
    public class RenameBox : TextBox
    {
        private readonly ListViewItem Item;
        private readonly TabControl MainTabs;
        private readonly ListView PlaylistView;

        private readonly int SubItemIndex = -1;

        public RenameBox(TabControl MainTabs)
        {
            this.MainTabs = MainTabs;

            var TabNum = MainTabs.SelectedIndex;
            var rect = MainTabs.GetTabRect(TabNum);
            var point = MainTabs.Location;
            rect = MainTabs.RectangleToScreen(rect);
            rect = MainTabs.Parent.RectangleToClient(rect);

            var renameBox = this;
            MainTabs.GetControl(TabNum).Controls.Add(renameBox);
            MainTabs.Parent.Controls.Add(renameBox);
            renameBox.TextAlign = HorizontalAlignment.Center;
            renameBox.Font = MainTabs.GetControl(TabNum).Font;
            renameBox.SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
            renameBox.Text = MainTabs.Controls[TabNum].Text;
            renameBox.BringToFront();
            renameBox.KeyPress += OnKeyPress;
            renameBox.LostFocus += OnLostFocus;
            renameBox.Focus();
            renameBox.Show();
        }

        public RenameBox(ListView PlaylistView, int SubItemIndex)
        {
            Debug.WriteLine(SubItemIndex);

            var Index = PlaylistView.SelectedIndices[0];
            this.SubItemIndex = SubItemIndex;

            Item = PlaylistView.Items[Index];

            foreach (ListViewItem item in PlaylistView.SelectedItems)
                if (item.Focused)
                {
                    Item = item;
                    break;
                }

            this.PlaylistView = PlaylistView;

            BorderStyle = BorderStyle.None;

            Text = Item.SubItems[SubItemIndex].Text;
            //PlaylistView.Controls.Add(this);
            var listView = PlaylistView as ListViewEx;
            listView.AddEmbeddedControl(this, SubItemIndex, Index);
            KeyPress += OnKeyPress;
            LostFocus += OnLostFocus;
            var rectangle = Item.SubItems[SubItemIndex].Bounds;
            SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            BringToFront();
            Focus();
            Show();
        }

        private void OnLostFocus(object sender, EventArgs e)
        {
            AcceptChange();
        }

        /// <summary>
        ///     Method for handling the textbox. For now Enter means to update the name and Esc to back to state before.
        /// </summary>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                AcceptChange();
            else if (e.KeyChar == (char)Keys.Escape)
                RejectChange();
        }

        public void AcceptChange()
        {
            if (MainTabs != null && MainTabs.SelectedTab != null)
            {
                MainTabs.SelectedTab.Text = Text;
            }
            else if (SubItemIndex >= 0)
            {
                var tab = Item.ListView.Parent as MusicTab;
                if (tab == null) return;

                foreach (int Index in PlaylistView.SelectedIndices)
                {
                    var Item = PlaylistView.Items[Index];
                    Item.SubItems[SubItemIndex].Text = Text;
                    tab.AfterSubItemEdit(Item);
                }
            }

            KillTextBox();
            MainWindow.SavePlaylists();
        }

        private void RejectChange()
        {
            KillTextBox();
        }

        /// <summary>
        ///     Auxiliary method for killing a textbox.
        /// </summary>
        private void KillTextBox()
        {
            Parent?.Controls.Remove(this);
            Parent?.Select();
            Hide();
        }
    }
}