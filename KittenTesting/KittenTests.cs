using KittenPlayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using NAudio.Wave.Asio;

namespace KittenTesting
{
    [TestClass]
    public class Testing
    {

        [TestMethod]
        public void TabOrder()
        {
            MainWindow window = new MainWindow();
            var mainTab = window.MainTab;
            int IndexA = 0;
            int IndexB = 5;

            while (mainTab.MainTab.Controls.Count < 6)
            {
                mainTab.AddNewTab(mainTab.MainTab.Controls.Count.ToString());
            }

            var musicPageA = mainTab.MainTab.Controls[IndexA] as MusicPage;
            var musicPageB = mainTab.MainTab.Controls[IndexB] as MusicPage;
            mainTab.SwapTabPages(musicPageA, musicPageB);

            for (int i = 0; i < mainTab.MainTab.Controls.Count; i++)
            {
                if(mainTab.MainTab.Controls[i].Text != mainTab.MainTab.TabPages[i].Text)Assert.Fail();
            }
        }


        [TestMethod, Timeout(20000)]
        public void RequestOnlineTitle()
        {
            MainWindow window = new MainWindow();
            Track track = TestTrack;

            track.Artist = "aaa";
            track.Album = "bbb";
            track.Title = "ccc";

            String defaultDir = MainWindow.Instance.Options.DefaultDirectory;

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
            var window = new MainWindow();
            var track = TestTrack;

            track.Artist = "aaa";
            track.Album = "bbb";
            track.Title = "ccc";

            var defaultDir = window.Options.DefaultDirectory;

            if (!(MainTabs.Instance.Controls[0] is MusicPage tab)) return;
            tab.musicTab.AddTrack(track);
            tab.musicTab.DownloadTrack(track);
            if (!File.Exists(track.filePath)) Assert.Fail();
            tab.musicTab.MoveTrackToArtistAlbumDir(track);
            if (!File.Exists(defaultDir + "\\aaa\\bbb\\ccc.m4a")) Assert.Fail();
            if (!string.Equals(track.filePath, defaultDir + "\\aaa\\bbb\\ccc.m4a")) Assert.Fail();
        }

        private Track TestTrack { get; } = new Track("", "zReWPjreJzI");
    }
}