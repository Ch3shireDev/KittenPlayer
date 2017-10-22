using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;

namespace KittenPlayer
{
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public void SearchFor(String str)
        {
            Debug.WriteLine("Search");
            YoutubeDL yt = new YoutubeDL("\"ytsearch50: "+str+"\"");
            List<String> URLs = yt.Search("");
            
            using (WebClient client = new WebClient())
            {
                for(int i = 0; i < URLs.Count; i++)
                {
                    client.DownloadFile(@"https://i.ytimg.com/vi/"+URLs[i]+@"/hqdefault.jpg", @"./"+i+".jpg");
                    PictureBox picture = new PictureBox();
                    picture.ImageLocation = @"./" + i + ".jpg";
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    picture.Size = new Size(480 / 5, 360 / 5);
                    FlowLayoutPanel.Controls.Add(picture);
                }
            }

            //http://i.ytimg.com/vi/{video_id}/maxresdefault.jpg
            //http://img.youtube.com/vi/Ys5xfdn5rlo/2.jpg
            //https://i.ytimg.com/vi/Ys5xfdn5rlo/hqdefault.jpg?sqp=-oaymwEXCPYBEIoBSFryq4qpAwkIARUAAIhCGAE=&rs=AOn4CLDVDpwePx9tKLUbyaxWj9W8_YefaA
            //https://i.ytimg.com/vi/Ys5xfdn5rlo/hqdefault.jpg


        }
    }
}
