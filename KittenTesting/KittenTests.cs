using KittenPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenTesting
{
    [TestClass]
    public class Testing
    {
        [TestMethod, Timeout(10000)]
        public void RequestOnlineTitle()
        {
            //RequestOnlineTitle(Track track)
            MainWindow Window = new MainWindow();
            Track track = GetTestTrack();

            track.Artist = "aaa";
            track.Album = "bbb";
            track.Title = "ccc";

            String DefaultDir = MainWindow.Instance.Options.DefaultDirectory;

            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.AddTrack(track);
                tab.musicTab.DownloadTrack(track);
                if (!File.Exists(track.filePath)) Assert.Fail();
                MusicTab.RequestOnlineTitle(track);

            }
        }

        [TestMethod, Timeout(10000)]
        public void MoveFileToArtistAlbum()
        {
            MainWindow Window = new MainWindow();
            Track track = GetTestTrack();

            track.Artist = "aaa";
            track.Album = "bbb";
            track.Title = "ccc";

            String DefaultDir = MainWindow.Instance.Options.DefaultDirectory;

            if(MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.AddTrack(track);
                tab.musicTab.DownloadTrack(track);
                if (!File.Exists(track.filePath)) Assert.Fail();
                tab.musicTab.MoveTrackToArtistAlbumDir(track);
                if (!File.Exists(DefaultDir + "\\aaa\\bbb\\ccc.m4a")) Assert.Fail();
                if (!String.Equals(track.filePath, DefaultDir + "\\aaa\\bbb\\ccc.m4a")) Assert.Fail();
            }
        }

        [TestMethod, Timeout(10000)]
        public void DownloadTrackWithDash()
        {
            MainWindow Window = new MainWindow();
            String ID = "-uNBi5sYcYo";
            Track track = new Track("", ID);
            
            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.PlaylistView.Items.Clear();
                tab.musicTab.Tracks.Clear();
                tab.musicTab.AddTrack(track);
                tab.musicTab.DownloadTrack(track);
                if (!File.Exists(track.filePath)) Assert.Fail();
            }
            else
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void MissingTitlesOnDownload()
        {
            MainWindow Window = new MainWindow();
            //String ID = "rjrxP5CC9Jo";
            //String ID = "9Ll3TaVmIfk";
            String ID = "zReWPjreJzI";
            Track track = new Track("", ID);

            String Artist = "aaa";
            String Album = "bbb";
            String Title = "ccc";

            track.Artist = Artist;
            track.Album = Album;
            track.Title = Title;


            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.PlaylistView.Items.Clear();
                tab.musicTab.Tracks.Clear();
                tab.musicTab.AddTrack(track);

                bool f1 = track.Item.SubItems[0].Text == Title;
                bool f2 = track.Item.SubItems[1].Text == Artist;
                bool f3 = track.Item.SubItems[2].Text == Album;

                if (!(f1 && f2 && f3)) Assert.Fail();

                tab.musicTab.DownloadTrack(track);
                //Task.Run(() => tab.musicTab.DownloadTrack(track)).Wait();

                List<bool> list = new List<bool> {
                    track.Item.SubItems[0].Text == Title,
                    track.Item.SubItems[1].Text == Artist,
                    track.Item.SubItems[2].Text == Album,
                    track.Title == Title,
                    track.Artist == Artist,
                    track.Album == Album
                };

                foreach(bool flag in list)
                {
                    if (!flag) Assert.Fail();
                }

                if (!File.Exists(track.filePath)) Assert.Fail();
            }
        }

        [TestMethod]
        public void UnavailableID()
        {
            MainWindow Window = new MainWindow();
            //String ID = "rjrxP5CC9Jo";
            String ID = "9Ll3TaVmIfk";
            //String ID = "zReWPjreJzI";
            Track track = new Track("", ID);


            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.PlaylistView.Items.Clear();
                tab.musicTab.Tracks.Clear();
                tab.musicTab.AddTrack(track);
                tab.musicTab.DownloadTrack(track);
                //Task.Run(() => tab.musicTab.DownloadTrack(track)).Wait();
            }
        }

        void RemoveFiles()
        {
            var files = Directory.EnumerateFiles(@"C:/Users/cheshire/Music/");
            foreach (var file in files)
            {
                if (Path.GetExtension(file).Contains("m4a"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
            }
        }

        [TestMethod, Timeout(10000)]
        public void SearchAndAdd()
        {
            MainWindow Window = new MainWindow();
            RemoveFiles();

            SearchResult query = new SearchResult("Dead Can Dance");
            var results = Task.Run(async () => await query.GetResults()).Result;

            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.PlaylistView.Items.Clear();
                tab.musicTab.Tracks.Clear();

                List<Track> tracks = new List<Track>();

                foreach (Result r in results)
                {
                    tracks.AddRange(tab.musicTab.DropThumbnail(new Thumbnail(r)));
                }

                tab.musicTab.AddTrack(tracks);



                //foreach (Track track in tab.musicTab.Tracks)
                //{
                //    String Title = Task.Run(() => track.GetOnlineTitle()).Result;
                //    Debug.WriteLine(Title);
                //    Debug.WriteLine(track.ID);
                //    if (String.IsNullOrWhiteSpace(Title))
                //    {
                //        Assert.Fail();
                //    }
                //    if (!Title.Contains("ERROR")) continue;

                //    bool Success = Task.Run(() => tab.musicTab.Download(track)).Result;
                //    if (Success && String.IsNullOrWhiteSpace(track.filePath))
                //    {
                //        Debug.WriteLine("Empty filepath!");
                //        Trace.WriteLine(track.ID);
                //        Assert.Fail();
                //    }
                //}
            }
        }


        Track GetTestTrack()
        {
            return new Track("", "zReWPjreJzI");
        }
        
        [TestMethod, Timeout(10000)]
        public void DownloadItem()
        {
            MainWindow window = new MainWindow();
            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                MusicTab musicTab = tab.musicTab;
                musicTab.Tracks.Clear();
                musicTab.PlaylistView.Items.Clear();
                Track track = GetTestTrack();
                musicTab.AddTrack(track);
                Task.Run(() => musicTab.DownloadTrack(track)).Wait();
            }
        }

        //[TestMethod, Timeout(20000)]
        //public void JustDownloadTrack()
        //{
        //    Assert.Fail();

        //    //MainWindow window = new MainWindow();
        //    //if (MainTabs.Instance.Controls[0] is MusicPage tab)
        //    //{
        //    //    MusicTab musicTab = tab.musicTab;
        //    //    musicTab.Tracks.Clear();
        //    //    musicTab.PlaylistView.Items.Clear();
        //    //    Track track = GetTestTrack();
        //    //    musicTab.AddTrack(track);
        //    //    Task.Run(() => track.Download()).Wait();
        //    //}
        //}

        //[TestMethod]
        //public void YoutubeDLTest()
        //{

        //    YoutubeDL youtube = new YoutubeDL("zReWPjreJzI");
        //    if (File.Exists("x.m4a")) File.Delete("x.m4a");
        //    String Title = Task.Run(()=>youtube.Download("-o x.m4a")).Result;
        //    if (String.IsNullOrWhiteSpace(Title)) Assert.Fail();

        //}

        //[TestMethod]
        //public void YoutubeDLTestWithProgressBar()
        //{
        //    ProgressBar bar = new ProgressBar();
        //    YoutubeDL youtube = new YoutubeDL("zReWPjreJzI") { progressBar = bar };
        //    if (File.Exists("x.m4a")) File.Delete("x.m4a");
        //    String Title = Task.Run(() => youtube.Download("-o x.m4a")).Result;
        //    if (String.IsNullOrWhiteSpace(Title)) Assert.Fail();

        //}

        //[TestMethod, Timeout(10000)]
        //public void DataTest()
        //{
        //    Assert.Fail();
        //    MainWindow window = new MainWindow();
        //    if (MainTabs.Instance.Controls[0] is MusicPage tab)
        //    {
        //        MusicTab musicTab = tab.musicTab;
        //        musicTab.Tracks.Clear();
        //        musicTab.PlaylistView.Items.Clear();
        //        Track track = new Track("","zReWPjreJzI");
        //        track.Title = Task.Run(()=>track.GetOnlineTitle()).Result;
        //        musicTab.AddTrack(track);

        //        String[] list = new[] { track.Title, "Aaa", "Bbb", "1" };
                
        //        //value disappears after download
        //        track.Artist = list[1];
        //        track.Album = list[2];
        //        track.Number = list[3];

        //        for (int i = 0; i < 4; i++)
        //            musicTab.PlaylistView.Items[0].SubItems[i].Text = list[i];

        //        track.Item = musicTab.PlaylistView.Items[0];
        //        //Task.Run(() => musicTab.Download(track)).Wait();

        //        String[] newList = new[] { track.Title, track.Artist, track.Album, track.Number };

        //        for (int i = 0; i < 4; i++)
        //        {
        //            String newValue = musicTab.PlaylistView.Items[0].SubItems[i].Text;
        //            bool f1 = newValue != list[i];
        //            bool f2 = newList[i] != list[i];
        //            if (f1 || f2)
        //            {
        //                Debug.WriteLine("List: " + i);
        //                Assert.Fail();
        //            }

        //        }

        //        if (track.Artist == "") Assert.Fail();

        //    }
        //}

        [TestMethod, Timeout(10000)]
        public void MusicTabDownload()
        {
            MainWindow window = new MainWindow();
            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                MusicTab musicTab = tab.musicTab;
                musicTab.Tracks.Clear();
                musicTab.PlaylistView.Items.Clear();
                Track track = new Track("", "zReWPjreJzI");
                musicTab.AddTrack(track);
                Task.Run(() => musicTab.DownloadTrack(track)).Wait();
                if (!track.IsOffline) Assert.Fail();
                if (!File.Exists(track.filePath)) Assert.Fail();
                if (track.Item.SubItems[4].Text != "Offline") Assert.Fail();
            }
        }

        void Download()
        {

        }
        
        [TestMethod, Timeout(10000)]
        public void TestDownload()
        {
            if (File.Exists("x.m4a")) File.Delete("x.m4a");

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a -o x.m4a zReWPjreJzI";
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
                String output = reader.ReadLineAsync().Result;         
#endif
                if (String.IsNullOrWhiteSpace(output)) continue;
                Debug.WriteLine(output);
            }
        }
    }
}
