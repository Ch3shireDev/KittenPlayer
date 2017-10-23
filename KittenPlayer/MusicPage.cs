using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace KittenPlayer
{
    public class MusicPage : TabPage
    {

        public MusicTab musicTab = new MusicTab();
       
        public MusicPage()
        {
            this.UseVisualStyleBackColor = true;
            this.Controls.Add(musicTab);
            musicTab.Dock = DockStyle.Fill;
        }

        public MusicPage(String Name):this()
        {
            this.Text = Name;
        }

        private void InitializeComponent()
        {
            
        }

        public void AddTrack(Track track)
        {
            musicTab.AddTrack(track);
        }
        
        public String GetSelectedTrackPath()
        {
            return musicTab.GetSelectedTrackPath();
        }

        public void DeleteSelectedTracks()
        {
            musicTab.RemoveSelectedTracks();
        }

        public void DeleteTracks(List<Track> Tracks)
        {
            //musicTab.RemoveTrack(Tracks);
        }

        public void SelectAll()
        {
            musicTab.SelectAll();
        }

        public override void Refresh()
        {
            base.Refresh();
            this.musicTab.Refresh();
        }
    }
}
