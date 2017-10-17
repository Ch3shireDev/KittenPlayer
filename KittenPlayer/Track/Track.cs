using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace KittenPlayer
{
    public enum TrackType
    {
        Local,
        Online
    }

    [Serializable]
    public class Track
    {

        public TrackType Type;

        public MusicPlayer musicPlayer
        {
            get { return MusicPlayer.Instance; }
        }

        public String filePath;
        public String fileName;

        public String Artist;
        public String Album;
        public String Title;
        public uint Number;

        public Track() { }

        public Track(String filePath, String fileName = "")
        {
            this.filePath = filePath;
            if (fileName == "")
            {
                this.fileName = Path.GetFileNameWithoutExtension(filePath);
            }
            else
            {
                this.fileName = fileName;
            }
            GetMP3Metadata();
        }

        public bool IsValid()
        {
            String Extension = Path.GetExtension(filePath);
            bool isMp3 = Extension.Equals(".mp3", StringComparison.CurrentCultureIgnoreCase);
            bool isM4a = Extension.Equals(".m4a", StringComparison.CurrentCultureIgnoreCase);
            return isMp3 || isM4a;
        }

        void GetMP3Metadata()
        {
            if (MusicTab.IsDirectory(filePath)) return;
            if (Path.GetExtension(filePath) != ".mp3") return;
            TagLib.File f = TagLib.File.Create(filePath);
            this.Artist = f.Tag.FirstPerformer;
            this.Album = f.Tag.Album;
            this.Title = f.Tag.Title;
            this.Number = f.Tag.Track;
        }

        public virtual void Play()
        {
            musicPlayer.Play(this);
        }

        public void Pause()
        {
            musicPlayer.Pause();
        }

        public void Stop()
        {
            musicPlayer.Stop();
        }

        public NAudio.Wave.MediaFoundationReader Load()
        {
            NAudio.Wave.MediaFoundationReader reader;
            reader = new NAudio.Wave.MediaFoundationReader(filePath);
            return reader;
        }

        public String GetOnlineFilename()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a --get-filename " + @"https://www.youtube.com/watch?v=" + filePath;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();

            var groups = Regex.Match(output, @"(.*)\s*$").Groups;
            return groups[1].Value;
        }

        public void Download()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a " + @"https://www.youtube.com/watch?v=" + filePath;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();
            Debug.WriteLine(output);

            this.filePath = GetOnlineFilename();
        }
    }
}
