using System.Text.RegularExpressions;

namespace KittenPlayer
{
    public class Result
    {
        public string ID;
        public string Playlist;
        public string Title;
        public EType Type = EType.None;

        public Result(string str)
        {
            if (!IsMatch(str)) return;
            var mWatch = Regex.Match(str, "watch\\?v=([^\"&]*)");
            if (mWatch.Success)
            {
                ID = mWatch.Groups[1].ToString();
                var mTitle = Regex.Match(str, "title=\"([^\"]*)");
                if (mTitle.Success) Title = mTitle.Groups[1].ToString();
                var mPlaylist = Regex.Match(str, "list=([^\"]*)");
                if (mPlaylist.Success)
                {
                    Playlist = mPlaylist.Groups[1].ToString();
                    Type = EType.Playlist;
                }
                else
                {
                    Type = EType.Track;
                }
            }
            else
            {
                Type = EType.None;
            }
        }

        public static bool IsMatch(string str)
        {
            return Regex.IsMatch(str, "yt-lockup-content");
        }
    }
}