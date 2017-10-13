using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace KittenPlayer
{

    /// <summary>
    /// Main window of the program.
    /// </summary>

    public partial class MainWindow : Form
    {

        MusicPlayer musicPlayer = MusicPlayer.GetInstance();
        LocalData localData = LocalData.GetInstance();
        ActionsControl actionsControl = ActionsControl.GetInstance();


        public MainWindow()
        {
            InitializeComponent();
            InitializeTrackbarTimer();

            if (localData.Num() > 0)
            {
                localData.LoadPlaylists(MainTabs);
                
            }
            else
            {
                MusicPage MainPage = new MusicPage("New Tab");
                MainTabs.Controls.Add(MainPage);
                MainTabs.Controls[0].Dock = DockStyle.Fill;
            }
        }
       
        public void SavePlaylists()
        {
            localData.SavePlaylists(MainTabs);
        }
        
        private void MainWindow_Click(object sender, EventArgs e)
        {
            this.Focus();
            
        }

        private void MainWindow_DoubleClick(object sender, EventArgs e)
        {
            AddNewTabAndRename();
        }

        private void ContextTab_Opening(object sender, CancelEventArgs e)
        {

        }

        RenameBox renameBox = null;
        
        /// <summary>
        /// On renaming action TextBox appears in exact place of original playlist name.
        /// </summary>

        private void RenameTab()
        {
            
            renameBox = new RenameBox(MainTabs);

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameTab();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MusicPage CurrentTab = MainTabs.SelectedTab as MusicPage;
                CurrentTab.musicTab.AddTrack(openFileDialog.FileNames);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void addNewPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTabAndRename();
        }

        private void deletePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TabNum = MainTabs.SelectedIndex;
            MainTabs.Controls.RemoveAt(MainTabs.SelectedIndex);
        }



        private void downloadLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YouTubeForm form = new YouTubeForm();
            form.ShowDialog();
        }

        private void findTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YouTubeSearch search = new YouTubeSearch();
            search.ShowDialog();
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
            int TabIndex = MainTabs.SelectedIndex;
            MusicPage musicPage = MainTabs.Controls[TabIndex] as MusicPage;
            String Path = musicPage.GetSelectedTrackPath();
            return Path;
        }

        private void youTubeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPage currentPage = MainTabs.SelectedTab as MusicPage;
            currentPage.DeleteSelectedTracks();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPage currentPage = MainTabs.SelectedTab as MusicPage;
            if (currentPage.musicTab.PlaylistView.Focused)
            {
                currentPage.SelectAll();
            }
        }

        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if(result == DialogResult.OK)
            {

            }
        }
    }
}
