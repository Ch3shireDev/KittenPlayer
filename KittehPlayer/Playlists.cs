using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittehPlayer
{
    class Playlists
    {
        static Playlists Instance = null;

        private Playlists() { }

        static public Playlists NewPlaylists()
        {
            if(Instance == null)
            {
                Instance = new Playlists();
            }
            return Instance;
        }

        public List<Playlist> List = new List<Playlist>();
    }

    class Playlist
    {
        public String playlistName;
        public List<Track> trackList = new List<Track>();
        public ListBox listBoxReference;

        public Playlist(ListBox reference)
        {
            this.listBoxReference = reference;
            this.playlistName = reference.Parent.Text;
        }
    }

    class Track
    {
        public String filePath;
        public String fileName;

        public Track(String filePath)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileName(filePath);
        }
    }
}
