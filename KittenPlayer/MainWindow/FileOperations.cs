using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainWindow : Form
    {
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var CurrentTab = MainTab.MainTab.SelectedTab as MusicPage;
                foreach (var str in openFileDialog.FileNames)
                    Debug.WriteLine(str);
                CurrentTab.musicTab.AddTrack(openFileDialog.FileNames);
            }
            SavePlaylists();
        }

        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = false;
            var result = folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            //{
            var CurrentTab = MainTab.MainTab.SelectedTab as MusicPage;

            string[] FileNames = {folderBrowserDialog.SelectedPath};

            //    Debug.WriteLine(folderBrowserDialog.SelectedPath);

            //    CurrentTab.musicTab.AddTrack(FileNames);
            //}
            //SavePlaylists();

            var trackList = MusicTab.MakeTracksList(FileNames);
            CurrentTab?.musicTab?.AddTrack(trackList);
            SavePlaylists();
        }
    }
}