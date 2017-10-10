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
            try
            {
                Tuple<String, List<Track>> MusicTuple;
                MusicTuple = new Tuple<String, List<Track>>(musicPage.Text, musicPage.musicTab.Tracks);
                formatter.Serialize(fs, MusicTuple);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        String GetFullPath(int i)
        {
            return Path + i + ".dat";
        }

        public void LoadPlaylists(TabControl MainTabs)
        {
            for(int i = 0; i < Num(); i++)
            {
                LoadPlaylist(i);
            }
        }

        void LoadPlaylist(int i)
        {

        }

        public int Num()
        {
            return Directory.GetFiles(Path, "*.dat", SearchOption.TopDirectoryOnly).Length;
        }

    }
}
