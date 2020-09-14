using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl, IKittenInterface
    {
        private int RenameIndex;

        public void RenameSelectedItem()
        {
            if (ActiveControl is ListView listView)
            {
                new RenameBox(listView, 0);
            }
            else if (ActiveControl is RenameBox renameBox)
            {
                renameBox.AcceptChange();
                RenameIndex++;
                RenameIndex %= 4;
                new RenameBox(PlaylistView, RenameIndex);
            }
        }

        public void RemoveSelectedTracks()
        {
            var SelectedIndices = new List<int>();
            foreach (int n in PlaylistView.SelectedIndices)
                SelectedIndices.Add(n);
            RemoveTrack(SelectedIndices);
            MainWindow.SavePlaylists();
        }

        public void RemoveTrack(List<int> Positions)
        {
            Positions.Sort();
            for (var i = 0; i < Positions.Count; i++)
                RemoveTrack(Positions[i] - i);
        }

        public void RemoveTrack(int Position)
        {
            if (Enumerable.Range(0, Tracks.Count).Contains(Position))
            {
                Tracks.RemoveAt(Position);
                PlaylistView.Items.RemoveAt(Position);
            }

            //Refresh();
        }

        public void PlaySelected()
        {
            if (PlaylistView.SelectedIndices.Count == 0) return;
            musicPlayer.CurrentTab = this;
            var Index = PlaylistView.SelectedIndices[0];
            Play(Index);
        }

        private void PlaylistView_DoubleClick(object sender, EventArgs e)
        {
            PlaySelected();
        }

        public void PlaySelectedTrack()
        {
            PlaySelected();
        }

        private void MusicTab_DoubleClick(object sender, EventArgs e)
        {
            Play(PlaylistView.TabIndex);
        }

        public async Task Play(Track track)
        {
            if (track == null) MainWindow.Instance.SetDefaultTitle();
#if DEBUG
            DownloadTrack(track);
#else
            await DownloadTrack(track);
#endif
            MainWindow.SavePlaylists();

            musicPlayer.CurrentTab = this;
            musicPlayer.CurrentTrack = track;
            musicPlayer.Stop();
            musicPlayer.Load(track);

            musicPlayer.Play();
        }

        public async Task Play(int Index)
        {
            if (Index >= Tracks.Count || Index < 0) return;
            var track = Tracks[Index];
#if DEBUG
            Play(track);
#else
            await Play(track);
#endif
        }

        private static void ProcessDir(string Dir)
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
        }

        public void MoveTrackToArtistAlbumDir(Track track)
        {
            if (!track.IsOffline) return;
            if (track.IsPlaying) return;
            var DefaultDir = MainWindow.Instance.Options.DefaultDirectory;

            foreach (var str in new[] {track.Artist, track.Album})
            {
                if (string.IsNullOrWhiteSpace(str)) continue;
                DefaultDir += "\\" + str;
                ProcessDir(DefaultDir);
            }

            var newPath = DefaultDir + "\\" + track.Title + Path.GetExtension(track.filePath);
            if (string.Compare(track.filePath, newPath, true) == 0) return;
            if (File.Exists(newPath)) File.Delete(newPath);
            File.Copy(track.filePath, newPath);
            if (File.Exists(newPath))
            {
                track.filePath = newPath;
                File.Delete(track.filePath);
            }
        }

        internal void ConvertToMp3()
        {
            if (PlaylistView.SelectedIndices.Count == 0) return;
            var track = Tracks[PlaylistView.SelectedIndices[0]];
            track.ConvertToMp3();
        }
        //public void AddTrack(String filePath, String fileName = "", int Position = -1)
        //{
        //    if (File.Exists(filePath)) return;
        //    if (IsDirectory(filePath))
        //    {
        //        string[] array = new string[] { filePath };
        //        AddTrack(array, Position);
        //    }
        //    else
        //    {
        //        Track track = new Track(filePath, fileName);
        //        AddTrack(track, Position);
        //    }
        //}

        public void AddTrack(Track track, int Position = -1)
        {
            var item = track.GetListViewItem(PlaylistView);
            track.MusicTab = this;

            if (Position >= 0 && Position < PlaylistView.Items.Count)
            {
                Tracks.Insert(Position, track);
                PlaylistView.Items.Insert(Position, item);
            }
            else
            {
                Tracks.Add(track);
                PlaylistView.Items.Add(item);
            }
        }

        public void AddTrack(string[] fileNames, int Position = -1)
        {
            var fileList = new List<string>(fileNames);
            fileList.Sort();
            AddTrack(fileList, Position);
        }

        public void AddTrack(List<string> fileList, int Position = -1)
        {
            var Tracks = MakeTracksList(fileList);
            AddTrack(Tracks, Position);
        }

        public void AddTrack(List<Track> Tracks, int Position = -1)
        {
            if (Position >= 0 && Position < PlaylistView.Items.Count)
                for (var i = 0; i < Tracks.Count; i++)
                    AddTrack(Tracks[i], Position + i);
            else
                foreach (var track in Tracks)
                    AddTrack(track);
        }

        public List<Track> AddDirectory(List<string> FilesArray)
        {
            FilesArray.Sort();
            var NewList = new List<string>();
            var FilesToAdd = new List<string>();
            foreach (var Path in FilesArray)
                if (IsDirectory(Path))
                {
                    var FilesTab = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
                    foreach (var file in FilesTab)
                        if (IsMusicFile(file))
                            FilesToAdd.Add(file);
                }
                else if (IsMusicFile(Path))
                {
                    NewList.Add(Path);
                }

            NewList.AddRange(FilesToAdd);

            var Tracks = new List<Track>();

            foreach (var file in NewList)
            {
                var track = new Track(file);
                if (track.IsValid())
                    Tracks.Add(track);
            }

            return Tracks;
        }

        private static int index;

        public List<Track> Tracks = new List<Track>();
        private readonly MusicPlayer musicPlayer = MusicPlayer.Instance;
        public int Index;

        private static MusicTab SelectedTab => (MainTabs.Instance.SelectedTab as MusicPage).musicTab;

        public static class Names
        {
            public const string Play = "Play";
            public const string Pause = "Pause";
            public const string Stop = "Stop";
            public const string JDownload = "Just Download";
            public const string DAgain = "Download again";
            public const string DP = "Download and Play";
            public const string Title = "Retrieve Online Title";
            public const string Numbers = "Assign Track Numbers";
            public const string Dir = "Set Directory to /[Artist]/[Album]/...";
            public const string ConvertMP3 = "Convert to Mp3";
        }

        private readonly List<ToolStripMenuItem> MenuItems = new List<ToolStripMenuItem>
        {
            new ToolStripMenuItem(Names.Play, null, (sender, e) => { MusicPlayer.Instance.PlayAutomatic(); }),
            new ToolStripMenuItem(Names.Pause, null, (sender, e) => { MusicPlayer.Instance.Pause(); }),
            new ToolStripMenuItem(Names.Stop, null, (sender, e) => { MusicPlayer.Instance.Stop(); }),
            new ToolStripMenuItem(Names.JDownload, null, (sender, e) => { DownloadOnly(); }),
            new ToolStripMenuItem(Names.DAgain, null, (sender, e) => { DownloadAgain(); }),
            new ToolStripMenuItem(Names.DP, null, (sender, e) => { DownloadAndPlay(); }),
            new ToolStripMenuItem(Names.Title, null, (sender, e) => { GetOnlineTitles(); }),
            new ToolStripMenuItem(Names.Numbers, null, (sender, e) => { AssignTrackNumbers(); }),
            new ToolStripMenuItem(Names.Dir, null, (sender, e) => { SetDirToArtistAlbum(); }),
            new ToolStripMenuItem(Names.ConvertMP3, null, (sender, e) => { ConvertToMp3_Static(); })
        };

        //in this function you can technically call it for all tracks in playlist. Player hangs itself after that call. I need to add the limit.

        private static void ConvertToMp3_Static()
        {
            MainWindow.ConvertToMp3();
        }

        private static void DownloadAndPlay()
        {
            DownloadManager.PlayAfterDownload(GetSelectedTracks());
        }

        private static void DownloadAgain()
        {
            DownloadManager.DownloadAgain(GetSelectedTracks());
        }

        private static void DownloadOnly()
        {
            DownloadManager.JustDownload(GetSelectedTracks());
        }

        private static void SetDirToArtistAlbum()
        {
            foreach (var track in SelectedTracks)
            {
                if (track.IsPlaying) continue;
                track.MusicTab.MoveTrackToArtistAlbumDir(track);
            }
        }

        private static void AssignTrackNumbers()
        {
            foreach (var track in GetSelectedTracks())
            {
                var PlaylistView = track.Item.ListView;
                var Index = PlaylistView.Items.IndexOf(track.Item) + 1;
                track.Number = Index.ToString();
                track.Item.SubItems[3].Text = track.Number;
            }
        }

        private static void GetOnlineTitles()
        {
            foreach (var track in GetSelectedTracks())
                RequestOnlineTitle(track);
        }

        private static List<Track> SelectedTracks => GetSelectedTracks();

        public static List<Track> GetSelectedTracks()
        {
            var Output = new List<Track>();
            var Index = MainTabs.Instance.SelectedIndex;
            var Page = MainTabs.Instance.SelectedTab as MusicPage;
            var musicTab = Page.musicTab;
            ListView PlaylistView = musicTab.PlaylistView;
            foreach (int ItemIndex in PlaylistView.SelectedIndices)
            {
                var track = musicTab.Tracks[ItemIndex];
                track.Item = PlaylistView.Items[ItemIndex];
                Output.Add(track);
            }

            return Output;
        }

