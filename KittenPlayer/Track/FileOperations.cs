using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace KittenPlayer
{
    public partial class Track
    {
        public bool SendToArtistAlbum = false;

        public void SetArtistAlbumDir()
        {
            if (IsOnline) SendToArtistAlbum = true;
            if (!IsOffline) return;



            //String ArtistDir = SanitizeName(Artist);
            //String AlbumDir = SanitizeName(Album);
            String NameDir = Path.GetFileName(filePath);

            String OutputDir = MainWindow.Instance.options.SelectedDirectory;
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
            {
                File.Move(filePath, OutputDir);
            }
            else
            {
                File.Delete(filePath);
            }
            if (File.Exists(OutputDir))
            {
                filePath = OutputDir;
            }

            if (Item != null) Item.SubItems[5].Text = filePath;
            Debug.WriteLine("Moved to " + filePath);

            //throw new NotImplementedException();
        }

        static String SanitizeName(String Name)
        {
            return Name;
        }

    }
}
