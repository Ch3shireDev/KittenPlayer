using System.Collections.Generic;
using System.Threading.Tasks;
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
                var output = new List<Thumbnail>();
                foreach (Thumbnail thumb in FlowPanel.Controls)
                    if (thumb.Selected) output.Add(thumb);
                return output;
            }
        }

        public async Task SearchFor(string str)
        {
            FlowPanel.Controls.Clear();
            var query = new SearchResult(str);
            var results = await query.GetResults();

            foreach (var result in results)
            {
                var thumbnail = new Thumbnail(result);
                FlowPanel.Controls.Add(thumbnail);
            }
        }

        public void SelectThumbnail(Thumbnail thumbnail)
        {
            var num = 0;
            var first = -1;
            var index = FlowPanel.Controls.IndexOf(thumbnail);
            if (index < 0) return;
            for (var i = 0; i < FlowPanel.Controls.Count; i++)
            {
                if (!(FlowPanel.Controls[i] is Thumbnail thumb) || !thumb.Selected) continue;
                if (num == 0) first = i;
                num++;
            }

            if (num == 0)
            {
                thumbnail.Selected = true;
                return;
            }

            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                DeselectAll();
                int a, b;
                if (index < first)
                {
                    a = index;
                    b = first;
                }
                else
                {
                    a = first;
                    b = index;
                }
                for (var i = a; i <= b; i++)
                    ((Thumbnail) FlowPanel.Controls[i]).Selected = true;
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
                if (control is Thumbnail thumb) thumb.Selected = false;
            }
        }
    }
}