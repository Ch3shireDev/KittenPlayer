using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{

    public partial class MainTabs : UserControl, IKittenInterface
    {
        static public TabControl Instance = null;

        public MainTabs()
        {
            InitializeComponent();

            if (LocalData.Instance.Num() > 0)
                LocalData.Instance.LoadPlaylists(MainTab);
            else
            {
                MusicPage MainPage = new MusicPage("New Tab");
                MainTab.Controls.Add(MainPage);
                MainTab.Controls[0].Dock = DockStyle.Fill;
            }
            Instance = MainTab;
        }

        public MusicPage AddNewTab(String Name)
        {
            MusicPage tabPage = new MusicPage();
            this.MainTab.Controls.Add(tabPage);
            return tabPage;
        }

        public void AddNewTabAndRename()
        {
            MusicPage tabPage = AddNewTab("NewTab");
            tabPage.Text = "NewTab";
            this.MainTab.SelectedTab = tabPage;
            MainWindow.Instance.RenameTab();
        }

        private void DeleteTab(Control Tab)
        {
            this.MainTab.Controls.Remove(Tab);
        }



        private void MainTabs_DoubleClick(object sender, EventArgs e)
        {
            if (sender is TabControl)
            {
                TabControl Tab = sender as TabControl;
                //Debug.WriteLine(Tab.Name);
                MainWindow.Instance.RenameTab();
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

                    MainWindow.Instance.ContextTab.Show(Instance, mouseEvent.Location);
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
                int hoverTab_index = this.HoverTabIndex(Instance);
                if (hoverTab_index != this.MainTab.SelectedIndex)
                {
                    this.MainTab.DoDragDrop(this.MainTab.SelectedTab, DragDropEffects.All);
                }
            }
        }

        private void MainTabs_DragOver(object sender, DragEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            MusicPage dragTab = e.Data.GetData(typeof(MusicPage)) as MusicPage;
            if (dragTab == null) return;
            int dragTab_index = tc.TabPages.IndexOf(dragTab);
            int hoverTab_index = this.HoverTabIndex(tc);
            if (hoverTab_index < 0) { e.Effect = DragDropEffects.None; return; }
            TabPage hoverTab = tc.TabPages[hoverTab_index];
            e.Effect = DragDropEffects.Move;
            if (dragTab == hoverTab) return;
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
                MainWindow.Instance.RenameTab();
            }
        }


        private void MainTabs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainTabs_Load(object sender, EventArgs e)
        {

        }

        private void musicTab1_Load(object sender, EventArgs e)
        {

        }

        private void MainTabs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddNewTabAndRename();
        }

        private void MainTabs_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            if(e.Button == MouseButtons.Right)
            {
                AddPlaylistContext.Show(PointToScreen(e.Location));
                AddPlaylistContext.Focus();
            }
        }


        private void addPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabAndRename();
        }

        private void MainTabs_Scroll(object sender, ScrollEventArgs e)
        {
            Debug.WriteLine("MainTabs scroll");
        }
    }
}
