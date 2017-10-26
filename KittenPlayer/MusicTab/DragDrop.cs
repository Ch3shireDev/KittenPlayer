using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace KittenPlayer
{

    public partial class MusicTab : UserControl
    {

        private void PlaylistView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = new List<ListViewItem>();
            items.Add((ListViewItem)e.Item);
            foreach (ListViewItem lvi in PlaylistView.SelectedItems)
                if (!items.Contains(lvi))
                    items.Add(lvi);
            PlaylistView.DoDragDrop(items, DragDropEffects.Move);
        }
        
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
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string Html = e.Data.GetData(DataFormats.Text) as string;
                tracksList = MakeTracksList(Html);
            }
            else if (e.Data.GetDataPresent(typeof(Thumbnail)))
            {
                Thumbnail thumbnail = e.Data.GetData(typeof(Thumbnail)) as Thumbnail;
                if (thumbnail.Playlist == "")
                {
                    tracksList = MakeTracksList("v=" + thumbnail.ID);
                    foreach(Track track in tracksList)
                        track.Title = thumbnail.Title;
                }
                else tracksList = MakeTracksList("list=" + thumbnail.Playlist);
            }

            AddTrack(tracksList, DropIndex);
            Refresh();
            MainWindow.SavePlaylists();
        }

        private void PlaylistView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else if (e.Data.GetDataPresent(DataFormats.Html))
                e.Effect = DragDropEffects.Link;
            else if (e.Data.GetDataPresent(typeof(Thumbnail)))
                e.Effect = DragDropEffects.All;
            else e.Effect = DragDropEffects.None;
            
        }

        public static bool IsDirectory(String path)
        {
            FileAttributes attr = File.GetAttributes(path);
            FileAttributes FileDir = attr & FileAttributes.Directory;
            bool isDirectory = (FileDir == FileAttributes.Directory);
            return isDirectory;
        }

        public static bool IsMusicFile(String Path)
        {
            List<String> Extensions = new List<String> { ".mp3", ".m4a" };
            if (IsDirectory(Path)) return false;
            foreach (String extension in Extensions)
                if (Path.EndsWith(extension, false, null)) return true;
            return false;
        }

        public static List<String> GetAllTracksFromFile(List<String> FilesArray)
        {
            FilesArray.Sort();
            List<String> FilesToAdd = new List<String>();
            List<String> NewList = new List<String>();
            foreach (String Path in FilesArray)
            {
                if (IsDirectory(Path))
                {
                    string[] FilesTab = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
                    foreach (string file in FilesTab)
                        if (IsMusicFile(file))
                            FilesToAdd.Add(file);
                }
                if (IsMusicFile(Path)) NewList.Add(Path);
            }
            NewList.AddRange(FilesToAdd);
            return NewList;
        }

        public static List<Track> MakeTracksList(List<String> FileList)
        {
            List<Track> Tracks = new List<Track>();
            foreach (String file in FileList)
                Tracks.Add(new Track(file));
            return Tracks;
        }

        public static List<Track> MakeTracksList(string[] FilesArray)
        {
            List<String> Array = GetAllTracksFromFile(new List<String>(FilesArray));
            return MakeTracksList(Array);
        }

        public static List<Track> MakeTracksList(string URL)
        {
            List<Track> Array = new List<Track>();

            var GroupID = Regex.Match(URL, @"v=([^&]*)").Groups;
            var GroupPlaylist = Regex.Match(URL, @"list=([^&]*)").Groups;
            var GroupUser = Regex.Match(URL, @"/user/([^/]*)/").Groups;

            bool IsTrack = GroupID.Count > 1;
            bool IsPlaylist = GroupPlaylist.Count > 1;
            bool IsUser = GroupUser.Count > 1;

            if (IsTrack)
            {
                String YoutubeID = GroupID[1].Value;
                Track track = new Track("", YoutubeID);
                Array.Add(track);
            }
            else if (IsPlaylist)
            {
                String Playlist = GroupPlaylist[1].Value;
                YoutubeDL youtube = new YoutubeDL(Playlist);
                Array.AddRange(youtube.GetData());
            }
            else if (IsUser)
            {
                String User = GroupUser[1].Value;
                YoutubeDL youtube = new YoutubeDL("ytuser:" + User);
                Array.AddRange(youtube.GetData());
            }

            return Array;
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
                e.Effect = DragDropEffects.Move;
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
