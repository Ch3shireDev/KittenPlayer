using System;
using System.Windows.Forms;


namespace KittenPlayer
{
    public partial class ResultsPage : UserControl
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        public void SearchFor(String str)
        {
            FlowPanel.Controls.Clear();
            SearchResult result = new SearchResult(str);
            int N = result.Tracks.Count;
            
            foreach(var track in result.Tracks)
            {
                Thumbnail thumbnail = new Thumbnail(track);
                FlowPanel.Controls.Add(thumbnail);
            }

        }
        
    }

}

