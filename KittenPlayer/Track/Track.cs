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
        public String ID;

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

        public Track(String filePath, String ID = "")
        {
            this.path = filePath;
            this.ID = ID;
            
            GetMetadata();
        }


        bool IsBroken
        {
            get
            {
                if (Status is StatusType.Online)
                    return false;
                else
                    return !File.Exists(path);
            }
        }

        public enum StatusType
        {
            Local, //file is only on disk
            Offline, //file is both on disk and on the internet
            Online //file is only on the internet
        }

        public StatusType Status => GetStatus();

        StatusType GetStatus()
        {
            if(String.IsNullOrWhiteSpace(ID))
            {
                Debug.WriteLine(ID);
                return StatusType.Local;
            }
            else
            {
                if (!String.IsNullOrEmpty(path))
                    return StatusType.Offline;
                else
                    return StatusType.Online;
            }
        }

        public bool IsLocal => Status == StatusType.Local;
        public bool IsOffline => Status == StatusType.Offline;
        public bool IsOnline => Status == StatusType.Online;

        bool IsOnlineTrack()
        {
            if (path == "") return false;
            if (path.Contains(".")) return false;
            if (path.Contains("/")) return false;
            return true;
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
            if (File.Exists(path)) return true;
            else
            {
                if (!String.IsNullOrWhiteSpace(ID)) return true;
            }
            //if (!File.Exists(path)) return false;
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

            Properties.Clear();

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
            f.Tag.Performers = new string[]{ Item.SubItems[1].Text };
            
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
            startInfo.Arguments = "/C youtube-dl -f m4a " + ID + " " + args;
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

        public String GetOnlineTitle()
        {
            String output = YoutubeDl("--get-title");
            var groups = Regex.Match(output, @"(.*)\s*$").Groups;
            return groups[1].Value;
        }

        String SanitizeFilename(String name)
        {
            return name
                .Replace("'", "")
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }

        public void Download()
        {

            if (IsOnline)
            {

                String OutputPath = GetDefaultDirectory() + "\\" + SanitizeFilename(GetOnlineTitle()) + ".m4a";
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }
                if (!File.Exists(OutputPath))
                {
                    YoutubeDl("-o x.m4a");
                    if (File.Exists("x.m4a"))
                    {
                        this.path = "x.m4a";
                        File.Move(this.path, OutputPath);
                    }
                }
                this.path = OutputPath;
            }
            if (IsOffline)
            {
                SetPath(GetDefaultDirectory());
            }
            GetMetadata();
        }

        public String GetDefaultDirectory()
        {
            MainWindow window = Application.OpenForms[0] as MainWindow;
            return window.options.SelectedDirectory;
        }

        public bool SetPath(String NewPath)
        {
            String newPath = NewPath + "\\" + SanitizeFilename(GetOnlineTitle()) + Path.GetExtension(path);
            try
            {
                File.Move(path, newPath);
                path = newPath;

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public ListViewItem GetListViewItem(ListView PlaylistView)
        {
            ListViewItem item = new ListViewItem();
            item.Text = this.Title;

            item.SubItems.Add(this.Artist);
            item.SubItems.Add(this.Album);
            item.SubItems.Add(this.Number);
            item.SubItems.Add(this.Status.ToString());
            item.SubItems.Add(this.path);
            //item.SubItems.Add(this.name);
            item.SubItems.Add(this.ID);


            foreach (ColumnHeader Column in PlaylistView.Columns)
            {
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                //subItem.Name = Column.Name;
                //subItem.Text = track.GetValue(Column.Text);
                //item.SubItems.Insert(Column.Index, subItem);
            }


            return item;
        }

    }
}
