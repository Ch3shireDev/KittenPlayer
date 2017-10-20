using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

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

        public String Artist { get => GetValue("Artist"); }
        public String Album { get => GetValue("Album"); }
        public String Title { get => GetValue("Title"); }
        public String Number { get => GetValue("Number"); }

        public TagLib.Tag Tag { get; set; }

        public String GetValue(String Key)
        {
            if (Properties.Count == 0) GetMetadata();
            if (!Properties.ContainsKey(Key)) return "";
            else return Properties[Key];
        }

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
            GetMetadata();
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

        public Dictionary<String, String> Properties = new Dictionary<String, String>();

        TagLib.TagTypes Type {
            get
            {
                if (IsMp3) return TagLib.TagTypes.Id3v2;
                else if (IsM4a) return TagLib.TagTypes.Apple;
                else return TagLib.TagTypes.None;
            }
        }

        public bool Writeable = false;

        void GetMetadata()
        {
            if (!File.Exists(path)) return;
            if (MusicTab.IsDirectory(path)) return;
            if (!IsValid()) return;
            TagLib.File f = TagLib.File.Create(path);

            Writeable = f.Writeable;
            if (Type is TagLib.TagTypes.None) return;

            Tag = f.GetTag(Type, true);
                
            Properties.Add("Artist", Tag.FirstPerformer);
            Properties.Add("Album", Tag.Album);
            Properties.Add("Title", Tag.Title);

            String Number = Tag.Track == 0 ? "" : Tag.Track.ToString();
            Properties.Add("Number", Number);

        }
        
        public void SetMetadata(ListViewItem Item)
        {
            TagLib.File f = TagLib.File.Create(path);
            if (!f.Writeable) return;
            
            f.Tag.Title = Item.Text;
            f.Tag.Album = Item.SubItems[2].Text;
            
            uint.TryParse(Item.SubItems[3].Text, out uint n);

            f.Tag.Track = n;
           
            Tag = f.Tag;
            
            Properties["Artist"] = Tag.FirstPerformer;
            Properties["Album"] = Tag.Album;
            Properties["Title"] = Tag.Title;

            String Number = Tag.Track == 0 ? "" : Tag.Track.ToString();
            Properties["Number"] = Number;

            try
            {
                f.Save();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
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
            GetMetadata();
        }

        public String GetDefaultDirectory()
        {
            MainWindow window = Application.OpenForms[0] as MainWindow;
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
