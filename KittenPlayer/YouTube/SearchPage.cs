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
using Newtonsoft.Json.Linq;


namespace KittenPlayer
{
    public partial class SearchPage : UserControl
    {
        public SearchPage()
        {
            InitializeComponent();
            //SearchFor("Dead Can Dance");
        }

        public void SearchFor(String str)
        {
            SearchResult result = new SearchResult(str);
            int N = result.Tracks.Count;


            ////Debug.WriteLine("Search");
            //YoutubeDL yt = new YoutubeDL("\"ytsearch10: " + str + "\"");
            //List<TrackObject> Tracks = yt.Search("");
            //int N = 50;
            //N = Tracks.Count;
            
            using (WebClient client = new WebClient())
            {
                for(int i = 0; i < N; i++)
                {
                    var track = result.Tracks[i];
                    String ID = track.ID;
                    String Title = track.Title;
                    client.DownloadFile(@"https://i.ytimg.com/vi/" + ID + @"/hqdefault.jpg", @"./" + i + ".jpg");

                    Thumbnail thumbnail = new Thumbnail(ID);
                    PictureBox picture = thumbnail.Picture;
                    picture.ImageLocation = System.IO.Directory.GetCurrentDirectory() + @"./" + i + ".jpg";
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    picture.Size = new Size(480 / 5, 360 / 5);
                    thumbnail.Margin = new Padding(0);
                    
                    thumbnail.Title.Text = Title;

                    FlowLayoutPanel.Controls.Add(thumbnail);
                }
            }

            //http://i.ytimg.com/vi/{video_id}/maxresdefault.jpg
            //http://img.youtube.com/vi/Ys5xfdn5rlo/2.jpg
            //https://i.ytimg.com/vi/Ys5xfdn5rlo/hqdefault.jpg?sqp=-oaymwEXCPYBEIoBSFryq4qpAwkIARUAAIhCGAE=&rs=AOn4CLDVDpwePx9tKLUbyaxWj9W8_YefaA
            //https://i.ytimg.com/vi/Ys5xfdn5rlo/hqdefault.jpg


        }
        
    }

}

