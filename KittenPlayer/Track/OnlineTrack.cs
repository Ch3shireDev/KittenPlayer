using System;
using System.Diagnostics;

namespace KittenPlayer
{
    class OnlineTrack : Track
    {
        public String YoutubeID = "";

        public override void Play()
        {
            DownloadTrack();
        }

        public OnlineTrack(String ID, String Title = "")
        {
            base.Title = Title;
            base.fileName = Title;
            this.YoutubeID = ID;
        }

        public void DownloadTrack()
        {

        }
    }
}
