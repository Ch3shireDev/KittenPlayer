using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {

        public void AddTrack(String filePath, String fileName = "", int Position = -1)
        {
            if (File.Exists(filePath)) return;
            if (IsDirectory(filePath))
            {
                string[] array = new string[] { filePath };
                AddTrack(array, Position);
            }
            else
            {
                Track track = new Track(filePath, fileName);
                AddTrack(track, Position);
            }
        }



        public void AddTrack(Track track, int Position = -1)
        {
            var item = track.GetListViewItem(PlaylistView);

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
            List<Track> Tracks = MakeTracksList(fileList);
            AddTrack(Tracks, Position);
        }

        public void AddTrack(List<Track> Tracks, int Position = -1)
        {
            if (Position >= 0 && Position < PlaylistView.Items.Count)
            {
                for (int i = 0; i < Tracks.Count; i++)
                    AddTrack(Tracks[i], Position + i);
            }
            else
            {
                foreach (Track track in Tracks)
                    AddTrack(track);
            }
        }

        public List<Track> AddDirectory(List<String> FilesArray)
        {
            FilesArray.Sort();
            List<String> NewList = new List<String>();
            List<String> FilesToAdd = new List<String>();
            foreach (String Path in FilesArray)
            {
                if (IsDirectory(Path))
                {
                    string[] FilesTab = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
                    foreach (string file in FilesTab)
                    {
                        if (IsMusicFile(file))
                            FilesToAdd.Add(file);
                    }
                }
                else if (IsMusicFile(Path))
                    NewList.Add(Path);
            }

            NewList.AddRange(FilesToAdd);

            List<Track> Tracks = new List<Track>();

            foreach(String file in NewList)
            {
                Track track = new Track(file);
                if (track.IsValid())
                    Tracks.Add(track);
            }

            return Tracks;
        }
        


    }
}
