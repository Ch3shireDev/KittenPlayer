using System;
using System.Windows.Forms;


namespace KittenPlayer
{
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public void SearchFor(String str)
        {
            SearchResult result = new SearchResult(str);
            int N = result.Tracks.Count;
            
            foreach(var track in result.Tracks)
            {
                Thumbnail thumbnail = new Thumbnail(track);
                FlowLayoutPanel.Controls.Add(thumbnail);
            }

        }
        
    }

}

