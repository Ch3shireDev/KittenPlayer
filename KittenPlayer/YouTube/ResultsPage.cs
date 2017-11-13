using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace KittenPlayer
{
    public partial class ResultsPage : UserControl
    {
        public ResultsPage()
        {
            InitializeComponent();
        }

        public List<Thumbnail> SelectedThumbnails
        {
            get
            {
                var Output = new List<Thumbnail>();
                foreach (Thumbnail thumb in FlowPanel.Controls)
                    if (thumb.Selected) Output.Add(thumb);
                return Output;
            }
        }

        public async void SearchFor(string str)
        {
            FlowPanel.Controls.Clear();
            var Query = new SearchResult(str);
            var Results = await Query.GetResults();

            foreach (var result in Results)
            {
                var thumbnail = new Thumbnail(result);
                FlowPanel.Controls.Add(thumbnail);
            }
        }

        public void SelectThumbnail(Thumbnail thumbnail)
        {
            var Num = 0;
            int First = -1, Last = -1;
            var Index = FlowPanel.Controls.IndexOf(thumbnail);
            if (Index < 0) return;
            for (var i = 0; i < FlowPanel.Controls.Count; i++)
            {
                var thumb = FlowPanel.Controls[i] as Thumbnail;
                if (thumb.Selected)
                {
                    if (Num == 0) First = i;
                    Last = i;
                    Num++;
                }
            }

            if (Num == 0)
            {
                thumbnail.Selected = true;
                return;
            }

            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                DeselectAll();
                int a = 0, b = 0;
                if (Index < First)
                {
                    a = Index;
                    b = First;
                }
                else
                {
                    a = First;
                    b = Index;
                }
                for (var i = a; i <= b; i++)
                    (FlowPanel.Controls[i] as Thumbnail).Selected = true;
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                thumbnail.Selected = !thumbnail.Selected;
            }
            else
            {
                DeselectAll();
                thumbnail.Selected = true;
            }
        }

        public void DeselectAll()
        {
            foreach (Control control in FlowPanel.Controls)
            {
                var thumb = control as Thumbnail;
                thumb.Selected = false;
            }
        }
    }
}