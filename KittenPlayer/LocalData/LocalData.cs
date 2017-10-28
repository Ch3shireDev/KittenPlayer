using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace KittenPlayer
{

    public class LocalData
    {
        private String Path;

        private LocalData()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Path += "\\KittenPlayer\\";

            if (!File.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        static LocalData instance;
        public static LocalData Instance {
            get {
                if (instance == null)
                    instance = new LocalData();
                return instance;
            }
        }
        

        public void SavePlaylists(TabControl MainTabs)
        {
            foreach(String file in Directory.GetFiles(Path))
            {
                if (File.Exists(file)) File.Delete(file);
            }

            for (int i = 0; i < MainTabs.Controls.Count; i++)
            {
                SavePlaylist(MainTabs.Controls[i] as MusicPage, GetFullPath(i));
            }
        }

        [Serializable]
        class PlaylistData
        {
            public String PlaylistName;
            List<TrackData> Tracks = new List<TrackData>();

            public PlaylistData(MusicPage musicPage)
            {
                this.PlaylistName = musicPage.Text;
                AddTrack(musicPage.musicTab.Tracks);
            }

            public PlaylistData(String PlaylistName)
            {
                this.PlaylistName = PlaylistName;
            }

            void AddTrack(Track track)
            {
                TrackData data = new TrackData(track);
                Tracks.Add(data);
            }

            void AddTrack(List<Track> Tracks)
            {
                foreach(Track track in Tracks) AddTrack(track);
            }

            public MusicPage GetMusicPage()
            {
                MusicPage Out = new MusicPage(PlaylistName);
                foreach (TrackData data in Tracks)
                    Out.AddTrack(data.GetTrack());
                Out.Refresh();
                return Out;
            }
            
            [Serializable]
            class TrackData
            {
                public String TrackPath;
                public String ID;
                public Hashtable Info;

                public TrackData(Track track)
                {
                    this.TrackPath = track.filePath;
                    this.ID = track.ID;
                    Info = track.Info;
                }

                public Track GetTrack()
                {
                    Track Out = new Track(TrackPath, ID);
                    Out.Info = Info;
                    return Out;
                }
            }
        }

        public void SavePlaylist(MusicPage musicPage, String Name)
        {
            FileStream fs = new FileStream(Name, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            PlaylistData data = new PlaylistData(musicPage);
            formatter.Serialize(fs, data);
            fs.Close();
            fs = new FileStream(Name, FileMode.Open);
            fs.Close();
        }
        
        MusicPage LoadPlaylist(int i)
        {
            String Name = GetFullPath(i);
            if (!File.Exists(Name)) return null;
            FileStream fs = new FileStream(Name, FileMode.Open);

            if (!fs.CanRead) return null;
            
            BinaryFormatter formatter = new BinaryFormatter();
            PlaylistData data = formatter.Deserialize(fs) as PlaylistData;
            fs.Close();

            if (data == null) return null;
            
            return data.GetMusicPage();
        }

        String GetFullPath(int i)
        {
            return Path + i + ".dat";
        }

        public void LoadPlaylists(TabControl MainTabs)
        {
            for(int i = 0; i < Num(); i++)
            {
                MusicPage musicPage = LoadPlaylist(i);
                if(musicPage != null)
                {
                    MainTabs.Controls.Add(musicPage);
                }
            }
        }


        public int Num()
        {
            return Directory.GetFiles(Path, "*.dat", SearchOption.TopDirectoryOnly).Length;
        }

        public void SaveColumns(ListView PlaylistView)
        {
            List<int> Widths = new List<int>();
            foreach (ColumnHeader column in PlaylistView.Columns)
            {
                Widths.Add(column.Width);
                Debug.Write(column.Width + " ");
            }
            Debug.WriteLine("");
            String Name = Path + "columns.dat";
            FileStream fs = new FileStream(Name, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Widths);
            fs.Close();
            Debug.WriteLine("Data saved to " + Name);
        }

        public void LoadColumns(ref ListView PlaylistView)
        {
            if (PlaylistView == null) return;
            String Name = Path + "columns.dat";
            if (!File.Exists(Name)) return;
            FileStream fs = new FileStream(Name, FileMode.Open);
            if (!fs.CanRead) return;
            BinaryFormatter formatter = new BinaryFormatter();
            List<int> Widths = formatter.Deserialize(fs) as List<int>;
            fs.Close();
            if (Widths == null) return;
            for(int i = 0; i < PlaylistView.Columns.Count; i++)
            {
                if (i >= Widths.Count) return;
                PlaylistView.Columns[i].Width = Widths[i];
            }
            Debug.WriteLine("Data loaded from " + Name);
        }
    }
}
