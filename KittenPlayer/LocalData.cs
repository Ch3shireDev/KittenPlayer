using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace KittenPlayer
{
    public class LocalData
    {
        private static LocalData instance { get; set; }
        private  string Path { get; }

        private LocalData()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Path += "\\KittenPlayer\\";

            if (!File.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        public static LocalData Instance
        {
            get
            {
                if (instance == null)
                    instance = new LocalData();
                return instance;
            }
        }

        public void SavePlaylists(TabControl MainTabs)
        {
            if (MainTabs == null) return;
            try
            {
                foreach (var file in Directory.GetFiles(Path))
                    if (File.Exists(file)) File.Delete(file);

                for (var i = 0; i < MainTabs.Controls.Count; i++)
                    SavePlaylist(MainTabs.Controls[i] as MusicPage, GetFullPath(i));

                Debug.WriteLine("Playlist data saved.");
            }
            catch
            {
                Debug.WriteLine("Problem with playlist save.");
            }
        }

        public void SavePlaylist(MusicPage musicPage, string Name)
        {
            var fs = new FileStream(Name, FileMode.Create);
            var formatter = new BinaryFormatter();
            var data = new PlaylistData(musicPage);
            formatter.Serialize(fs, data);
            fs.Close();
            fs = new FileStream(Name, FileMode.Open);
            fs.Close();
        }

        private MusicPage LoadPlaylist(int i)
        {
            var Name = GetFullPath(i);
            if (!File.Exists(Name)) return null;
            var fs = new FileStream(Name, FileMode.Open);

            if (!fs.CanRead) return null;
            if (fs == null) return null;
            

            var formatter = new BinaryFormatter();

            if (fs.Length == 0) return null;

            var data = formatter.Deserialize(fs) as PlaylistData;
            fs.Close();

            if (data == null) return null;

            return data.GetMusicPage();
        }

        private string GetFullPath(int i)
        {
            return Path + i + ".dat";
        }

        public void LoadPlaylists(TabControl MainTab)
        {
            for (var i = 0; i < Num(); i++)
            {
                var musicPage = LoadPlaylist(i);
                if (musicPage != null && musicPage.musicTab.Tracks.Count > 0)
                    MainTab.Controls.Add(musicPage);
            }
        }

        public int Num()
        {
            return Directory.GetFiles(Path, "*.dat", SearchOption.TopDirectoryOnly).Length;
        }

        public void SaveColumns(ListView PlaylistView)
        {
            var Widths = new List<int>();
            foreach (ColumnHeader column in PlaylistView.Columns)
            {
                Widths.Add(column.Width);
                Debug.Write(column.Width + " ");
            }
            Debug.WriteLine("");
            var Name = Path + "columns.dat";
            try
            {
                var fs = new FileStream(Name, FileMode.Create);
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, Widths);
                fs.Close();
                Debug.WriteLine("Data saved to " + Name);
            }
            catch
            {
                Debug.WriteLine("Can't get access to columns.dat");
            }
        }

        public void LoadColumns(ref ListViewEx PlaylistView)
        {
            if (PlaylistView == null) return;
            var Name = Path + "columns.dat";
            if (!File.Exists(Name)) return;
            try
            {
                var fs = new FileStream(Name, FileMode.Open);
                if (!fs.CanRead) return;
                var formatter = new BinaryFormatter();
                var Widths = formatter.Deserialize(fs) as List<int>;
                fs.Close();
                if (Widths == null) return;
                for (var i = 0; i < PlaylistView.Columns.Count; i++)
                {
                    if (i >= Widths.Count) return;
                    PlaylistView.Columns[i].Width = Widths[i];
                }
            }
            catch
            {
                try
                {
                    File.Delete(Name);
                }
                catch
                {
                    Debug.WriteLine("Can't delete file!");
                }
                Debug.WriteLine("Can't read from columns file!");
            }
            Debug.WriteLine("Data loaded from " + Name);
        }

        [Serializable]
        private class PlaylistData
        {
            public readonly string PlaylistName;
            private readonly List<TrackData> Tracks = new List<TrackData>();

            public PlaylistData(MusicPage musicPage)
            {
                if (musicPage == null) return;
                PlaylistName = musicPage.Text;
                AddTrack(musicPage.musicTab.Tracks);
            }

            public PlaylistData(string PlaylistName)
            {
                this.PlaylistName = PlaylistName;
            }

            private void AddTrack(Track track)
            {
                var data = new TrackData(track);
                Tracks.Add(data);
            }

            private void AddTrack(List<Track> Tracks)
            {
                foreach (var track in Tracks) AddTrack(track);
            }

            public MusicPage GetMusicPage()
            {
                var Out = new MusicPage(PlaylistName);
                foreach (var data in Tracks)
                    Out.AddTrack(data.GetTrack());
                Out.Refresh();
                return Out;
            }

            [Serializable]
            private class TrackData
            {
                public readonly string ID;
                public readonly Hashtable Info;
                public readonly string TrackPath;

                public TrackData(Track track)
                {
                    TrackPath = track.filePath;
                    ID = track.ID;
                    Info = track.Info;
                }

                public Track GetTrack()
                {
                    var Out = new Track(TrackPath, ID);
                    Out.Info = Info;
                    return Out;
                }
            }
        }
    }
}