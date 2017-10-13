using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

        public void AddTrack(String filePath, String fileName = "", int Position = -1)
        {
            Track track = new Track(filePath, fileName);
            AddTrack(track, Position);

            //var item = new ListViewItem();
            //item.Text = track.Number.ToString();
            //item.SubItems.Add(track.fileName);
            //Tracks.Insert(Position, track);
            //PlaylistView.Items.Insert(Position, item);
        }

        public void AddTrack(Track track, int Position = -1)
        {
            ListViewItem item = new ListViewItem();
            item.Text = track.Number.ToString();
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

        public void AddTrack(string[] fileNames, int Position = -1)
        {
            List<String> fileList = new List<String>(fileNames);
            fileList.Sort();
            AddTrack(fileList, Position);
        }

        public void AddTrack(List<String> fileList, int Position = -1)
        {
            List<Track> Tracks = new List<Track>();
            foreach(String file in fileList)
            {
                Track track = new Track(file);
                if (track.IsValid())
                {
                    Tracks.Add(track);
                }
            }

            AddTrack(Tracks, Position);
        }

        void AddTrack(List<Track> Tracks, int Position = -1)
        {
            if (Position >= 0 && Position < PlaylistView.Items.Count)
            {
                for (int i = 0; i < Tracks.Count; i++)
                {
                    AddTrack(Tracks[i], Position + i);
                }
            }
            else
            {
                foreach (Track track in Tracks)
                {
                    AddTrack(track);
                }
            }
        }
        


    }
}
