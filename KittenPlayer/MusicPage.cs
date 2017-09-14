using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Diagnostics;

namespace KittenPlayer
{
    class MusicPage : TabPage
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

        public void AddTrack(String Name)
        {
            musicTab.AddNewTrack(Name);
        }

        public String GetSelectedTrackPath()
        {
            return musicTab.GetSelectedTrackPath();
        }
    }
}
