using System;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace KittenPlayer
{
    public partial class Thumbnail : UserControl
    {
        static WebClient client = new WebClient();

        public String ID = "";
        public String Playlist = "";
        public String Title = "";

        void InitializeControls()
        {
            foreach (Control control in Controls)
            {
                control.Click += Clicked;
                control.MouseDown += Grab;
                control.MouseUp += Release;
                control.MouseMove += Moved;
            }
        }

        public Thumbnail(Result track): 
            this(track.Title, track.ID, track.Playlist) { }

        public Thumbnail(String Title, String ID, String Playlist = "")
        {
            this.Title = Title;
            this.Playlist = Playlist;
            this.ID = ID;
            InitializeComponent();
            InitializeControls();

            if (!String.IsNullOrWhiteSpace(this.Playlist))
                TitleBox.Text = "[Playlist] ";

            TitleBox.Text += Title;

            client.DownloadFile(@"https://i.ytimg.com/vi/" + ID + @"/hqdefault.jpg", @"./" + ID + ".jpg");
            Picture.ImageLocation = System.IO.Directory.GetCurrentDirectory() + @"./" + ID + ".jpg";
            Picture.SizeMode = PictureBoxSizeMode.Zoom;
            Picture.Size = new Size(480 / 5, 360 / 5);
            Margin = new Padding(0);
            
        }

        bool isGrabbed = false;

        void Grab(object sender, EventArgs e)
        {
            isGrabbed = true;
        }

        void Release(object sender, EventArgs e)
        {
            isGrabbed = false;
        }

        void Clicked(object sender, EventArgs e)
        {

            Debug.WriteLine(Playlist);
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

        void Moved(object sender, EventArgs e)
        {   
            if (isGrabbed)
            {
                isGrabbed = false;
                MainWindow.ActiveTab.PlaylistView.DoDragDrop(this, DragDropEffects.Move);
            }
        }
        
    }
}
