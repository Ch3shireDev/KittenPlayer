using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Input;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class ResultsPage : UserControl
    {

        public List<Thumbnail> SelectedThumbnails
        {
            get
            {
                List<Thumbnail> Output = new List<Thumbnail>();
                foreach (Thumbnail thumb in FlowPanel.Controls)
                    if (thumb.Selected) Output.Add(thumb);
                return Output;
            }
        }

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
                Thumbnail thumbnail = new Thumbnail(result);
                FlowPanel.Controls.Add(thumbnail);
            }
        }

        public void SelectThumbnail(Thumbnail thumbnail)
        {
            int Num = 0;
            int First = -1, Last = -1;
            int Index = FlowPanel.Controls.IndexOf(thumbnail);
            if (Index < 0) return;
            for (int i = 0; i < FlowPanel.Controls.Count; i++)
            {
                Thumbnail thumb = FlowPanel.Controls[i] as Thumbnail;
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
                if (Index < First) { a = Index; b = First; }
                else { a = First; b = Index; }
                for (int i = a; i <= b; i++)
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

        private void FlowPanel_Leave(object sender, EventArgs e)
        {
            DeselectAll();
        }

        void DeselectAll()
        {
            foreach (Control control in FlowPanel.Controls)
            {
                Thumbnail thumb = control as Thumbnail;
                thumb.Selected = false;
            }
        }
    }

}

