using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
        private int DropIndex;

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
            var tracksList = new List<Track>();

            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                var ItemsList = e.Data.GetData(typeof(List<ListViewItem>)) as List<ListViewItem>;
                tracksList = MakeTracksList(ItemsList);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var FilesArray = e.Data.GetData(DataFormats.FileDrop, false) as string[];
                tracksList = MakeTracksList(FilesArray);
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                var Html = e.Data.GetData(DataFormats.Text) as string;
                tracksList = MakeTracksList(Html);
            }
            else if (e.Data.GetDataPresent(typeof(List<Thumbnail>)))
            {
                var thumbnails = e.Data.GetData(typeof(List<Thumbnail>)) as List<Thumbnail>;
                tracksList = DropThumbnail(thumbnails);
                MainWindow.Instance.ResultsPage.DeselectAll();
            }

            AddTrack(tracksList, DropIndex);
            Refresh();
            MainWindow.SavePlaylists();
        }

        public List<Track> DropThumbnail(List<Thumbnail> thumbnails)
        {
            var output = new List<Track>();

            foreach (var thumbnail in thumbnails)
            {
                var tracksList = new List<Track>();

                if (string.IsNullOrEmpty(thumbnail.Playlist))
                {
                    tracksList = MakeTracksList("v=" + thumbnail.ID);
                    foreach (var track in tracksList)
                    {
                        //track.Duration = "00:00";
                        track.Title = thumbnail.Title;
                        track.MusicTab = this;
                    }
                }
                else
                {
                    tracksList = MakeTracksList("list=" + thumbnail.Playlist);
                }

                output.AddRange(tracksList);
            }
            return output;
        }

        private void PlaylistView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else if (e.Data.GetDataPresent(DataFormats.Html))
                e.Effect = DragDropEffects.Link;
            else if (e.Data.GetDataPresent(typeof(List<Thumbnail>)))
                e.Effect = DragDropEffects.All;
            else e.Effect = DragDropEffects.None;
        }

        public static bool IsDirectory(string path)
        {
            var attr = File.GetAttributes(path);
            var FileDir = attr & FileAttributes.Directory;
            var isDirectory = FileDir == FileAttributes.Directory;
            return isDirectory;
        }

        public static bool IsMusicFile(string Path)
        {
            var Extensions = new List<string> { ".mp3", ".m4a" };
            if (IsDirectory(Path)) return false;
            foreach (var extension in Extensions)
                if (Path.EndsWith(extension, false, null)) return true;
            return false;
        }

        public static List<string> GetAllTracksFromFile(List<string> FilesArray)
        {
            FilesArray.Sort();
            var FilesToAdd = new List<string>();
            var NewList = new List<string>();
            foreach (var Path in FilesArray)
            {
                if (IsDirectory(Path))
                {
                    var FilesTab = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
                    foreach (var file in FilesTab)
                        if (IsMusicFile(file)) FilesToAdd.Add(file);
                }
                if (IsMusicFile(Path)) NewList.Add(Path);
            }
            NewList.AddRange(FilesToAdd);
            return NewList;
        }

        public static List<Track> MakeTracksList(List<string> FileList)
        {
            var Tracks = new List<Track>();
            foreach (var file in FileList)
                Tracks.Add(new Track(file));
            return Tracks;
        }

        public static List<Track> MakeTracksList(string[] FilesArray)
        {
            var Array = GetAllTracksFromFile(new List<string>(FilesArray));
            return MakeTracksList(Array);
        }

        public List<Track> MakeTracksList(string URL)
        {
            var Array = new List<Track>();

            var GroupID = Regex.Match(URL, @"v=([^&]*)").Groups;
            var GroupPlaylist = Regex.Match(URL, @"list=([^&]*)").Groups;
            var GroupUser = Regex.Match(URL, @"/user/([^/]*)/").Groups;

            var IsTrack = GroupID.Count > 1;
            var IsPlaylist = GroupPlaylist.Count > 1;
            var IsUser = GroupUser.Count > 1;

            if (IsTrack)
            {
                var YoutubeID = GroupID[1].Value;
                var track = new Track("", YoutubeID);
                track.MusicTab = this;
                Array.Add(track);
            }
            else if (IsPlaylist)
            {
                var Playlist = GroupPlaylist[1].Value;
                var youtube = new YoutubeDL(Playlist);
                Array.AddRange(youtube.GetData());
            }
            else if (IsUser)
            {
                var User = GroupUser[1].Value;
                var youtube = new YoutubeDL("ytuser:" + User);
                Array.AddRange(youtube.GetData());
            }

            return Array;
        }

        private List<Track> MakeTracksList(List<ListViewItem> Items)
        {
            var TracksList = new List<Track>();

            foreach (var item in Items)
            {
                var Position = PlaylistView.Items.IndexOf(item);
                PlaylistView.Items.Remove(item);
                var track = Tracks[Position];
                TracksList.Add(track);
                Tracks.Remove(track);
            }

            return TracksList;
        }

        private void PlaylistView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
                e.Effect = DragDropEffects.Move;
            var N = PlaylistView.Items.Count;
            if (N == 0)
            {
                DropIndex = 0;
                Application.DoEvents();
                return;
            }

            var mLoc = PlaylistView.PointToClient(Cursor.Position);

            var r0 = PlaylistView.Items[0].Bounds;
            var r1 = PlaylistView.Items[N - 1].Bounds;

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
                var idx = hitt.Item.Index;
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