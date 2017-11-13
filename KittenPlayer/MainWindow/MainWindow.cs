using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class MainWindow : IKittenInterface
    {
        private void DeletePlaylist(object sender, EventArgs e)
        {
            int Index = MainTab.MainTab.SelectedIndex;
            Debug.WriteLine("Remove tab: "+MainTab.MainTab.TabPages[Index].Text);
            //MainTab.MainTab.Controls.RemoveAt(Index);
            for (int i = 0; i < MainTab.MainTab.Controls.Count; i++)
            {
                Debug.WriteLine(i+". "+MainTab.MainTab.Controls[i].Text+" - " + MainTab.MainTab.TabPages[i].Text);
            }
        }

        private readonly ActionsControl _actionsControl = ActionsControl.GetInstance();

        private readonly MusicPlayer _musicPlayer = MusicPlayer.Instance;

        private Form _about;
        public Options Options = new Options();
        private RenameBox _renameBox = null;

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            InitializeTrackbarTimer();

            //add current version
            Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public static MusicPage ActivePage => GetActivePage();
        public static MusicTab ActiveTab => ActivePage?.musicTab;

        public static MainWindow Instance { get; private set; }

        [STAThread]
        private static void Main()
        {
            //TextWriterTraceListener[] listeners = new TextWriterTraceListener[]
            //{
            //    new TextWriterTraceListener("log.txt"),
            //    new TextWriterTraceListener(Console.Out)
            //};
            //Debug.Listeners.AddRange(listeners);
            //Debug.WriteLine("Some Value", "Some Category");
            //Debug.Flush();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        private static MusicPage GetActivePage()
        {
            if (Instance.MainTab.MainTab.Controls.Count == 0)
                Instance.MainTab.AddNewTab("New Tab");
            var Index = Instance.MainTab.MainTab.SelectedIndex;
            return Instance.MainTab.MainTab.Controls[Index] as MusicPage;
        }

        public static void SavePlaylists() => LocalData.Instance.SavePlaylists(MainTabs.Instance);

        private void MainWindow_Click(object sender, EventArgs e) => Focus();

        public void RenameTab() => _renameBox = new RenameBox(MainTabs.Instance);

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e) => RenameTab();

        public void AddNewPlaylistToolStripMenuItem_Click(object sender, EventArgs e) => Instance.MainTab.AddNewTabAndRename();

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e) => _actionsControl.Undo();

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e) => _actionsControl.Redo();

        public string GetSelectedTrackPath()
        {
            var Path = (MainTab.Controls[MainTab.MainTab.SelectedIndex] as MusicPage).GetSelectedTrackPath();
            return Path;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) => (MainTab.MainTab.SelectedTab as MusicPage).DeleteSelectedTracks();

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(MainTab.MainTab.SelectedTab is MusicPage currentPage)) return;
            if (currentPage.musicTab?.PlaylistView == null) return;
            if (currentPage.musicTab.PlaylistView.Focused)
                currentPage.SelectAll();
            else if (Instance.searchBarPage.searchBar.Focused)
                Instance.searchBarPage.searchBar.SelectAll();
        }

        private void addYoutubePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddPlaylistForm();
            form.ShowDialog();
            var Tracks = form.Tracks;
            var currentPage = MainTab.MainTab.SelectedTab as MusicPage;
            currentPage.musicTab.AddTrack(Tracks);
            SavePlaylists();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_about != null) return;
            _about = new AboutForm();
            _about.Show();
            _about.Focus();
        }

        private void SavePlaylistsToolStripMenuItem_Click(object sender, EventArgs e) => SavePlaylists();

        private void LayoutPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                AddPlaylistStrip.Show(LayoutPanel.PointToScreen(e.Location));
            }
        }

        private void RenameToolStripMenuItem1_Click(object sender, EventArgs e) => RenameSelectedItem();

        private void ConvertToMp3ToolStripMenuItem_Click(object sender, EventArgs e) => ConvertToMp3();

        public static void ConvertToMp3()
        {
            if (Instance.MainTab.MainTab.SelectedTab is MusicPage page)
                page.musicTab.ConvertToMp3();
        }
    }
}