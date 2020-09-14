using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class Thumbnail : UserControl
    {
        public Thumbnail(Result track) :
            this(track.ID, track.Title, track.Playlist)
        {
        }

        public Thumbnail(string ID, string Title, string Playlist = "")
        {
            this.Title = Title;
            this.Playlist = Playlist;
            this.ID = ID;
            InitializeComponent();
            InitializeControls();
            Margin = new Padding(0);
            DownloadContent();
        }

        public string ID { get; set; } = "";

        private bool _isGrabbed { get; set; }

        public bool IsSelected { get; set; }
        public string Playlist { get; set; } = "";
        public string Title { get; set; } = "";


        public ResultsPage resultsPage => MainWindow.Instance.ResultsPage;

        public bool Selected
        {
            get => IsSelected;
            set
            {
                if (value)
                {
                    BackColor = SystemColors.Highlight;
                    IsSelected = true;
                }
                else
                {
                    BackColor = SystemColors.Control;
                    IsSelected = false;
                }
            }
        }

        private async Task DownloadContent()
        {
            if (!string.IsNullOrWhiteSpace(Playlist))
                TitleBox.Text = "[Playlist] ";

            TitleBox.Text += Title;

            await Task.Run(() =>
            {
                var client = new WebClient();
                client.DownloadFile(@"https://i.ytimg.com/vi/" + ID + @"/hqdefault.jpg",
                    Path.GetTempPath() + @"/" + ID + ".jpg");
            });

            Picture.ImageLocation = Path.GetTempPath() + @"/" + ID + ".jpg";
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Size = new Size(480 / 5, 360 / 5);
        }

        private void InitializeControls()
        {
            foreach (Control control in Controls)
            {
                control.Click += Clicked;
                control.MouseDown += Grab;
                control.MouseUp += Release;
                control.MouseMove += Moved;
                control.MouseDoubleClick += DoubleClick;
            }
        }

        private new void DoubleClick(object sender, MouseEventArgs e)
        {
            AddAndPlay();
        }

        private void AddAndPlay()
        {
            var thumbnails = new List<Thumbnail> {this};

            var tracksList = MainWindow.ActiveTab.DropThumbnail(thumbnails);
            MainWindow.ActiveTab.AddTrack(tracksList);
            if (tracksList.Count != 0)
                tracksList[0].MusicTab.Play(tracksList[0]);
        }

        private void Grab(object sender, EventArgs e)
        {
            _isGrabbed = true;
        }

        private void Release(object sender, EventArgs e)
        {
            _isGrabbed = false;
        }

        private void Clicked(object sender, EventArgs e)
        {
            resultsPage.SelectThumbnail(this);
        }

        private void Moved(object sender, EventArgs e)
        {
            if (!_isGrabbed) return;
            _isGrabbed = false;
            Selected = true;
            var thumbnails = MainWindow.Instance.ResultsPage.SelectedThumbnails;
            MainWindow.ActiveTab.PlaylistView.DoDragDrop(thumbnails, DragDropEffects.Move);
        }
    }
}