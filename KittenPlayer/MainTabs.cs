using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainTabs : IKittenInterface
    {
        public static TabControl Instance;

        public MainTabs()
        {
            InitializeComponent();

            if (LocalData.Instance.Num() > 0)
            {
                LocalData.Instance.LoadPlaylists(MainTab);
            }
            else
            {
                var mainPage = new MusicPage("New Tab");
                MainTab.Controls.Add(mainPage);
                MainTab.Controls[0].Dock = DockStyle.Fill;
            }
            Instance = MainTab;
        }

        public MusicPage AddNewTab(string Name)
        {
            var tabPage = new MusicPage();
            MainTab.Controls.Add(tabPage);
            return tabPage;
        }

        public void AddNewTabAndRename()
        {
            var tabPage = AddNewTab("NewTab");
            tabPage.Text = "NewTab";
            MainTab.SelectedTab = tabPage;
            MainWindow.Instance.RenameTab();
        }

        private void MainTabs_DoubleClick(object sender, EventArgs e)
        {
            if (sender is TabControl)
            {
                MainWindow.Instance.RenameTab();
            }
        }

        private void MainTabs_Click(object sender, EventArgs Event)
        {
            if (!(Event is MouseEventArgs mouseEvent) || !(sender is TabControl)) return;
            switch (mouseEvent.Button)
            {
                case MouseButtons.Left:
                    Debug.WriteLine("Selected: " + Instance.SelectedTab.Text);
                    break;

                case MouseButtons.Right:
                    var tabControl = (TabControl)sender;

                    var mouseRect = new Rectangle(mouseEvent.X, mouseEvent.Y, 1, 1);
                    for (var i = 0; i < tabControl.TabCount; i++)
                        if (tabControl.GetTabRect(i).IntersectsWith(mouseRect))
                        {
                            tabControl.SelectedIndex = i;
                            break;
                        }

                    MainWindow.Instance.ContextTab.Show(Instance, mouseEvent.Location);
                    break;
            }
        }

        private void MainTabs_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var hoverTabIndex = HoverTabIndex(Instance);
            if (hoverTabIndex != MainTab.SelectedIndex)
                MainTab.DoDragDrop(MainTab.SelectedTab, DragDropEffects.All);
        }

        private void MainTabs_DragOver(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(typeof(MusicPage)) is MusicPage dragTab)) return;
            var dragTabIndex = MainTab.TabPages.IndexOf(dragTab);
            var hoverTabIndex = HoverTabIndex(MainTab);
            if (hoverTabIndex < 0)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            var hoverTab = MainTab.TabPages[hoverTabIndex] as MusicPage;
            e.Effect = DragDropEffects.Move;
            if (dragTab == hoverTab) return;
            var dragTabRect = MainTab.GetTabRect(dragTabIndex);
            var hoverTabRect = MainTab.GetTabRect(hoverTabIndex);

            if (dragTabRect.Width < hoverTabRect.Width)
            {
                var tcLocation = MainTab.PointToScreen(MainTab.Location);
                if (dragTabIndex < hoverTabIndex)
                {
                    if (e.X - tcLocation.X > hoverTabRect.X + hoverTabRect.Width - dragTabRect.Width)
                        SwapTabPages(dragTab, hoverTab);
                }
                else if (dragTabIndex > hoverTabIndex)
                {
                    if (e.X - tcLocation.X < hoverTabRect.X + dragTabRect.Width)
                        SwapTabPages(dragTab, hoverTab);
                }
            }
            else
            {
                SwapTabPages(dragTab, hoverTab);
            }
            MainTab.SelectedIndex = MainTab.TabPages.IndexOf(dragTab);
        }

        public void SwapTabPages(MusicPage src, MusicPage dst)
        {
            if (src == dst) return;
            if (src is null || dst is null) return;

            var indexSrc = MainTab.TabPages.IndexOf(src);
            var indexDst = MainTab.TabPages.IndexOf(dst);

            MainTab.TabPages[indexDst] = src;
            MainTab.TabPages[indexSrc] = dst;

            MainTab.Controls.SetChildIndex(src, indexDst);
            MainTab.Controls.SetChildIndex(dst, indexSrc);

            MainTab.Refresh();
        }

        private static int HoverTabIndex(TabControl tabControl)
        {
            for (var i = 0; i < tabControl.TabPages.Count; i++)
            {
                var position = tabControl.PointToClient(Cursor.Position);
                var rectangle = tabControl.GetTabRect(i);
                if (rectangle.Contains(position)) return i;
            }
            return -1;
        }

        private void MainTabs_DragEnter(object sender, DragEventArgs e) => e.Effect = DragDropEffects.All;

        private void MainTabs_KeyPress(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                MainWindow.Instance.RenameTab();
        }

        private void MainTabs_MouseDoubleClick(object sender, MouseEventArgs e) => AddNewTabAndRename();

        private void MainTabs_MouseClick(object sender, MouseEventArgs e)
        {
            Focus();
            if (e.Button == MouseButtons.Right)
            {
                AddPlaylistContext.Show(PointToScreen(e.Location));
                AddPlaylistContext.Focus();
            }
        }

        private void addPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabAndRename();
        }
    }
}