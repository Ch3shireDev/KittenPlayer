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

        [TestMethod]
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
        // https://www.youtube.com/watch?v=-uNBi5sYcYo

        [TestMethod]
        public void YoutubeDLError()
        {
            MainWindow Window = new MainWindow();
            Track track = new Track("", "9Ll3TaVmIfk");


            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                tab.musicTab.PlaylistView.Items.Clear();
                tab.musicTab.Tracks.Clear();
                tab.musicTab.AddTrack(track);
                bool Success = Task.Run(() => tab.musicTab.Download(track)).Result;
            }
        }



        [TestMethod]
        public void DisappearingDataTest()
        {
            MainWindow window = new MainWindow();
            if (MainTabs.Instance.Controls[0] is MusicPage tab)
            {
                MusicTab musicTab = tab.musicTab;
                musicTab.Tracks.Clear();
                musicTab.PlaylistView.Items.Clear();
                Track track = new Track("","zReWPjreJzI");
                track.Title = Task.Run(()=>track.GetOnlineTitle()).Result;
                musicTab.AddTrack(track);

                String[] list = new[] { track.Title, "Aaa", "Bbb", "1" };
                
                //value disappears after download
                track.Artist = list[1];
                track.Album = list[2];
                track.Number = list[3];

                for (int i = 0; i < 4; i++)
                    musicTab.PlaylistView.Items[0].SubItems[i].Text = list[i];

                track.Item = musicTab.PlaylistView.Items[0];
                Task.Run(()=>musicTab.Download(track)).Wait();
                
                String[] newList = new[] { track.Title, track.Artist, track.Album, track.Number };

                for (int i = 0; i < 4; i++)
                {
                    String newValue = musicTab.PlaylistView.Items[0].SubItems[i].Text;
                    bool f1 = newValue != list[i];
                    bool f2 = newList[i] != list[i];
                    if (f1 || f2)
                    {
                        Debug.WriteLine("List: " + i);
                        Assert.Fail();
                    }

                }
                
            }
        }
    }
}
