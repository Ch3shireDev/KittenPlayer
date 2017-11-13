using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public MusicPage(String Name) : this()
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

        internal void RenameSelectedItem()
        {
            throw new NotImplementedException();
        }
    }
}