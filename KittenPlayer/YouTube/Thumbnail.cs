using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class Thumbnail : UserControl
    {
        private static readonly WebClient client = new WebClient();

        public string ID = "";

        private bool isGrabbed;

        public bool isSelected;
        public string Playlist = "";
        public string Title = "";

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

            if (!string.IsNullOrWhiteSpace(this.Playlist))
                TitleBox.Text = "[Playlist] ";

            TitleBox.Text += Title;

            client.DownloadFile(@"https://i.ytimg.com/vi/" + ID + @"/hqdefault.jpg",
                Path.GetTempPath() + @"/" + ID + ".jpg");
            Picture.ImageLocation = Path.GetTempPath() + @"/" + ID + ".jpg";
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Size = new Size(480 / 5, 360 / 5);
            Margin = new Padding(0);
        }

        public ResultsPage resultsPage => MainWindow.Instance.ResultsPage;

        public bool Selected
        {
            get => isSelected;
            set
            {
                if (value)
                {
                    BackColor = SystemColors.Highlight;
                    isSelected = true;
                }
                else
                {
                    BackColor = SystemColors.Control;
                    isSelected = false;
                }
            }
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

        private void DoubleClick(object sender, MouseEventArgs e)
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
            isGrabbed = true;
        }

        private void Release(object sender, EventArgs e)
        {
            isGrabbed = false;
        }

        private void Clicked(object sender, EventArgs e)
        {
            resultsPage.SelectThumbnail(this);
        }

        private void Moved(object sender, EventArgs e)
        {
            if (isGrabbed)
            {
                isGrabbed = false;
                Selected = true;
                var Thumbnails = MainWindow.Instance.ResultsPage.SelectedThumbnails;
                MainWindow.ActiveTab.PlaylistView.DoDragDrop(Thumbnails, DragDropEffects.Move);
            }
        }
    }
}