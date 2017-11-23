using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
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
            if (track == null)
            {
                MainWindow.Instance.SetDefaultTitle();
            }
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

            foreach (var str in new[] { track.Artist, track.Album })
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
    }
}