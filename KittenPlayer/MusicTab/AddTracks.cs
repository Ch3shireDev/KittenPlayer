using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MusicTab : UserControl
    {
        //public void AddTrack(String filePath, String fileName = "", int Position = -1)
        //{
        //    if (File.Exists(filePath)) return;
        //    if (IsDirectory(filePath))
        //    {
        //        string[] array = new string[] { filePath };
        //        AddTrack(array, Position);
        //    }
        //    else
        //    {
        //        Track track = new Track(filePath, fileName);
        //        AddTrack(track, Position);
        //    }
        //}

        public void AddTrack(Track track, int Position = -1)
        {
            var item = track.GetListViewItem(PlaylistView);
            track.MusicTab = this;

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
            var fileList = new List<string>(fileNames);
            fileList.Sort();
            AddTrack(fileList, Position);
        }

        public void AddTrack(List<string> fileList, int Position = -1)
        {
            var Tracks = MakeTracksList(fileList);
            AddTrack(Tracks, Position);
        }

        public void AddTrack(List<Track> Tracks, int Position = -1)
        {
            if (Position >= 0 && Position < PlaylistView.Items.Count)
                for (var i = 0; i < Tracks.Count; i++)
                    AddTrack(Tracks[i], Position + i);
            else
                foreach (var track in Tracks)
                    AddTrack(track);
        }

        public List<Track> AddDirectory(List<string> FilesArray)
        {
            FilesArray.Sort();
            var NewList = new List<string>();
            var FilesToAdd = new List<string>();
            foreach (var Path in FilesArray)
                if (IsDirectory(Path))
                {
                    var FilesTab = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
                    foreach (var file in FilesTab)
                        if (IsMusicFile(file))
                            FilesToAdd.Add(file);
                }
                else if (IsMusicFile(Path))
                {
                    NewList.Add(Path);
                }

            NewList.AddRange(FilesToAdd);

            var Tracks = new List<Track>();

            foreach (var file in NewList)
            {
                var track = new Track(file);
                if (track.IsValid())
                    Tracks.Add(track);
            }

            return Tracks;
        }
    }
}