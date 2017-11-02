using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

#if DEBUG
        public void DownloadTrack(Track track)
#else
        public async Task DownloadTrack(Track track)
#endif
        {
            if (!track.IsOnline) return;

            Debug.WriteLine(track.ID);

            Rectangle rect = track.Item.SubItems[5].Bounds;
            ListViewEx listViewEx = PlaylistView as ListViewEx;
            ProgressBar progressBar = new ProgressBar { Bounds = rect };
            int Index = PlaylistView.Items.IndexOf(track.Item);
            listViewEx.AddEmbeddedControl(progressBar, 5, Index);
            progressBar.Show();
            progressBar.Focus();

            if (File.Exists("x.m4a")) File.Delete("x.m4a");

            YoutubeDL.ProcessStart(track, "-o x.m4a", out Process process);

            StreamReader reader = process.StandardOutput;
            while (!process.HasExited)
            {
#if DEBUG
                String output = reader.ReadLine();
#else
                String output = await reader.ReadLineAsync();         
#endif
                if (String.IsNullOrWhiteSpace(output)) continue;
                Debug.WriteLine(output);
                Regex r = new Regex(@"\[download]\s*([0-9.]*)%", RegexOptions.IgnoreCase);
                Match m = r.Match(output);
                if (m.Success)
                {
                    Group g = m.Groups[1];
                    double Percent = double.Parse(g.ToString());
                    progressBar.Value = Convert.ToInt32(Percent);
                }
            }

            YoutubeDL.ProcessStart(track, "--get-filename", out Process process2);

            reader = process2.StandardOutput;

            String Name;
            {
#if DEBUG
                String output = reader.ReadToEnd();
#else
                String output = await reader.ReadToEndAsync();         
#endif
                string[] str = output.Split('\n');
                Debug.WriteLine(str[0]);
                Name = str[0];
            }

            progressBar.Hide();
            listViewEx.RemoveEmbeddedControl(progressBar);

            if (File.Exists("x.m4a"))
            {
                String OutputDir = MainWindow.Instance.Options.DefaultDirectory + "\\" + Name;
                if (File.Exists(OutputDir))
                {
                    File.Delete(OutputDir);
                }
                File.Move("x.m4a", OutputDir);
                track.filePath = OutputDir;
                track.OfflineToLocalData();
                track.UpdateItem();
            }
            else
            {
                PlaylistView.Items.Remove(track.Item);
                Tracks.Remove(track);
            }

            MainWindow.SavePlaylists();
        }
    }
}
