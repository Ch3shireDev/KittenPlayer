using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace KittenPlayer
{
    public partial class ResultsPage : UserControl
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        public List<Thumbnail> SearchFor(String str)
        {
            FlowPanel.Controls.Clear();
            SearchResult result = new SearchResult(str);
            int N = result.Tracks.Count;
            List<Thumbnail> Thumbnails = new List<Thumbnail>();

            foreach(var track in result.Tracks)
                Thumbnails.Add(new Thumbnail(track));

            return Thumbnails;
        }
        
    }

}

