using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl, IKittenInterface
    {
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
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlaySelectedTrack();
            }
            else if (e.KeyChar == (char)Keys.Space)
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
    }
}