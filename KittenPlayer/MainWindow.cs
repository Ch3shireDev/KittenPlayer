using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace KittenPlayer
{

    /// <summary>
    /// Main window of the program.
    /// </summary>

    public partial class MainWindow : Form
    {

        MusicPlayer musicPlayer = MusicPlayer.GetInstance();
        LocalData localData = LocalData.NewLocalData();
        ActionsControl actionsControl = ActionsControl.NewActionsControl();


        public MainWindow()
        {
            InitializeComponent();

            MusicPage MainPage = new MusicPage("New Tab");

            MainTabs.Controls.Add(MainPage);
            MainTabs.Controls[0].Dock = DockStyle.Fill;
            

        }

        const String Directory = "./";

        private void SavePlaylist(Control PlaylistTab, int Index)
        {
            String Name = Directory + Index;

            //PlaylistBox playlistBox = PlaylistTab.Controls[0] as PlaylistBox;
            //playlistBox.SaveToFile(Name, PlaylistTab.Text);
        }

        private void SaveAllPlaylists()
        {
            for(int i = 0; i < MainTabs.Controls.Count; i++)
            {
                var playlistTab = MainTabs.Controls[i];
                SavePlaylist(playlistTab, i);
            }
        }

        private void LoadAllPlaylists()
        {
            //MainTabs.Controls.Clear();
            int Index = 0;
            String Name = Directory + Index;
            while (File.Exists(Name))
            {
                LoadPlaylist(Name);
                Index++;
                Name = Directory + Index;
            }
        }

        private void LoadPlaylist(String Name)
        {
            var tabPage = AddNewTab("");
            //PlaylistBox playlist = tabPage.Controls[0] as PlaylistBox;
            //String Title;
            //playlist.LoadFromFile(Name, out Title);
            //tabPage.Text = Title;
        }
        
        
        private void MainWindow_Click(object sender, EventArgs e)
        {
            this.Focus();
            Debug.WriteLine("Focus!");
        }

        private void MainWindow_DoubleClick(object sender, EventArgs e)
        {
            //AddNewTab("NewTab");
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
                foreach (String s in openFileDialog.FileNames)
                {
                    CurrentTab.AddTrack(s);
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void addNewPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddNewTab("New Tab");
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

    }
}
