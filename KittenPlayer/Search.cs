using System;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class OnlineTracks
{
    static String Download(String name = "Dead Can Dance")
    {

        HttpWebRequest request = WebRequest.Create(@"https://www.youtube.com/results?search_query=" + name) as HttpWebRequest;

        request.MaximumAutomaticRedirections = 4;
        request.MaximumResponseHeadersLength = 4;

        request.Credentials = CredentialCache.DefaultCredentials;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;

        Stream receiveStream = response.GetResponseStream();

        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

        String stream = readStream.ReadToEnd();

        response.Close();
        readStream.Close();

        return stream;

    }

    public enum EType
    {
        None, Track, Playlist
    };

    public class OnlineTrack
    {
        public String link;
        public String title;
        public String playlist;
        public EType type = EType.None;

        public static bool IsMatch(String str)
        {
            return Regex.IsMatch(str, "yt-lockup-content");
        }

        public OnlineTrack(String str)
        {
            if (!IsMatch(str)) return;

            Match mWatch = Regex.Match(str, "watch\\?v=([^\"&]*)");

            if (mWatch.Success)
            {
                link = mWatch.Groups[1].ToString();
                Match mTitle = Regex.Match(str, "title=\"([^\"]*)");
                if (mTitle.Success)
                {
                    title = mTitle.Groups[1].ToString();
                }
                Match mPlaylist = Regex.Match(str, "list=([^\"]*)");
                if (mPlaylist.Success)
                {
                    playlist = mPlaylist.Groups[1].ToString();
                    type = EType.Playlist;
                }
                else
                {
                    type = EType.Track;
                }
            }
            else
            {
                type = EType.None;
            }
        }
    }
    
    public OnlineTracks(String Name)
    {
        String data = Download(Name);

        string[] lines = Regex.Split(data, @"\n");
        foreach (string str in lines)
        {
            OnlineTrack track = new OnlineTrack(str);
            if(track.type != EType.None)
            {
                Tracks.Add(track);
            }
        }
    }

    public List<OnlineTrack> Tracks = new List<OnlineTrack>();
}