using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

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
        public String Album;

        public Track() { }

        public Track(String filePath)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileNameWithoutExtension(filePath);
        }


        void GetMP3Metadata()
        {
            
            byte[] b = new byte[128];
            string sTitle;
            string sSinger;
            string sAlbum;
            string sYear;
            string sComm;

            FileStream fs = new FileStream(filePath, FileMode.Open);
            fs.Seek(-128, SeekOrigin.End);
            fs.Read(b, 0, 128);
            String sFlag = System.Text.Encoding.Default.GetString(b, 0, 3);
            
            sTitle = System.Text.Encoding.Default.GetString(b, 3, 30);
            sSinger = System.Text.Encoding.Default.GetString(b, 33, 30);
            sAlbum = System.Text.Encoding.Default.GetString(b, 63, 30);
            sYear = System.Text.Encoding.Default.GetString(b, 93, 4);
            sComm = System.Text.Encoding.Default.GetString(b, 97, 30);

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
