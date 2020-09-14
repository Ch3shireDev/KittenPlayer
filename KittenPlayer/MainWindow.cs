using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
// using KittenLibrary;

namespace KittenPlayer
{
    public partial class MainWindow : Form, IKittenInterface
    {
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            InitializeTrackbarTimer();

            //add current version
            Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
            defaultTitle = Text;
        }

        private ActionsControl _actionsControl { get; } = ActionsControl.GetInstance();

        private MusicPlayer _musicPlayer { get; } = MusicPlayer.Instance;


        private Timer trackbarTimer { get; } = new Timer();

        private Form _about { get; set; }

        private DownloadVideoForm DownloadVideo { get; set; }
        private bool IsHolding { get; set; }
        public Options Options { get; set; } = new Options();

        private string defaultTitle { get; }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public static MusicPage ActivePage => GetActivePage();
        public static MusicTab ActiveTab => ActivePage?.musicTab;

        public static MainWindow Instance { get; private set; }

        public void RenameSelectedItem()
        {
            Debug.WriteLine("From " + this + " to " + ActiveControl);
            if (ActiveControl is IKittenInterface tab)
            {
                Debug.WriteLine(tab + " is Interface");
                tab.RenameSelectedItem();
            }
        }

        private void DeletePlaylist(object sender, EventArgs e)
        {
            var index = MainTab.MainTab.SelectedIndex;
            MainTab.MainTab.Controls.RemoveAt(index);
            MainTab.MainTab.SelectedIndex = index > 0 ? index - 1 : 0;
        }

        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private void volumeBar_ValueChanged(object sender, EventArgs e)
        {
            _musicPlayer.Volume = volumeBar.Value * 1.0 / volumeBar.Maximum;
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var Window = new MainWindow();
            if (Window.IsDisposed) return;
            Application.Run(Window);
        }

        private static MusicPage GetActivePage()
        {
            if (Instance.MainTab.MainTab.Controls.Count == 0)
                Instance.MainTab.AddNewTab("New Tab");
            var Index = Instance.MainTab.MainTab.SelectedIndex;
            return Instance.MainTab.MainTab.Controls[Index] as MusicPage;
        }

        public static void SavePlaylists()
        {
            LocalData.Instance.SavePlaylists(MainTabs.Instance);
        }

        private void MainWindow_Click(object sender, EventArgs e)
        {
            Focus();
        }

        public void RenameTab()
        {
            var renameBox = new RenameBox(MainTabs.Instance);
        }

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameTab();
        }

        public void AddNewPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instance.MainTab.AddNewTabAndRename();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _actionsControl.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _actionsControl.Redo();
        }

        public string GetSelectedTrackPath()
        {
            var Path = (MainTab.Controls[MainTab.MainTab.SelectedIndex] as MusicPage).GetSelectedTrackPath();
            return Path;
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (MainTab.MainTab.SelectedTab as MusicPage).DeleteSelectedTracks();
        }

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

        private void SavePlaylistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePlaylists();
        }

        private void LayoutPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) AddPlaylistStrip.Show(LayoutPanel.PointToScreen(e.Location));
        }

        private void RenameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RenameSelectedItem();
        }

        private void ConvertToMp3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertToMp3();
        }

        public static void ConvertToMp3()
        {
            if (Instance.MainTab.MainTab.SelectedTab is MusicPage page)
                page.musicTab.ConvertToMp3();
        }

        public void SetDefaultTitle()
        {
            Text = defaultTitle;
        }

        private void DownloadVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DownloadVideo == null) DownloadVideo = new DownloadVideoForm();
            DownloadVideo.Show();
            DownloadVideo.Focus();
        }

        public void ShowMesssage(string s)
        {
            MessageBox.Show(s, "Kitten Player");
            //throw new NotImplementedException();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var CurrentTab = MainTab.MainTab.SelectedTab as MusicPage;
                foreach (var str in openFileDialog.FileNames)
                    Debug.WriteLine(str);
                CurrentTab.musicTab.AddTrack(openFileDialog.FileNames);
            }

            SavePlaylists();
        }

        private void addDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.ShowNewFolderButton = false;
            var result = folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK) return;
            //{
            var CurrentTab = MainTab.MainTab.SelectedTab as MusicPage;

            string[] FileNames = {folderBrowserDialog.SelectedPath};

            //    Debug.WriteLine(folderBrowserDialog.SelectedPath);

            //    CurrentTab.musicTab.AddTrack(FileNames);
            //}
            //SavePlaylists();

            var trackList = MusicTab.MakeTracksList(FileNames);
            CurrentTab?.musicTab?.AddTrack(trackList);
            SavePlaylists();
        }

        public void InitializeTrackbarTimer()
        {
            trackbarTimer.Tick += trackbarTimer_Tick;

            trackbarTimer.Interval = 500;
            trackbarTimer.Enabled = true;
            trackbarTimer.Start();
        }

        private void trackbarTimer_Tick(object sender, EventArgs e)
        {
            if (IsHolding) return;

            if (_musicPlayer.IsPlaying)
                SetTrackbarTime();
        }

        public void SetTrackbarTime()
        {
            var min = trackBar.Minimum;
            var max = trackBar.Maximum;
            var alpha = _musicPlayer.Progress;
            if (alpha < 0 || alpha > 1) return;
            if (double.IsNaN(alpha)) return;
            trackBar.Value = (int) Math.Floor(min + alpha * (max - min));
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
        }

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            IsHolding = true;
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            IsHolding = false;
            if (!_musicPlayer.IsPlaying) return;

            var min = trackBar.Minimum;
            var max = trackBar.Maximum;
            var val = trackBar.Value;

            var valMouse = e.X / 2;
            //trackBar.

            Debug.WriteLine("Values: " + val + " " + valMouse);

            var alpha = (double) (val - min) / (max - min);

            _musicPlayer.Progress = alpha;
        }
    }
}