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

        public async void SearchFor(String str)
        {
            FlowPanel.Controls.Clear();
            SearchResult Query = new SearchResult(str);
            var Results = await Query.GetResults();

            foreach (var result in Results)
            {
                Thumbnail thumbnail = new Thumbnail(result, this);
                FlowPanel.Controls.Add(thumbnail);
            }
        }

        public void SelectThumbnail(Thumbnail thumbnail)
        {
            foreach(Control control in FlowPanel.Controls)
            {
                Thumbnail thumb = control as Thumbnail;
                thumb.Selected = false;
            }
            thumbnail.Selected = true;
        }
        
    }

}

