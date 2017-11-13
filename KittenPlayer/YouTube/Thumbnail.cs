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
        private static WebClient client = new WebClient();

        public String ID = "";
        public String Playlist = "";
        public String Title = "";

        public ResultsPage resultsPage => MainWindow.Instance.ResultsPage;

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
            List<Thumbnail> thumbnails = new List<Thumbnail> { this };

            List<Track> tracksList = MainWindow.ActiveTab.DropThumbnail(thumbnails);
            MainWindow.ActiveTab.AddTrack(tracksList);
            if (tracksList.Count != 0)
            {
                tracksList[0].MusicTab.Play(tracksList[0]);
            }
        }

        public Thumbnail(Result track) :
            this(track.ID, track.Title, track.Playlist)
        { }

        public Thumbnail(String ID, String Title, String Playlist = "")
        {
            this.Title = Title;
            this.Playlist = Playlist;
            this.ID = ID;
            InitializeComponent();
            InitializeControls();

            if (!String.IsNullOrWhiteSpace(this.Playlist))
                TitleBox.Text = "[Playlist] ";

            TitleBox.Text += Title;

            client.DownloadFile(@"https://i.ytimg.com/vi/" + ID + @"/hqdefault.jpg", Path.GetTempPath() + @"/" + ID + ".jpg");
            Picture.ImageLocation = Path.GetTempPath() + @"/" + ID + ".jpg";
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Size = new Size(480 / 5, 360 / 5);
            Margin = new Padding(0);
        }

        private bool isGrabbed = false;

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

        public bool isSelected = false;

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