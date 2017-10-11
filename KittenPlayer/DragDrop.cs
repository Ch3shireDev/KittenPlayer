using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace KittenPlayer
{

    public partial class MusicTab : UserControl
    {

        private void PlaylistView_DragDrop(object sender, DragEventArgs e)
        {
            List<Track> tracksList = new List<Track>();

            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                List<ListViewItem> ItemsList = e.Data.GetData(typeof(List<ListViewItem>)) as List<ListViewItem>;
                tracksList = MakeTracksList(ItemsList);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] FilesArray = e.Data.GetData(DataFormats.FileDrop, false) as string[];
                tracksList = MakeTracksList(FilesArray);
            }

            AddTracks(tracksList, DropIndex);
            Refresh();
            MainWindow mainWindow = Application.OpenForms[0] as MainWindow;
            mainWindow.SavePlaylists();
        }

        void AddTracks(List<Track> Tracks, int DropIndex)
        {
            for(int i = 0; i < Tracks.Count; i++)
            {
                AddTrack(Tracks[i], DropIndex + i);
            }
        }

        bool IsDirectory(String Path)
        {
            FileAttributes attr = File.GetAttributes(Path);
            System.IO.FileAttributes FileDir = attr & FileAttributes.Directory;
            bool isDirectory = (FileDir == FileAttributes.Directory);
            return isDirectory;
        }

        bool IsMusicFile(String Path)
        {
            List<String> Extensions = new List<String> { ".mp3" };
            if (IsDirectory(Path))
            {
                return false;
            }
            foreach (String extension in Extensions)
            {
                if (Path.EndsWith(extension, false, null))
                {
                    return true;
                }
            }
            return false;
        }

        List<String> GetAllTracksFromFile(List<String> FilesArray)
        {
            FilesArray.Sort();

            List<String> FilesToAdd = new List<String>();

            List<String> NewList = new List<String>();

            foreach (String Path in FilesArray)
            {
                if (IsDirectory(Path))
                {
                    string[] FilesTab = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);

                    foreach(string file in FilesTab)
                    {
                        if (IsMusicFile(file))
                        {
                            FilesToAdd.Add(file);
                        }
                    }

                }
                if (IsMusicFile(Path))
                {
                    NewList.Add(Path);
                }
            }

            NewList.AddRange(FilesToAdd);
            return NewList;
        }

        List<Track> MakeTracksList(string[] FilesArray)
        {
            List<String> Array = GetAllTracksFromFile(new List<String>(FilesArray));

            List<Track> Tracks = new List<Track>();

            foreach (String file in Array)
            {
                Tracks.Add(new Track(file));
            }

            return Tracks;
        }

        List<Track> MakeTracksList(List<ListViewItem> Items)
        {
            List<Track> TracksList = new List<Track>();

            foreach (ListViewItem item in Items)
            {
                int Position = PlaylistView.Items.IndexOf(item);
                PlaylistView.Items.Remove(item);
                Track track = Tracks[Position];
                TracksList.Add(track);
                Tracks.Remove(track);
            }

            return TracksList;
        }

        int DropIndex = 0;

        private void PlaylistView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                e.Effect = DragDropEffects.Move;
            }
            
            int N = PlaylistView.Items.Count;

            if (N == 0)
            {
                DropIndex = 0;
                Application.DoEvents();
                return;
            }

            Point mLoc = PlaylistView.PointToClient(Cursor.Position);
            
            Rectangle r0 = PlaylistView.Items[0].Bounds;
            Rectangle r1 = PlaylistView.Items[N-1].Bounds;

            var hitt = PlaylistView.HitTest(mLoc);

            if (hitt.Item == null)
            {

                if (mLoc.Y < r0.Y)
                {
                    PlaylistView.InsertionMark.AppearsAfterItem = false;
                    PlaylistView.InsertionMark.Index = 0;
                    DropIndex = 0;
                }
                else if (mLoc.Y > r1.Y)
                {
                    PlaylistView.InsertionMark.AppearsAfterItem = true;
                    PlaylistView.InsertionMark.Index = N - 1;
                    DropIndex = N;
                }
            }
            else
            {
                int idx = hitt.Item.Index;
                PlaylistView.InsertionMark.Index = idx;

                if (mLoc.Y < r0.Y)
                {
                    PlaylistView.InsertionMark.AppearsAfterItem = false;
                    DropIndex = 0;
                }
                else
                {
                    PlaylistView.InsertionMark.AppearsAfterItem = true;
                    DropIndex = idx + 1;
                }
                if (idx == prevItem) return;
            }
            Application.DoEvents();

        }

        private void PlaylistView_DragLeave(object sender, EventArgs e)
        {
            PlaylistView.InsertionMark.Index = -1;
        }
    }
}
