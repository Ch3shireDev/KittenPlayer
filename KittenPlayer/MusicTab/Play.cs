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

        //public async Task HideBar(Track track)
        //{
        //    ListViewEx listViewEx = PlaylistView as ListViewEx;

        //    ProgressBar bar = track.progressBar;

        //    while (bar.Value != 100)
        //    {
        //        await Task.Delay(25);
        //    }

        //    bar.Hide();
        //    listViewEx.RemoveEmbeddedControl(bar);
        //}

        //public async Task<bool> Download(Track track)
        //{
        //    Rectangle rect = track.Item.SubItems[5].Bounds;
        //    ListViewEx listViewEx = PlaylistView as ListViewEx;
        //    ProgressBar bar = new ProgressBar
        //    {
        //        Bounds = rect
        //    };

        //    int Index = PlaylistView.Items.IndexOf(track.Item);
        //    listViewEx.AddEmbeddedControl(bar, 5, Index);
        //    bar.Show();
        //    bar.Focus();

        //    track.progressBar = bar;
        //    //bool Success = await track.Download();
        //    PlaylistView.FocusedItem = track.Item;
        //    HideBar(track);
        //    //if (Success)
        //    //{
        //    //    PlaylistView.Items[Index] = track.GetListViewItem(PlaylistView);
        //    //    track.Item = PlaylistView.Items[Index];
        //    //    track.OfflineToLocalData();
        //    //    track.SetMetadata();
        //    //    track.SaveMetadata();
        //    //}

        //    //this one should be outside download function

        //    //else
        //    //{
        //    //    PlaylistView.Items.Remove(track.Item);
        //    //    Tracks.Remove(track);
        //    //}

        //    //return Success;
        //}



        //public void PlayNext()
        //{
        //    int Index = Tracks.IndexOf(musicPlayer.CurrentTrack);
        //    Index++;
        //    if (Index >= 0 && Index < Tracks.Count)
        //    {
        //        PlaylistView.SelectedIndices.Clear();
        //        PlaylistView.SelectedIndices.Add(Index);
        //        Play(Index);
        //    }
        //    //if (PlaylistView.SelectedIndices.Count == 0) return;
        //    //int Index = PlaylistView.SelectedIndices[0];
        //    //Index++;
        //    //if (Index < Tracks.Count)
        //    //{
        //    //    PlaylistView.SelectedIndices.Clear();
        //    //    PlaylistView.SelectedIndices.Add(Index);
        //    //    Play(Index);
        //    //}
        //}

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


        public async Task Play(int Index)
        {
            if (Index >= Tracks.Count || Index < 0) return;
            Track track = Tracks[Index];
            if (track.IsOnline)
            {
#if DEBUG
                DownloadTrack(track);
#else
                await DownloadTrack(track);
#endif
                //bool Success = await Download(track);
                //if (!Success)
                //{
                //    Tracks.RemoveAt(Index);
                //    PlaylistView.Items.RemoveAt(Index);
                //    MainWindow.SavePlaylists();
                //    return;
                //}
                //MainWindow.SavePlaylists();
            }
            musicPlayer.CurrentTab = this;
            musicPlayer.CurrentTrack = track;
            musicPlayer.Stop();
            musicPlayer.Load(track, this);
            musicPlayer.Play();
            //RemoveTrack(Index);
        }

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
            ProgressBar progressBar = new ProgressBar{Bounds = rect};
            int Index = PlaylistView.Items.IndexOf(track.Item);
            listViewEx.AddEmbeddedControl(progressBar, 5, Index);
            progressBar.Show();
            progressBar.Focus();

            if (File.Exists("x.m4a")) File.Delete("x.m4a");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a -o x.m4a ";
            if (track.ID[0] == '-')
            {
                startInfo.Arguments += "-- ";
            }
            startInfo.Arguments += track.ID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

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

            process = new Process();
            startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl --get-filename -f m4a ";
            if (track.ID[0] == '-')
            {
                startInfo.Arguments += "-- ";
            }
            startInfo.Arguments += track.ID;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            reader = process.StandardOutput;

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
            String DefaultDir = MainWindow.Instance.Options.DefaultDirectory;

            foreach (String str in new[] { track.Artist, track.Album })
            {
                DefaultDir += "\\" + str;
                ProcessDir(DefaultDir);
            }
            String newPath = DefaultDir + "\\" + track.Title + Path.GetExtension(track.filePath);
            File.Copy(track.filePath, newPath);
            track.filePath = newPath;
        }
    }
}
