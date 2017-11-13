using System.Diagnostics;
using System.IO;

namespace KittenPlayer
{
    public partial class Track
    {
        public bool SendToArtistAlbum;
        public bool IsPlaying => MusicPlayer.Instance.CurrentTrack == this;

        public void SetArtistAlbumDir()
        {
            if (IsOnline) SendToArtistAlbum = true;
            if (!IsOffline) return;

            //String ArtistDir = SanitizeName(Artist);
            //String AlbumDir = SanitizeName(Album);
            var NameDir = Path.GetFileName(filePath);

            var OutputDir = MainWindow.Instance.Options.DefaultDirectory;
            //Debug.WriteLine(OutputDir);

            //foreach (String Component in new[] { ArtistDir, AlbumDir })
            //{
            //    if (!String.IsNullOrWhiteSpace(Component))
            //    {
            //        OutputDir += "\\" + Component;
            //        if (!Directory.Exists(OutputDir))
            //        {
            //            Directory.CreateDirectory(OutputDir);
            //        }
            //    }
            //}
            //OutputDir += "\\" + NameDir;

            OutputDir += "\\x\\" + NameDir;

            if (!File.Exists(OutputDir))
                File.Move(filePath, OutputDir);
            else
                File.Delete(filePath);
            if (File.Exists(OutputDir))
                filePath = OutputDir;

            if (Item != null) Item.SubItems[5].Text = filePath;
            Debug.WriteLine("Moved to " + filePath);

            //throw new NotImplementedException();
        }

        private static string SanitizeName(string Name)
        {
            return Name;
        }
    }
}