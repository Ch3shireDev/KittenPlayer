using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
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

  



        private void PlaylistView_Click(object sender, EventArgs e)
        {

            //PlaylistView.SelectedIndices.Add(0);
        }
        
        private void PlaylistView_DoubleClick(object sender, EventArgs e)
        {
            //int Index = PlaylistBox.SelectedIndex;
            //Tracks[Index].Play();
        }
        

        private void PlaylistView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var items = new List<ListViewItem>();
            items.Add((ListViewItem)e.Item);
            foreach (ListViewItem lvi in PlaylistView.SelectedItems)
            {
                if (!items.Contains(lvi))
                {
                    items.Add(lvi);
                }
            }
            PlaylistView.DoDragDrop(items, DragDropEffects.Move);
        }

        int prevItem = -1;

        private void PlaylistView_DragEnter(object sender, DragEventArgs e)
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

        private void PlaylistView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                var items = (List<ListViewItem>)e.Data.GetData(typeof(List<ListViewItem>));
                items.Reverse();



                foreach (ListViewItem lvi in items)
                {
                    int Position = PlaylistView.InsertionMark.Index + 1;
                    if (Position > PlaylistView.Items.Count) Position = 0;

                    PlaylistView.Items.Remove(lvi);
                    PlaylistView.Items.Insert(Position, lvi);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                List<Action> Actions = new List<Action>();
                List<Action> Reversed = new List<Action>();

                string[] FileList = e.Data.GetData(DataFormats.FileDrop, false) as string[];

                Debug.WriteLine(FileList.Length);

                foreach (string filePath in FileList)
                {
                    if (Path.GetExtension(filePath) != ".mp3") continue;

                    int Position = PlaylistView.InsertionMark.Index+1;
                    if (Position > PlaylistView.Items.Count) Position = 0;

                    Action Do = () => this.AddNewTrack(filePath, Position);
                    Action Redo = () => this.RemoveTrack(Position);
                    Actions.Add(Do);
                    Reversed.Add(Redo);
                    Do();
                }

                ActionsControl.Instance.AddActionsList(Actions, Reversed);
            }
        }
        
        public void AddNewTrack(String filePath, int Position = -1)
        {
            Track track = new Track(filePath);


            var item = new ListViewItem();
            item.Text = Tracks.Count.ToString();
            item.SubItems.Add(track.fileName);

            if (Position > -1)
            {
                Tracks.Insert(Position, track);
                PlaylistView.Items.Insert(Position, item);
            }
            else
            {
                Tracks.Add(track);
                PlaylistView.Items.Add(item);
            }

        }

        public void RemoveTrack(int Position)
        {
            Tracks.RemoveAt(Position);
            PlaylistView.Items.RemoveAt(Position);
        }
        
        private void PlaylistView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                e.Effect = DragDropEffects.Move;
            }


            Point mLoc = PlaylistView.PointToClient(Cursor.Position);
            var hitt = PlaylistView.HitTest(mLoc);
            if (hitt.Item == null) return;

            int idx = hitt.Item.Index;
            PlaylistView.InsertionMark.Index = idx;

            PlaylistView.InsertionMark.AppearsAfterItem = true;

            if (idx == prevItem) return;

            Application.DoEvents();
        }

        private void PlaylistView_DragLeave(object sender, EventArgs e)
        {
            PlaylistView.InsertionMark.Index = -1;
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
