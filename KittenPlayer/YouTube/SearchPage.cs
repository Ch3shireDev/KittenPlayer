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

        private void DownloadResults(string Query)
        {
            MainWindow.Instance.ResultsPage.SearchFor(Query);
        }
    }
}