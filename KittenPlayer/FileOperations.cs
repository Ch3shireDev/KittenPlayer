using System;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainWindow : Form
    {

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
            SavePlaylists();
        }


        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                MusicPage CurrentTab = MainTabs.SelectedTab as MusicPage;
                CurrentTab.musicTab.AddTrack(folderBrowserDialog.SelectedPath);
            }
            SavePlaylists();
        }
    }
}
