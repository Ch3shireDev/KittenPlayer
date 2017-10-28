using System;
using System.Windows.Forms;
using System.Drawing;

namespace KittenPlayer
{
    public partial class MainWindow : Form
    {
        public System.Windows.Forms.TabControl MainTabs;

        public static MusicPage ActivePage => Instance.MainTabs.Controls[Instance.MainTabs.SelectedIndex] as MusicPage;
        public static MusicTab ActiveTab => ActivePage.musicTab;

        private void MainTabsInit()
        {

            MainTabs.AllowDrop = true;
            MainTabs.CausesValidation = false;
            MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            MainTabs.Location = new System.Drawing.Point(3, 3);
            MainTabs.Name = "MainTabs";
            MainTabs.SelectedIndex = 0;
            MainTabs.Size = new System.Drawing.Size(965, 207);
            MainTabs.TabIndex = 0;
            MainTabs.SelectedIndexChanged += new System.EventHandler(MainTabs_SelectedIndexChanged);
            MainTabs.Selected += new System.Windows.Forms.TabControlEventHandler(MainTabs_Selected);
            MainTabs.Click += new System.EventHandler(MainTabs_Click);
            MainTabs.DragDrop += new System.Windows.Forms.DragEventHandler(MainTabs_DragDrop);
            MainTabs.DragEnter += new System.Windows.Forms.DragEventHandler(MainTabs_DragEnter);
            MainTabs.DragOver += new System.Windows.Forms.DragEventHandler(MainTabs_DragOver);
            MainTabs.DoubleClick += new System.EventHandler(MainTabs_DoubleClick);
            MainTabs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(MainTabs_KeyPress);
            MainTabs.MouseDown += new System.Windows.Forms.MouseEventHandler(MainTabs_MouseDown);
            MainTabs.MouseMove += new System.Windows.Forms.MouseEventHandler(MainTabs_MouseMove);
            MainTabs.MouseUp += new System.Windows.Forms.MouseEventHandler(MainTabs_MouseUp);
            MainTabs.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(MainTabs_KeyPress);


            // 
            // AddPlaylistStrip
            // 
            this.AddPlaylistStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPlaylistToolStripMenuItem1});
            this.AddPlaylistStrip.Name = "AddPlaylistStrip";
            this.AddPlaylistStrip.Size = new System.Drawing.Size(138, 26);
            // 
            // addPlaylistToolStripMenuItem1
            // 
            this.addPlaylistToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPlaylistToolStripMenuItem1.Name = "addPlaylistToolStripMenuItem1";
            this.addPlaylistToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.addPlaylistToolStripMenuItem1.Text = "Add Playlist";
            this.addPlaylistToolStripMenuItem1.Click += new System.EventHandler(this.addNewPlaylistToolStripMenuItem_Click);
            // 
            // playControl1
            // 
        }

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
                TabControl Tab = sender as TabControl;
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
                RenameTab();
            }
        }

    }
}
