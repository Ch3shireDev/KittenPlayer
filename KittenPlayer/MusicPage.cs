using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace KittenPlayer
{
    class MusicPage : TabPage
    {

        public MusicTab musicTab = new MusicTab();
        
        public void SaveToFile(String FileName)
        {
            StreamWriter writer = new StreamWriter(FileName, false);
            writer.WriteLine(this.Text);
            foreach(Track track in musicTab.Tracks)
            {
                writer.WriteLine(track.GetStringData());
            }
            writer.Close();
        }

        public void LoadFromFile(String FileName)
        {
            StreamReader reader = new StreamReader(FileName);
            this.Text = reader.ReadLine();
            while(reader.Peek() > 0)
            {
                Track track = new Track();
                track.FromStringData(reader.ReadLine());
                AddTrack(track);
            }
            reader.Close();
        }

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
            musicTab.AddNewTrack(track);
        }

        public void AddTrack(String Name)
        {
            musicTab.AddNewTrack(Name);
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
