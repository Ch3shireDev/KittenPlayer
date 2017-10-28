using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MusicPage CurrentTab = MainTab.MainTab.SelectedTab as MusicPage;
                foreach(string str in openFileDialog.FileNames)
                {
                    Debug.WriteLine(str);
                }
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
            if (result != DialogResult.OK) return;
            //{
            MusicPage CurrentTab = MainTab.MainTab.SelectedTab as MusicPage;

            string[] FileNames = new string[] { folderBrowserDialog.SelectedPath };

            //    Debug.WriteLine(folderBrowserDialog.SelectedPath);

            //    CurrentTab.musicTab.AddTrack(FileNames);
            //}
            //SavePlaylists();


            List<Track> trackList = MusicTab.MakeTracksList(FileNames);
            CurrentTab?.musicTab?.AddTrack(trackList);
            MainWindow.SavePlaylists();
        }
        
    }
}
