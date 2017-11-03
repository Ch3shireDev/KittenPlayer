using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl, IKittenInterface
    {
        static int index = 0;

        public List<Track> Tracks = new List<Track>();
        MusicPlayer musicPlayer = MusicPlayer.Instance;
        public int Index;

        static MusicTab SelectedTab => (MainTabs.Instance.SelectedTab as MusicPage).musicTab;

        public static class Names
        {
            public const String Play = "Play";
            public const String Pause = "Pause";
            public const String Stop = "Stop";
            public const String JDownload = "Just Download";
            public const String DAgain = "Download again";
            public const String DP = "Download and Play";
            public const String Title = "Retrieve Online Title";
            public const String Numbers = "Assign Track Numbers";
            public const String Dir = "Set Directory to /[Artist]/[Album]/...";
        }

        List<ToolStripMenuItem> MenuItems = new List<ToolStripMenuItem>()
        {
            { new ToolStripMenuItem(Names.Play, null, (sender, e) => { MusicPlayer.Instance.PlayAutomatic(); }) },
            { new ToolStripMenuItem(Names.Pause, null, (sender, e) => { MusicPlayer.Instance.Pause(); }) },
            { new ToolStripMenuItem(Names.Stop, null, (sender, e) => { MusicPlayer.Instance.Stop(); }) },
            { new ToolStripMenuItem(Names.JDownload, null, (sender, e) => { DownloadOnly_Static(); }) },
            { new ToolStripMenuItem(Names.DAgain, null, (sender, e) => { DownloadAgain(); }) },
            { new ToolStripMenuItem(Names.DP, null, (sender, e) => { DownloadAndPlay(); }) },
            { new ToolStripMenuItem(Names.Title, null, (sender, e) => { GetOnlineTitles(); }) },
            { new ToolStripMenuItem(Names.Numbers, null, (sender, e)=>{ AssignTrackNumbers(); }) },
            { new ToolStripMenuItem(Names.Dir, null, (sender, e)=>{ SetDirToArtistAlbum(); }) },
        };

        //in this function you can technically call it for all tracks in playlist. Player hangs itself after that call. I need to add the limit.


        static void DownloadAndPlay()
        {
            throw new NotImplementedException();
        }

        static void DownloadAgain()
        {
            throw new NotImplementedException();
        }

        static void DownloadOnly_Static()
        {
            DownloadManager.AddToDownload(GetSelectedTracks());
            
        }
        
        static void SetDirToArtistAlbum()
        {
            foreach (Track track in SelectedTracks)
            {
                track.MusicTab.MoveTrackToArtistAlbumDir(track);
            }
        }

        static void AssignTrackNumbers()
        {
            foreach (Track track in GetSelectedTracks())
            {
                var PlaylistView = track.Item.ListView;
                int Index = PlaylistView.Items.IndexOf(track.Item) + 1;
                track.Number = Index.ToString();
                track.Item.SubItems[3].Text = track.Number;
            }
        }

        static void GetOnlineTitles()
        {
            foreach (Track track in GetSelectedTracks())
            {
                RequestOnlineTitle(track);
            }
        }
        

        static List<Track> SelectedTracks => GetSelectedTracks();

        public static List<Track> GetSelectedTracks()
        {
            var Output = new List<Track>();
            int Index = MainTabs.Instance.SelectedIndex;
            MusicPage Page = MainTabs.Instance.Controls[Index] as MusicPage;
            MusicTab musicTab = Page.musicTab;
            ListView PlaylistView = musicTab.PlaylistView;
            foreach (int ItemIndex in PlaylistView.SelectedIndices)
            {
                Track track = musicTab.Tracks[ItemIndex];
                track.Item = PlaylistView.Items[ItemIndex];
                Output.Add(track);
            }
            return Output;
        }

        public static void RequestOnlineTitle(Track track)
        {
#if DEBUG
            track.GetOnlineTitle();
#else
            Task.Run(()=>track.GetOnlineTitle());
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
            int Index = Tracks.IndexOf(track);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index))
            {
                PlaylistView.SelectedIndices.Add(Index);
            }
        }

        public Track GetNextTrack(Track Current)
        {
            int Index = Tracks.IndexOf(Current);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index + 1))
            {
                Index++;
                return Tracks[Index];
            }
            else
            {
                return null;
            }
        }

        public Track GetPreviousTrack(Track Current)
        {
            int Index = Tracks.IndexOf(Current);
            if (Enumerable.Range(0, Tracks.Count).Contains(Index - 1))
            {
                Index--;
                return Tracks[Index];
            }
            else
            {
                return null;
            }
        }

        public String GetSelectedTrackPath()
        {
            if (PlaylistView.SelectedIndices.Count == 0)
            {
                return "";
            }
            else
            {
                int Index = PlaylistView.SelectedIndices[0];
                return Tracks[Index].filePath;
            }
        }




        int prevItem = -1;

        private void MusicTab_Click(object sender, EventArgs e)
        {
        }

        private void PlaylistView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PlaylistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlaySelectedTrack();
            }
            else if (e.KeyChar == (char)Keys.Space)
            {
                MusicPlayer player = MusicPlayer.Instance;
                if (player.IsPlaying)
                {
                    MusicPlayer.Instance.Pause();
                }
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
            {
                Item.Selected = true;
            }
        }

        private void PlaylistView_Leave(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in PlaylistView.Items)
            {
                Item.Selected = false;
            }
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
                int Index = GetFocusedItem();
                Track track = Tracks[Index];

                var Items = new Dictionary<String, ToolStripItem>();
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
                }
                else if (track.Status == Track.StatusType.Offline)
                {
                    DropDownMenu.Items.Insert(0, Items[Names.Play]);
                    DropDownMenu.Items.Insert(1, Items[Names.Title]);
                    DropDownMenu.Items.Insert(2, Items[Names.DAgain]);
                    DropDownMenu.Items.Insert(3, Items[Names.Numbers]);
                    DropDownMenu.Items.Insert(4, Items[Names.Dir]);
                }
                else if (track.Status == Track.StatusType.Online)
                {
                    DropDownMenu.Items.Insert(0, Items[Names.DP]);
                    DropDownMenu.Items.Insert(1, Items[Names.Title]);
                    DropDownMenu.Items.Insert(2, Items[Names.JDownload]);
                    DropDownMenu.Items.Insert(3, Items[Names.Numbers]);
                }

                var font = DropDownMenu.Items[0].Font;
                DropDownMenu.Items[0].Font = new System.Drawing.Font(font.FontFamily, font.Size, System.Drawing.FontStyle.Bold);
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

        void SaveColumns() =>
            LocalData.Instance.SaveColumns(PlaylistView);

        void LoadColumns() =>
            LocalData.Instance.LoadColumns(ref PlaylistView);


