using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KittenPlayer
{

    public partial class MusicTab : UserControl
    {

        private void PlaylistView_DragDrop(object sender, DragEventArgs e)
        {
            List<Track> tracksList = new List<Track>();

            if (e.Data.GetDataPresent(typeof(List<ListViewItem>)))
            {
                List<ListViewItem> ItemsList = e.Data.GetData(typeof(List<ListViewItem>)) as List<ListViewItem>;
                tracksList = MakeTracksList(ItemsList);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] FilesArray = e.Data.GetData(DataFormats.FileDrop, false) as string[];
                tracksList = MakeTracksList(FilesArray);
            }

            foreach (Track track in tracksList)
            {
                AddNewTrack(track);
            }
        }

        List<Track> MakeTracksList(string[] FilesArray)
        {
            List<Track> Tracks = new List<Track>();

            foreach (String file in FilesArray)
            {
                Tracks.Add(new Track(file));
            }

            return Tracks;
        }

        List<Track> MakeTracksList(List<ListViewItem> Items)
        {
            List<Track> TracksList = new List<Track>();

            foreach (ListViewItem item in Items)
            {
                int Position = PlaylistView.Items.IndexOf(item);
                PlaylistView.Items.Remove(item);
                Track track = Tracks[Position];
                TracksList.Add(track);
                Tracks.Remove(track);
            }

            return TracksList;
        }
    }
}
