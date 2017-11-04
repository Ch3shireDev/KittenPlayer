using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
        public void PlaySelected()
        {
            if (PlaylistView.SelectedIndices.Count == 0) return;
            musicPlayer.CurrentTab = this;
            int Index = PlaylistView.SelectedIndices[0];
            Play(Index);
        }
        
        private void PlaylistView_DoubleClick(object sender, EventArgs e) =>
            PlaySelected();

        public void PlaySelectedTrack() =>
            PlaySelected();
        
        private void MusicTab_DoubleClick(object sender, EventArgs e) =>
            Play(PlaylistView.TabIndex);

        public async Task Play(Track track)
        {
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
            Track track = Tracks[Index];
            await Play(track);
        }

        static void ProcessDir(String Dir)
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
        }

        public void MoveTrackToArtistAlbumDir(Track track)
        {
            if (!track.IsOffline) return;
            if (track.IsPlaying) return;
            String DefaultDir = MainWindow.Instance.Options.DefaultDirectory;

            foreach (String str in new[] { track.Artist, track.Album })
            {
                if (String.IsNullOrWhiteSpace(str)) continue;
                DefaultDir += "\\" + str;
                ProcessDir(DefaultDir);
            }
            String newPath = DefaultDir + "\\" + track.Title + Path.GetExtension(track.filePath);
            if (String.Compare(track.filePath, newPath, true) == 0) return;
            if (File.Exists(newPath)) File.Delete(newPath);
            File.Copy(track.filePath, newPath);
            if (File.Exists(newPath))
            {
                track.filePath = newPath;
                File.Delete(track.filePath);
            }
        }
    }
}
