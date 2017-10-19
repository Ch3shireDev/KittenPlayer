using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace KittenPlayer
{

    class LocalData
    {
        private static LocalData Instance = null;
        private String Path;

        private LocalData()
        {
            Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Path += "\\KittenPlayer\\";

            if (!File.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        public static LocalData GetInstance()
        {
            if(Instance == null)
            {
                Instance = new LocalData();
            }
            return Instance;
        }
        

        public void SavePlaylists(TabControl MainTabs)
        {
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
                TrackData data = new TrackData(track.filePath, track.fileName);
                Tracks.Add(data);
            }

            void AddTrack(List<Track> Tracks)
            {
                foreach(Track track in Tracks)
                {
                    AddTrack(track);
                }
            }

            public MusicPage GetMusicPage()
            {
                MusicPage Out = new MusicPage(PlaylistName);
                foreach (TrackData data in Tracks)
                {
                    Out.AddTrack(data.GetTrack());
                }
                Out.Refresh();
                return Out;
            }
            
            [Serializable]
            class TrackData
            {
                public String TrackPath;
                public String TrackName;

                public TrackData(String TrackPath, String TrackName)
                {
                    this.TrackPath = TrackPath;
                    this.TrackName = TrackName;
                }

                public Track GetTrack()
                {
                    Track Out = new Track(TrackPath);
                    Out.fileName = TrackName;
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
            FileStream fs = new FileStream(Name, FileMode.Open);

            if (!fs.CanRead)
            {
                return null;
            }
            
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

    }
}
