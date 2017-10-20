using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace KittenPlayer
{
    class RenameBox : TextBox
    {

        TabControl MainTabs = null;
        

        public RenameBox(TabControl MainTabs)
        {
            this.MainTabs = MainTabs;

            int TabNum = MainTabs.SelectedIndex;
            Rectangle rect = MainTabs.GetTabRect(TabNum);
            Point point = MainTabs.Location;
            rect = MainTabs.RectangleToScreen(rect);
            rect = MainTabs.Parent.RectangleToClient(rect);

            RenameBox renameBox = this;
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

        ListViewItem Item;
        ListViewItem.ListViewSubItem subItem;

        public RenameBox(Control control, ListViewItem Item, ListViewItem.ListViewSubItem subItem)
        {
            BorderStyle = BorderStyle.None;
            this.Item = Item;
            this.subItem = subItem;
            this.Text = subItem.Text;
            control.Controls.Add(this);
            BringToFront();
            Focus();
            Show();
            KeyPress += OnKeyPress;
            LostFocus += OnLostFocus;
            Rectangle rectangle = subItem.Bounds;
            SetBounds(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        private void OnLostFocus(object sender, EventArgs e)
        {
            AcceptChange();
        }

        /// <summary>
        /// Method for handling the textbox. For now Enter means to update the name and Esc to back to state before.
        /// </summary>

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AcceptChange();
            }

            else if (e.KeyChar == (char)Keys.Escape)
            {
                RejectChange();
            }

        }

        private void AcceptChange()
        {
            if (MainTabs != null && MainTabs.SelectedTab != null)
            {
                MainTabs.SelectedTab.Text = this.Text;
            }
            else if(subItem != null)
            {
                subItem.Text = this.Text;
                if(Item != null)
                {
                    MusicTab tab = Item.ListView.Parent as MusicTab;
                    tab?.AfterSubItemEdit(Item);
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
        /// Auxiliary method for killing a textbox.
        /// </summary>

        private void KillTextBox()
        {
            Parent?.Controls.Remove(this);
            Parent?.Select();
            Hide();
        }

    }
}
