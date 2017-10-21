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

        public void RemoveSelectedTracks()
        {
            List<int> SelectedIndices = new List<int>();
            foreach (int n in PlaylistView.SelectedIndices)
            {
                SelectedIndices.Add(n);
            }
            RemoveTrack(SelectedIndices);
            MainWindow.SavePlaylists();
        }

        public void RemoveTrack(List<int> Positions)
        {
            Positions.Sort();
            for (int i = 0; i < Positions.Count; i++)
            {
                RemoveTrack(Positions[i] - i);
            }
        }

        public void RemoveTrack(int Position)
        {
            if (Enumerable.Range(0, Tracks.Count).Contains(Position))
            {
                Tracks.RemoveAt(Position);
                PlaylistView.Items.RemoveAt(Position);
            }
            //Refresh();

        }

    }
}
