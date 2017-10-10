using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class MainWindow : Form
    {

        private MusicPage AddNewTab(String Name)
        {
            MusicPage tabPage = new MusicPage();
            MainTabs.Controls.Add(tabPage);
            return tabPage;
        }

        private void AddNewTabAndRename()
        {
            MusicPage tabPage = AddNewTab("NewTab");
            tabPage.Text = "NewTab";
            MainTabs.SelectedTab = tabPage;
            RenameTab();
        }

        private void DeleteTab(Control Tab)
        {
            MainTabs.Controls.Remove(Tab);
        }



        private void MainTabs_DoubleClick(object sender, EventArgs e)
        {
            if (sender is TabControl)
            {
                TabControl Tab = (TabControl)sender;
                //Debug.WriteLine(Tab.Name);
                RenameTab();
            }

        }


        private void MainTabs_Selected(object sender, TabControlEventArgs e)
        {
            //Debug.WriteLine(e.TabPage.Text);
        }



        /// <summary>
        /// Right Click on Tab invoker. Right click automatically selects clicked tab.
        /// </summary>

        private void MainTabs_Click(object sender, EventArgs Event)
        {
            if (Event is MouseEventArgs && sender is TabControl)
            {
                MouseEventArgs mouseEvent = (MouseEventArgs)Event;
                if (mouseEvent.Button == MouseButtons.Right)
                {
                    TabControl tabControl = (TabControl)sender;

                    Rectangle mouseRect = new Rectangle(mouseEvent.X, mouseEvent.Y, 1, 1);
                    for (int i = 0; i < tabControl.TabCount; i++)
                    {
                        if (tabControl.GetTabRect(i).IntersectsWith(mouseRect))
                        {
                            tabControl.SelectedIndex = i;
                            break;
                        }
                    }

                    ContextTab.Show(MainTabs, mouseEvent.Location);
                }
            }
        }
        

        private void MainTabs_MouseDown(object sender, MouseEventArgs e)
        {
        }
        
        private void MainTabs_MouseUp(object sender, MouseEventArgs e)
        {
        }


        private void MainTabs_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Debug.WriteLine("mouse move");
                int hoverTab_index = this.HoverTabIndex(MainTabs);
                if (hoverTab_index != MainTabs.SelectedIndex)
                {
                    MainTabs.DoDragDrop(MainTabs.SelectedTab, DragDropEffects.All);
                }
            }
        }

        private void MainTabs_DragOver(object sender, DragEventArgs e)
        {

            TabControl tc = (TabControl)sender;

            MusicPage dragTab = e.Data.GetData(typeof(MusicPage)) as MusicPage;

            if (dragTab == null) return;

            int dragTab_index = tc.TabPages.IndexOf(dragTab);

            // hover over a tab?
            int hoverTab_index = this.HoverTabIndex(tc);
            if (hoverTab_index < 0) { e.Effect = DragDropEffects.None; return; }

            //tc.Controls[0].mar

            TabPage hoverTab = tc.TabPages[hoverTab_index];
            e.Effect = DragDropEffects.Move;

            // start of drag?
            if (dragTab == hoverTab) return;

            // swap dragTab & hoverTab - avoids toggeling
            Rectangle dragTabRect = tc.GetTabRect(dragTab_index);
            Rectangle hoverTabRect = tc.GetTabRect(hoverTab_index);

            if (dragTabRect.Width < hoverTabRect.Width)
            {
                Point tcLocation = tc.PointToScreen(tc.Location);

                if (dragTab_index < hoverTab_index)
                {
                    if ((e.X - tcLocation.X) > ((hoverTabRect.X + hoverTabRect.Width) - dragTabRect.Width))
                        this.swapTabPages(tc, dragTab, hoverTab);
                }
                else if (dragTab_index > hoverTab_index)
                {
                    if ((e.X - tcLocation.X) < (hoverTabRect.X + dragTabRect.Width))
                        this.swapTabPages(tc, dragTab, hoverTab);
                }
            }
            else this.swapTabPages(tc, dragTab, hoverTab);

            // select new pos of dragTab
            tc.SelectedIndex = tc.TabPages.IndexOf(dragTab);
        }


        private void swapTabPages(TabControl tc, TabPage src, TabPage dst)
        {
            int index_src = tc.TabPages.IndexOf(src);
            int index_dst = tc.TabPages.IndexOf(dst);
            tc.TabPages[index_dst] = src;
            tc.TabPages[index_src] = dst;
            tc.Refresh();
        }


        private int HoverTabIndex(TabControl tabControl)
        {
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                Point position = tabControl.PointToClient(Cursor.Position);
                Rectangle rectangle = tabControl.GetTabRect(i);
                if (rectangle.Contains(position))
                {
                    return i;
                }
            }
            return -1;
        }

        /*
       
            Dragging files to tabs.
             
        */

        private void MainTabs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void MainTabs_DragDrop(object sender, DragEventArgs e)
        {

        }


        private void MainTabs_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void MainTabs_KeyPress(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                RenameTab();
            }

        }

    }
}
