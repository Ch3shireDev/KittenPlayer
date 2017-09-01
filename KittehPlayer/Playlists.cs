using System;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittehPlayer
{
    class Aux
    {

    }

    class PlaylistBox : ListBox
    {

        class Track
        {
            public String filePath;
            public String fileName;

            public Track(String filePath)
            {
                this.filePath = filePath;
                this.fileName = Path.GetFileNameWithoutExtension(filePath);
                
            }
        }

        List<Track> Tracks = new List<Track>();
        
        public String GetDirectory(int Index)
        {
            return Tracks[Index].filePath;
        }

        public void AddNewTrack(String filePath)
        {
            Track track = new Track(filePath);
            Tracks.Add(track);
            this.Items.Add(track.fileName);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }


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
