using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace KittenPlayer
{

    public partial class MainWindow : Form, IKittenInterface
    {
        [STAThread]
        static void Main()
        {
            TextWriterTraceListener[] listeners = new TextWriterTraceListener[] 
            {
                new TextWriterTraceListener("log.txt"),
                new TextWriterTraceListener(Console.Out)
            };
            Debug.Listeners.AddRange(listeners);
            Debug.WriteLine("Some Value", "Some Category");
            Debug.Flush();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        static MusicPage GetActivePage()
        {
            if (Instance.MainTab.MainTab.Controls.Count == 0)
            {
                Instance.MainTab.AddNewTab("New Tab");
            }
            int Index = Instance.MainTab.MainTab.SelectedIndex;
            return Instance.MainTab.MainTab.Controls[Index] as MusicPage;
        }

        public static MusicPage ActivePage => GetActivePage();
        public static MusicTab ActiveTab => ActivePage?.musicTab;

        static MainWindow instance;
        public static MainWindow Instance => instance;

        MusicPlayer musicPlayer = MusicPlayer.Instance;
        ActionsControl actionsControl = ActionsControl.GetInstance();
        public Options Options = new Options();
        
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            InitializeTrackbarTimer();
        }

        public static void SavePlaylists()
        {
            LocalData.Instance.SavePlaylists(MainTabs.Instance);
        }
        
        private void MainWindow_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void MainWindow_DoubleClick(object sender, EventArgs e)
        {
        }

        private void ContextTab_Opening(object sender, CancelEventArgs e)
        {

        }

        RenameBox renameBox = null;
        
        /// <summary>
        /// On renaming action TextBox appears in exact place of original playlist name.
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
            int TabNum = MainTab.MainTab.SelectedIndex;
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
        
        public String GetSelectedTrackPath()
        {
            int TabIndex = MainTab.MainTab.SelectedIndex;
            MusicPage musicPage = MainTab.Controls[TabIndex] as MusicPage;
            String Path = musicPage.GetSelectedTrackPath();
            return Path;
        }

        private void youTubeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPage currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            currentPage.DeleteSelectedTracks();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPage currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            if (currentPage == null) return;
            if (currentPage.musicTab == null) return;
            if (currentPage.musicTab.PlaylistView == null) return;
            if (currentPage.musicTab.PlaylistView.Focused)
            {
                currentPage.SelectAll();
            }
            else if (MainWindow.Instance.searchBarPage.searchBar.Focused)
            {
                MainWindow.Instance.searchBarPage.searchBar.SelectAll();
            }
        }

        private void addYoutubePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPlaylistForm form = new AddPlaylistForm();
            form.ShowDialog();
            List<Track> Tracks = form.Tracks;
            MusicPage currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            currentPage.musicTab.AddTrack(Tracks);
            MainWindow.SavePlaylists();

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

        Form About = null;

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

        private void MainWindow_Load(object sender, EventArgs e)
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
            if(e.Button == MouseButtons.Right)
            {
                AddPlaylistStrip.Show(LayoutPanel.PointToScreen(e.Location));
            }
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
            {
                page.musicTab.ConvertToMp3();
            }
        }
    }
}
