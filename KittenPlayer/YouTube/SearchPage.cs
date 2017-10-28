using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            searchBar.ShortcutsEnabled = true;
        }

        private void SearchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void searchBar_Enter(object sender, EventArgs e)
        {
        }

        private void searchBar_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void searchBar_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (searchBar.Text != "")
                {
                    DownloadResults(searchBar.Text);
                }
            }
        }

        private async void DownloadResults(String Query)
        {

            List<Thumbnail> Thumbnails = await Task<List<Thumbnail>>.Factory.StartNew(() => { return MainWindow.Instance.ResultsPage.SearchFor(searchBar.Text); });
            foreach (Thumbnail thumbnail in Thumbnails)
            {
                MainWindow.Instance.ResultsPage.FlowPanel.Controls.Add(thumbnail);
            }
        }
    }
}