#if DEBUG

        public static void RequestOnlineTitle(Track track)
#else
        public static async void RequestOnlineTitle(Track track)
#endif
        {
#if DEBUG
            track.GetOnlineTitle();
#else
            await track.GetOnlineTitle();
#endif
        }

        public MusicTab()
        {
            Index = index++;
            InitializeComponent();
            InitPlaylistProperties();
            LoadColumns();
        }

        public void SelectTrack(Track track)
        {
            PlaylistView.SelectedIndices.Clear();
            var Index = Tracks.IndexOf(track);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index))
                PlaylistView.SelectedIndices.Add(Index);
        }

        public Track GetNextTrack(Track Current)
        {
            Debug.WriteLine(PlayControl.RepeatType);
            if (PlayControl.RepeatType is PlayControl.ERepeatType.RepeatOne)
                return Current;
            var Index = Tracks.IndexOf(Current) + 1;
            if (Index == Tracks.Count)
                if (PlayControl.RepeatType is PlayControl.ERepeatType.RepeatAll)
                    return Tracks[0];
                else return null;
            return Tracks[Index];
        }

        public Track GetPreviousTrack(Track Current)
        {
            var Index = Tracks.IndexOf(Current);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index - 1))
            {
                Index--;
                return Tracks[Index];
            }

            return null;
        }

        public string GetSelectedTrackPath()
        {
            if (PlaylistView.SelectedIndices.Count == 0)
                return "";
            var Index = PlaylistView.SelectedIndices[0];
            return Tracks[Index].filePath;
        }

        private readonly int prevItem = -1;

        private void MusicTab_Click(object sender, EventArgs e)
        {
        }

        private void PlaylistView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PlaylistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                PlaySelectedTrack();
            }
            else if (e.KeyChar == (char) Keys.Space)
            {
                var player = MusicPlayer.Instance;
                if (player.IsPlaying) MusicPlayer.Instance.Pause();
            }
        }

        private void PlayClick(object sender, EventArgs e)
        {
            PlaySelectedTrack();
            DropDownMenu.Hide();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.Instance.Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.Instance.Stop();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedTracks();
        }

        private void PlaylistView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedTracks();
            }
            else if (e.KeyCode == Keys.F1)
            {
                PlaylistProperties.Show(Cursor.Position);
                PlaylistProperties.Focus();
            }
        }

        public void RefreshPlaylistView()
        {
        }

        public override void Refresh()
        {
            base.Refresh();
            RefreshPlaylistView();
            MainWindow.SavePlaylists();
        }

        public void SelectAll()
        {
            foreach (ListViewItem Item in PlaylistView.Items)
                Item.Selected = true;
        }

        private void PlaylistView_Leave(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in PlaylistView.Items)
                Item.Selected = false;
        }

        private void PlaylistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }

        private void PlaylistView_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void PlaylistView_MouseEnter(object sender, EventArgs e)
        {
        }

        private void PlaylistView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (PlaylistView.SelectedIndices.Count == 0) return;
                var Index = GetFocusedItem();
                var track = Tracks[Index];

                var Items = new Dictionary<string, ToolStripItem>();
                foreach (var item in MenuItems)
                {
                    DropDownMenu.Items.Remove(item);
                    Items.Add(item.Text, item);
                }

                if (track.Status == Track.StatusType.Local)
                {
                    DropDownMenu.Items.Insert(0, Items[Names.Play]);
                    DropDownMenu.Items.Insert(1, Items[Names.Pause]);
                    DropDownMenu.Items.Insert(2, Items[Names.Stop]);
                    DropDownMenu.Items.Insert(3, Items[Names.Numbers]);
                    if (track.IsM4a) DropDownMenu.Items.Insert(4, Items[Names.ConvertMP3]);
                }
                else if (track.Status == Track.StatusType.Offline)
                {
                    DropDownMenu.Items.Insert(0, Items[Names.Play]);
                    DropDownMenu.Items.Insert(1, Items[Names.Title]);
                    DropDownMenu.Items.Insert(2, Items[Names.DAgain]);
                    DropDownMenu.Items.Insert(3, Items[Names.Numbers]);
                    DropDownMenu.Items.Insert(4, Items[Names.Dir]);
                    if (track.IsM4a) DropDownMenu.Items.Insert(2, Items[Names.ConvertMP3]);
                }
                else if (track.Status == Track.StatusType.Online)
                {
                    DropDownMenu.Items.Insert(0, Items[Names.DP]);
                    DropDownMenu.Items.Insert(1, Items[Names.Title]);
                    DropDownMenu.Items.Insert(2, Items[Names.JDownload]);
                    DropDownMenu.Items.Insert(3, Items[Names.Numbers]);
                }

                var font = DropDownMenu.Items[0].Font;
                DropDownMenu.Items[0].Font = new Font(font.FontFamily, font.Size, FontStyle.Bold);
                DropDownMenu.Show(PlaylistView.PointToScreen(e.Location));
            }
        }

        private void changePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void downloadAgainToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void MusicTab_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void PlaylistView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            SaveColumns();
        }

        private void SaveColumns()
        {
            LocalData.Instance.SaveColumns(PlaylistView);
        }

        private void LoadColumns()
        {
            LocalData.Instance.LoadColumns(ref PlaylistView);
        }

