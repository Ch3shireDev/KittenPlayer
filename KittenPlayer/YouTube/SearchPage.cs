using System;
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

        public void searchBar_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (searchBar.Text != "")
                {
                    MainWindow.Instance.LayoutPanel.RowStyles[2].Height = 200;
                    DownloadResults(searchBar.Text);
                }
                else
                {
                    MainWindow.Instance.LayoutPanel.RowStyles[2].Height = 0;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                searchBar.Text = "";
                MainWindow.Instance.LayoutPanel.RowStyles[2].Height = 0;
                ActiveControl = null;
            }
        }

        private void DownloadResults(String Query)
        {
            MainWindow.Instance.ResultsPage.SearchFor(Query);
        }
    }
}