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
        [TestMethod]
        public void SearchAndAdd()
        {
            MainWindow Window = new MainWindow();

            var files = Directory.EnumerateFiles(@"C:/Users/cheshire/Music/");

            foreach(var file in files)
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

                foreach(Track track in tab.musicTab.Tracks)
                {

                    tab.musicTab.Download(track).Wait();
                    return;
                    //tab.musicTab.Play(i).Wait();
                    //MusicPlayer.Instance.Play(track,tab.musicTab);

                    
                }
            }
        }
    }
}