#if DEBUG

        public void DownloadTrack(Track track) =>
#else
        public async Task DownloadTrack(Track track) =>
#endif
#if DEBUG
            YoutubeDL.DownloadTrack(track);

#else
            await YoutubeDL.DownloadTrack(track);

#endif


        private readonly List<string> Parameters = new List<string>
        {
            "Artist",
            "Album",
            "Title"
        };

        private void InitPlaylistProperties()
        {
            PlaylistProperties.Items.Clear();
            foreach (var Parameter in Parameters)
                PlaylistProperties.Items.Add(Parameter);
            foreach (ToolStripMenuItem Item in PlaylistProperties.Items)
                Item.CheckOnClick = true;

            ChangeTitleToolStripMenuItem.Click += (x, y) => { ChangeSelectedProperty(0); };
            ChangeArtistToolStripMenuItem.Click += (x, y) => { ChangeSelectedProperty(1); };
            ChangeAlbumToolStripMenuItem.Click += (x, y) => { ChangeSelectedProperty(2); };
            ChangeTrackNumberToolStripMenuItem.Click += (x, y) => { ChangeSelectedProperty(3); };
        }

        private void PlaylistProperties_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PlaylistProperties.AutoClose = true;
                PlaylistProperties.Hide();
            }
        }

        private void PlaylistProperties_Opened(object sender, EventArgs e)
        {
            PlaylistProperties.AutoClose = false;
        }

        public int GetFocusedItem()
        {
            var ItemIndex = -1;
            foreach (int Index in PlaylistView.SelectedIndices)
                if (PlaylistView.Items[Index].Focused)
                    ItemIndex = Index;

            return ItemIndex;
        }

        public void ChangeSelectedProperty(int SubItemIndex)
        {
            var Indices = PlaylistView.SelectedIndices;
            if (Indices.Count == 0) return;

            var ItemIndex = GetFocusedItem();
            //new RenameBox(PlaylistView, SubItemIndex);

            if (ItemIndex < PlaylistView.Items.Count)
            {
                var Item = PlaylistView.Items[ItemIndex];
                if (Item.SubItems.Count == 0) return;
                if (SubItemIndex == 0)
                {
                    Item.BeginEdit();
                }
                else if (SubItemIndex < Item.SubItems.Count)
                {
                    var renameBox = new RenameBox(PlaylistView, SubItemIndex);
                    renameBox?.Focus();
                }
            }
        }

        public void AfterSubItemEdit(ListViewItem Item)
        {
            var Index = PlaylistView.Items.IndexOf(Item);
            if (Index < 0 || Index >= Tracks.Count) return;
            var track = Tracks[Index];
            track.SetMetadata();
        }

        private void PlaylistView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (sender is ListViewItem s)
                AfterSubItemEdit(s);
        }

        private void DropDownMenu_Opening(object sender, CancelEventArgs e)
        {
            if (PlaylistView.SelectedIndices.Count == 0)
            {
                DropDownMenu.Hide();
            }
            else
            {
                var Index = PlaylistView.SelectedIndices[0];
                foreach (ToolStripItem Item in DropDownMenu.Items)
                    if (Item.Text == "Properties")
                        Item.Enabled = Tracks[Index].Writeable;
            }
        }

        private int DropIndex;

        private void PlaylistView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = new List<ListViewItem>();
            items.Add((ListViewItem) e.Item);
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
            var Extensions = new List<string> {".mp3", ".m4a"};
            if (IsDirectory(Path)) return false;
            foreach (var extension in Extensions)
                if (Path.EndsWith(extension, false, null))
                    return true;
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
                        if (IsMusicFile(file))
                            FilesToAdd.Add(file);
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