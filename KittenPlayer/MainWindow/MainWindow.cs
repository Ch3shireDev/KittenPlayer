using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainWindow : Form, IKittenInterface
    {
        private readonly ActionsControl actionsControl = ActionsControl.GetInstance();

        private readonly MusicPlayer musicPlayer = MusicPlayer.Instance;

        private Form About;
        public Options Options = new Options();

        private RenameBox renameBox;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            InitializeTrackbarTimer();

            //add current version
            Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static MusicPage ActivePage => GetActivePage();
        public static MusicTab ActiveTab => ActivePage?.musicTab;

        public static MainWindow Instance { get; private set; }

        [STAThread]
        private static void Main()
        {
            //TextWriterTraceListener[] listeners = new TextWriterTraceListener[] 
            //{
            //    new TextWriterTraceListener("log.txt"),
            //    new TextWriterTraceListener(Console.Out)
            //};
            //Debug.Listeners.AddRange(listeners);
            //Debug.WriteLine("Some Value", "Some Category");
            //Debug.Flush();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        private static MusicPage GetActivePage()
        {
            if (Instance.MainTab.MainTab.Controls.Count == 0)
                Instance.MainTab.AddNewTab("New Tab");
            var Index = Instance.MainTab.MainTab.SelectedIndex;
            return Instance.MainTab.MainTab.Controls[Index] as MusicPage;
        }

        public static void SavePlaylists()
        {
            LocalData.Instance.SavePlaylists(MainTabs.Instance);
        }

        private void MainWindow_Click(object sender, EventArgs e)
        {
            Focus();
        }

        private void MainWindow_DoubleClick(object sender, EventArgs e)
        {
        }

        private void ContextTab_Opening(object sender, CancelEventArgs e)
        {
        }

        /// <summary>
        ///     On renaming action TextBox appears in exact place of original playlist name.
        /// </summary>
        public void RenameTab()
        {
            renameBox = new RenameBox(MainTabs.Instance);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameTab();
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        public void addNewPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.MainTab.AddNewTabAndRename();
        }

        private void deletePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var TabNum = MainTab.MainTab.SelectedIndex;
            MainTab.MainTab.Controls.RemoveAt(MainTab.MainTab.SelectedIndex);
        }


        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actionsControl.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actionsControl.Redo();
        }

        public string GetSelectedTrackPath()
        {
            var TabIndex = MainTab.MainTab.SelectedIndex;
            var musicPage = MainTab.Controls[TabIndex] as MusicPage;
            var Path = musicPage.GetSelectedTrackPath();
            return Path;
        }

        private void youTubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            currentPage.DeleteSelectedTracks();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            if (currentPage == null) return;
            if (currentPage.musicTab == null) return;
            if (currentPage.musicTab.PlaylistView == null) return;
            if (currentPage.musicTab.PlaylistView.Focused)
                currentPage.SelectAll();
            else if (Instance.searchBarPage.searchBar.Focused)
                Instance.searchBarPage.searchBar.SelectAll();
        }

        private void addYoutubePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddPlaylistForm();
            form.ShowDialog();
            var Tracks = form.Tracks;
            var currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            currentPage.musicTab.AddTrack(Tracks);
            SavePlaylists();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PlaylistProperties_Opening(object sender, CancelEventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (About == null)
            {
                About = new AboutForm();
                About.Show();
                About.Focus();
            }
        }

        private void addYoutubeTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void addYoutubeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void savePlaylistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePlaylists();
        }

        private void searchPage1_Load(object sender, EventArgs e)
        {
        }

        private void SearchPanel_Paint(object sender, PaintEventArgs e)
        {
        }


        private void MainWindow_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void LayoutPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                AddPlaylistStrip.Show(LayoutPanel.PointToScreen(e.Location));
        }

        private void downloadAndPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void downloadAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void downloadOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ResultsPage_Load(object sender, EventArgs e)
        {
        }

        private void LayoutPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void playControl1_Load(object sender, EventArgs e)
        {
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RenameSelectedItem();
        }

        private void MainWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void searchBarPage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void searchBarPage_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void abortOperationToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void convertToMp3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertToMp3();
        }

        public static void ConvertToMp3()
        {
            if (Instance.MainTab.MainTab.SelectedTab is MusicPage page)
                page.musicTab.ConvertToMp3();
        }
    }
}