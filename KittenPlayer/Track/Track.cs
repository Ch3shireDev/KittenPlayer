using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace KittenPlayer
{

    [Serializable]
    public class Track
    {

        public MusicPlayer musicPlayer
        {
            get { return MusicPlayer.Instance; }
        }

        public String path;
        public String name;
        public String YoutubeID;

        public String Artist;
        public String Album;
        public String Title;
        public uint Number;

        public Track() { }

        public Track(String filePath, String fileName = "", String ID = "")
        {
            this.path = filePath;
            YoutubeID = ID;
            if (fileName == "")
            {
                this.name = Path.GetFileNameWithoutExtension(filePath);
            }
            else
            {
                this.name = fileName;
            }
            GetMP3Metadata();
        }

        bool IsOnlineTrack()
        {
            if (path == "") return false;
            if (path.Contains(".")) return false;
            if (path.Contains("/")) return false;
            return true;
        }

        public bool IsOnline
        {
            get { return IsOnlineTrack(); }
        }

        bool CheckExtension(String ext)
        {
            String Extension = Path.GetExtension(path);
            return Extension.Equals(ext, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool IsMp3 { get { return CheckExtension(".mp3"); } }
        public bool IsM4a { get { return CheckExtension(".m4a"); } }

        public bool IsValid()
        {
            if (!File.Exists(path)) return false;
            return IsMp3 || IsM4a;
        }

        void GetMP3Metadata()
        {
            if (!File.Exists(path)) return;
            if (MusicTab.IsDirectory(path)) return;
            if (!IsValid()) return;
            TagLib.File f = TagLib.File.Create(path);

            if (IsMp3)
            {
                this.Artist = f.Tag.FirstPerformer;
                this.Album = f.Tag.Album;
                this.Title = f.Tag.Title;
                this.Number = f.Tag.Track;
            }
            else if (IsM4a)
            {
                var tag = f.GetTag(TagLib.TagTypes.Apple, true) as TagLib.Mpeg4.AppleTag;
                this.Artist = tag.FirstPerformer;
                this.Album = tag.Album;
                this.Title = tag.Title;
                this.Number = tag.Track;
            }
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
            reader = new NAudio.Wave.MediaFoundationReader(path);
            return reader;
        }

        String YoutubeDl(String args = "")
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C youtube-dl -f m4a " + path + " " + args;
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            String output = reader.ReadToEnd();
            Debug.WriteLine(output);

            return output;
        }

        public String GetOnlineFilename()
        {
            String output = YoutubeDl("--get-filename");
            var groups = Regex.Match(output, @"(.*)\s*$").Groups;
            return groups[1].Value;
        }

        public void Download()
        {
            String output = YoutubeDl();
            this.path = GetOnlineFilename();
            SetPath(GetDefaultDirectory());
        }

        public String GetDefaultDirectory()
        {
            MainWindow window = System.Windows.Forms.Application.OpenForms[0] as MainWindow;
            return window.options.SelectedDirectory;
        }

        public bool SetPath(String NewPath)
        {
            String fName = Path.GetFileName(path);
            String newPath = NewPath + "/" + fName;
            try
            {
                File.Move(path, newPath);
                path = newPath;

            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
