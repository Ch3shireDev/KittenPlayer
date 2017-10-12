using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

        public void AddTrack(String filePath, String fileName, int Position)
        {
            Track track = new Track(filePath);
            track.fileName = fileName;

            var item = new ListViewItem();
            item.Text = (Tracks.Count + 1).ToString();
            item.SubItems.Add(track.fileName);

            Tracks.Insert(Position, track);
            PlaylistView.Items.Insert(Position, item);
        }

        public void AddTrack(Track track, int Position = -1)
        {
            ListViewItem item = new ListViewItem();
            item.SubItems.Add(track.fileName);
            item.SubItems.Add(track.Album);
            if (Position >= 0 && Position < PlaylistView.Items.Count)
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

        //public void AddTrack(String filePath, int Position = -1)
        //{
        //    Track track = new Track(filePath);

        //    var item = new ListViewItem();
        //    item.Text = (Tracks.Count + 1).ToString();
        //    item.SubItems.Add(track.fileName);
        //    item.SubItems.Add(track.Album);

        //    Console.WriteLine(track.Album);

        //    if (Position > -1)
        //    {
        //        Tracks.Insert(Position, track);
        //        PlaylistView.Items.Insert(Position, item);
        //    }
        //    else
        //    {
        //        Tracks.Add(track);
        //        PlaylistView.Items.Add(item);
        //    }

        //}




        //void AddFilesToPage(List<String> FileList)
        //{
        //Debug.WriteLine("Add files");

        //List<Action> Actions = new List<Action>();
        //List<Action> Reversed = new List<Action>();

        //FileList.Sort();

        //int Position = PlaylistView.InsertionMark.Index;
        //if (Position > PlaylistView.Items.Count) Position = 0;

        //int Iteration = 0;

        //foreach (string filePath in FileList)
        //{
        //    if (Path.GetExtension(filePath) != ".mp3") continue;

        //    Action Redo = () => this.AddNewTrack(filePath, Position + Iteration);
        //    Action Undo = () => this.RemoveTrack(Position + Iteration);
        //    Actions.Add(Redo);
        //    Reversed.Add(Undo);
        //    Redo();
        //    Iteration++;
        //}

        //ActionsControl.Instance.AddActionsList(Actions, Reversed);
        //}


    }
}
