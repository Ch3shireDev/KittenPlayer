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
    public partial class MusicTab : UserControl
    {
        List<Track> Tracks = new List<Track>();

        public MusicTab()
        {
            InitializeComponent();
            
        }

        private void PlaylistBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PlaylistBox_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is ListBox)
            {
                ListBox playlist = sender as ListBox;

                List<Action> Actions = new List<Action>();
                List<Action> Reversed = new List<Action>();

                string[] FileList = e.Data.GetData(DataFormats.FileDrop, false) as string[];
                foreach (string filePath in FileList)
                {
                    int Position = Tracks.Count;

                    Action Do = () => this.AddNewTrack(filePath);
                    Action Redo = () => this.RemoveTrack(Position);
                    Actions.Add(Do);
                    Reversed.Add(Redo);
                    Do();
                }

                ActionsControl.Instance.AddActionsList(Actions, Reversed);

            }
        }

        public void AddNewTrack(String filePath)
        {
            Track track = new Track(filePath);
            Tracks.Add(track);
            PlaylistBox.Items.Add(track.fileName);
        }

        public void RemoveTrack(int Position)
        {
            Tracks.RemoveAt(Position);
            PlaylistBox.Items.RemoveAt(Position);
        }

        private void PlaylistBox_Click(object sender, EventArgs e)
        {

        }

        private void PlaylistBox_DoubleClick(object sender, EventArgs e)
        {
            int Index = PlaylistBox.SelectedIndex;
            Tracks[Index].Play();
        }
    }




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


}


/*

namespace KittehPlayer
{



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
        

        public void SaveToFile(String fileName, String playlistTitle)
        {
            var writer = new StreamWriter(File.OpenWrite(fileName));
            writer.WriteLine(playlistTitle);
            foreach (Track track in Tracks)
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
            foreach (Track track in Tracks)
            {

            }
        }
        
    }


}
*/