#if DEBUG
        public void DownloadTrack(Track track) =>
#else
        public async Task DownloadTrack(Track track) =>
#endif
        YoutubeDL.DownloadTrack(track);

    }



    class DownloadManager
    {
        static DownloadManager Instance = null;
        List<Track> TracksToDownload;
        public static int ActiveDownloads = 0;

        public static void CallDownloadStarted() { ActiveDownloads++; }
        public static bool RequestDownloadStart()
        {
            return true;
            //return ActiveDownloads < 1;
        }
        public static void DownloadEnded() { ActiveDownloads--; }

        DownloadManager() { }

        public static void AddToDownload(List<Track> tracks)
        {
            if (Instance == null)
                Instance = new DownloadManager();
            if (Instance.TracksToDownload == null)
                Instance.TracksToDownload = new List<Track>();
            Instance.TracksToDownload.AddRange(tracks);
            Instance.Download();
        }

        public static int Counter = 0;

#if DEBUG
        private void Download()
#else
        private async Task Download()
#endif
        {
            while (TracksToDownload.Count > 0)
            {
                Track track = TracksToDownload[0];
#if DEBUG
                YoutubeDL.DownloadTrack(track);
#else
                if (Counter < 3)
                {
                    YoutubeDL.DownloadTrack(track);
                }
                else
                {
                    await YoutubeDL.DownloadTrack(track);
                }
#endif
                TracksToDownload.Remove(track);
            }
        }

    }
}