using System;
using System.Windows.Forms;

namespace KittenPlayer
{
    public class MusicPage : TabPage
    {
        public MusicTab musicTab = new MusicTab();

        public MusicPage()
        {
            UseVisualStyleBackColor = true;
            Controls.Add(musicTab);
            musicTab.Dock = DockStyle.Fill;
        }

        public MusicPage(string Name) : this()
        {
            Text = Name;
        }

        public void AddTrack(Track track)
        {
            musicTab.AddTrack(track);
        }

        public string GetSelectedTrackPath()
        {
            return musicTab.GetSelectedTrackPath();
        }

        public void DeleteSelectedTracks()
        {
            musicTab.RemoveSelectedTracks();
        }

        public void SelectAll()
        {
            musicTab.SelectAll();
        }

        public override void Refresh()
        {
            base.Refresh();
            musicTab.Refresh();
        }

        internal void RenameSelectedItem()
        {
            throw new NotImplementedException();
        }
    }
}