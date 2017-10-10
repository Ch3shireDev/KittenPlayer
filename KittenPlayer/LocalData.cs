using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;

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

        public void SavePlaylist(MusicPage musicPage, String Name)
        {
            FileStream fs = new FileStream(Name, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            
            Tuple<String, List<Track>> MusicTuple;
            MusicTuple = new Tuple<String, List<Track>>(musicPage.Text, musicPage.musicTab.Tracks);
            formatter.Serialize(fs, MusicTuple);
            fs.Close();

            fs = new FileStream(Name, FileMode.Open);
            var MT2 = formatter.Deserialize(fs) as Tuple<String,List<Track>>;
            fs.Close();

            Console.WriteLine(MusicTuple.Item1 + " " + MT2.Item1);
                
        }
        
        MusicPage LoadPlaylist(int i)
        {
            MusicPage Out = new MusicPage();
            String Name = GetFullPath(i);

            Console.WriteLine(Name);
            FileStream fs = new FileStream(Name, FileMode.Open);
            if (!fs.CanRead)
            {
                return null;
            }

            Tuple<String, List<Track>> MusicTuple;

            BinaryFormatter formatter = new BinaryFormatter();
            MusicTuple = formatter.Deserialize(fs) as Tuple<String, List<Track>>;
            fs.Close();
            if (MusicTuple == null) return null;
            
            Out.Text = MusicTuple.Item1;
            foreach (Track track in MusicTuple.Item2)
            {
                Out.musicTab.AddNewTrack(track);
            }
            Out.musicTab.Refresh();
            return Out;
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
