using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
        public List<Track> Tracks = new List<Track>();

        MusicPlayer musicPlayer = MusicPlayer.GetInstance();

        public MusicTab()
        {
            InitializeComponent();
            
        }

        public String GetSelectedTrackPath()
        {
            if (PlaylistView.SelectedIndices.Count == 0)
            {
                return "";
            }
            else
            {
                int Index = PlaylistView.SelectedIndices[0];
                return Tracks[Index].filePath;
            }
        }


        private void PlaylistView_Click(object sender, EventArgs e)
        {

        }
        
        private void PlaylistView_DoubleClick(object sender, EventArgs e)
        {
            PlaySelectedTrack();
        }
        
        public void PlaySelectedTrack()
        {

            if (PlaylistView.SelectedIndices.Count > 0)
            {
                int Index = PlaylistView.SelectedIndices[0];
                musicPlayer.CurrentTab = this;
                musicPlayer.CurrentTrack = Index;
                Tracks[Index].Play();
            }
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

                SortFiles(ref FileList);

                int Position = PlaylistView.InsertionMark.Index;
                if (Position > PlaylistView.Items.Count) Position = 0;

                int Iteration = 0;

                foreach (string filePath in FileList)
                {
                    if (Path.GetExtension(filePath) != ".mp3") continue;
                    
                    Action Redo = () => this.AddNewTrack(filePath, Position + Iteration);
                    Action Undo = () => this.RemoveTrack(Position + Iteration);
                    Actions.Add(Redo);
                    Reversed.Add(Undo);
                    Redo();
                    Iteration++;
                }

                ActionsControl.Instance.AddActionsList(Actions, Reversed);
            }
        }


        void RemoveSelectedTracks()
        {
            
            //List<Action> RedoActions = new List<Action>();
            //List<Action> UndoActions = new List<Action>();

            //int Iteration = 0;
            //foreach (int Index in PlaylistView.SelectedIndices)
            //{
            //    int Position = Index - Iteration;

            //    Track track = Tracks[Position];
                
            //    String filePath = track.filePath;
            //    String fileName = track.fileName;

            //    Action Redo = () => this.RemoveTrack(Index - Iteration);
            //    Action Undo = () => this.AddNewTrack(filePath, fileName, Index);

            //    RedoActions.Add(Redo);
            //    UndoActions.Add(Undo);

            //    Redo();
            //    Iteration++;
            //}

            //ActionsControl.Instance.AddActionsList(RedoActions, UndoActions);
            
        }




        public void SortFiles(ref string[] FileList)
        {
            List<String> lista = new List<String>(FileList);
            lista.Sort();
            FileList = lista.ToArray();

            foreach(string file in FileList)
            {
                Debug.WriteLine(file);
            }
        }

        public void AddNewTrack(String filePath, String fileName, int Position)
        {
            Track track = new Track(filePath);
            track.fileName = fileName;

            var item = new ListViewItem();
            item.Text = (Tracks.Count + 1).ToString();
            item.SubItems.Add(track.fileName);

            Tracks.Insert(Position, track);
            PlaylistView.Items.Insert(Position, item);
        }

        public void AddNewTrack(String filePath, int Position = -1)
        {
            Track track = new Track(filePath);


            var item = new ListViewItem();
            item.Text = (Tracks.Count+1).ToString();
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
            if (Enumerable.Range(0, Tracks.Count).Contains(Position))
            {
                Tracks.RemoveAt(Position);
                PlaylistView.Items.RemoveAt(Position);
            }
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

        private void MusicTab_DoubleClick(object sender, EventArgs e)
        {
            if(PlaylistView.TabIndex >= 0 && PlaylistView.TabIndex < PlaylistView.Items.Count)
            {
                musicPlayer.Play(Tracks[PlaylistView.TabIndex].filePath);
            }
        }

        private void MusicTab_Click(object sender, EventArgs e)
        {
        }

        private void PlaylistView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PlaylistView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PlaySelectedTrack();
            }
            else if (e.KeyChar == (char)Keys.Space)
            {
                MusicPlayer player = MusicPlayer.GetInstance();
                if (MusicPlayer.IsPlaying)
                {
                    MusicPlayer.GetInstance().Pause();
                }
            }

            if (e.KeyChar == (char)Keys.Down)
            {
                Debug.WriteLine("del");
                RemoveSelectedTracks();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlaylistView_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                DropDownMenu.Show(Cursor.Position);
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            PlaySelectedTrack();
            DropDownMenu.Hide();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.GetInstance().Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MusicPlayer.GetInstance().Stop();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedTracks();
        }

        private void PlaylistView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                RemoveSelectedTracks();
            }
        }
    }




    public class Track
    {
        public MusicPlayer musicPlayer = MusicPlayer.GetInstance();

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