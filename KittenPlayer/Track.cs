using System;
using System.IO;

namespace KittenPlayer
{
    [Serializable]
    public class Track
    {
        public MusicPlayer musicPlayer
        {
            get { return MusicPlayer.GetInstance(); }
        }

        public String filePath;
        public String fileName;

        public String Artist;
        public String Album;
        public String Title;
        public uint Number;

        public Track() { }

        public Track(String filePath)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileNameWithoutExtension(filePath);
            GetMP3Metadata();
        }


        void GetMP3Metadata()
        {
            TagLib.File f = TagLib.File.Create(filePath);
            this.Artist = f.Tag.FirstPerformer;
            this.Album = f.Tag.Album;
            this.Title = f.Tag.Title;
            this.Number = f.Tag.Track;
        }

        //public void FromStringData(String Input)
        //{
        //    Console.WriteLine("Input: " + Input);

        //    String pattern = @"// (.*) // (.*) //$";

        //    Match matches = Regex.Match(Input, pattern);


        //    if (matches.Groups.Count != 3)
        //    {
        //        Debug.WriteLine("Wrong filestring!");
        //        return;
        //    }

        //    this.filePath = matches.Groups[1].ToString();
        //    this.fileName = matches.Groups[2].ToString();

        //}

        public void Play()
        {
            musicPlayer.Play(this.filePath);
        }

        public void Pause()
        {
            musicPlayer.Pause();
        }

        public void Stop()
        {
            musicPlayer.Stop();
        }
    }
}
