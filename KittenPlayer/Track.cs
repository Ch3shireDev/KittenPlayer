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

        public Track() { }

        public Track(String filePath)
        {
            this.filePath = filePath;
            this.fileName = Path.GetFileNameWithoutExtension(filePath);
        }

        public String GetStringData()
        {
            return "// " + filePath + " // " + fileName + " //";
        }

        public void FromStringData(String Input)
        {
            Console.WriteLine("Input: " + Input);

            String pattern = @"// (.*) // (.*) //$";

            Match matches = Regex.Match(Input, pattern);


            if (matches.Groups.Count != 3)
            {
                Debug.WriteLine("Wrong filestring!");
                return;
            }

            this.filePath = matches.Groups[1].ToString();
            this.fileName = matches.Groups[2].ToString();

        }

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
