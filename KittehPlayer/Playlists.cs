using System;
using System.Text.RegularExpressions;
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

    class Track
    {
        public MusicPlayer musicPlayer = MusicPlayer.NewMusicPlayer();

        public String filePath;
        public String fileName;

        public Track() { }

        public Track(String filePath)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileNameWithoutExtension(filePath);

        }

        public String GetStringData()
        {
            return "// " + filePath + " // " + fileName + " //";
        }

        public void FromStringData(String Input)
        {
            Console.WriteLine("Input: " + Input);

            String pattern = @"// (.*) // (.*) //$";

            Match matches = Regex.Match(Input, pattern);


            if (matches.Groups.Count != 3)
            {
                Debug.WriteLine("Wrong filestring!");
                return;
            }

            this.filePath = matches.Groups[1].ToString();
            this.fileName = matches.Groups[2].ToString();

        }

        public void Play()
        {
            musicPlayer.Play(this.filePath);
        }

        public void Pause()
        {
            musicPlayer.Pause();
        }

        public void Stop()
        {
            musicPlayer.Stop();
        }
    }

    class PlaylistBox : ListBox
    {


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

        public void AddNewTrack(Track InTrack)
        {
            Tracks.Add(InTrack);
            this.Items.Add(InTrack.fileName);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        public void SaveToFile(String fileName, String playlistTitle)
        {
            var writer = new StreamWriter(File.OpenWrite(fileName));
            writer.WriteLine(playlistTitle);
            foreach(Track track in Tracks)
            {
                writer.WriteLine(track.GetStringData());
            }
            writer.Close();
            
        }

        public void LoadFromFile(String fileName, out String playlistTitle)
        {
            this.Tracks.Clear();
            var reader = new StreamReader(File.OpenRead(fileName));
            playlistTitle = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                String buffer = reader.ReadLine();
                Track track = new Track();
                track.FromStringData(buffer);
                AddNewTrack(track);
            }
            reader.Close();
        }


        public void ReloadList()
        {
            //this.LostB
            foreach(Track track in Tracks)
            {

            }
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
    
}
