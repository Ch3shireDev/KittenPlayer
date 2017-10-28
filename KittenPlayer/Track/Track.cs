using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace KittenPlayer
{

    [Serializable]
    public class Track
    {

        public MusicPlayer Player
        {
            get { return MusicPlayer.Instance; }
        }

        public String path;
        public String ID;
        public String Info;

        public ProgressBar progressBar;

        public void SaveProperties()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, Properties);
            Info = stream.ToString();

            Debug.WriteLine(Info);
        }

        public void LoadProperties()
        {
            if (Info == null || Info == "") return;
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            //formatter.Deserialize()
            //Info = stream.ToString();

            //Debug.WriteLine(Info);
        }

        public String Artist { get => GetValue("Artist"); set => SetValue("Artist", value); }
        public String Album { get => GetValue("Album"); set => SetValue("Album", value); }
        public String Title { get => GetValue("Title"); set => SetValue("Title", value); }
        public String Number { get => GetValue("Number"); set => SetValue("Number", value); }

        public TagLib.Tag Tag { get; set; }

        public String GetValue(String Key)
        {
            if (Properties.Count == 0) GetMetadata();
            if (!Properties.ContainsKey(Key)) return "";
            else return Properties[Key];
        }

        public void SetValue(String Key, String Value)
        {
            Properties[Key] = Value;
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

            Properties["Artist"] = Item.SubItems[1].Text;
            Properties["Album"] = Item.SubItems[2].Text;
            Properties["Title"] = Item.Text;
            String Number = Tag.Track == 0 ? "" : Item.SubItems[3].Text;
            Properties["Number"] = Number;


            if (path == "") return;
            TagLib.File f = TagLib.File.Create(path);
            
            f.Tag.Title = Item.Text;
            f.Tag.Album = Item.SubItems[2].Text;
            f.Tag.Performers = new string[]{ Item.SubItems[1].Text };
            
            uint.TryParse(Item.SubItems[3].Text, out uint n);

            f.Tag.Track = n;
           
            Tag = f.Tag;
            
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
            Player.Pause();
        }

        public void Stop()
        {
            Player.Stop();
        }

        public NAudio.Wave.MediaFoundationReader Load()
        {
            NAudio.Wave.MediaFoundationReader reader;
            reader = new NAudio.Wave.MediaFoundationReader(path);
            return reader;
        }

        public async Task<String> GetOnlineTitle()
        {
            YoutubeDL youtube = new YoutubeDL(ID);
            String output = await youtube.Download("--get-title");
            var groups = Regex.Match(output, @"(.*)\s*$").Groups;
            return groups[1].Value;
        }

        String SanitizeFilename(String name)
        {
            if (String.IsNullOrEmpty(name)) return "unnamed";
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
                .Replace("|", "")
                .Replace("\n", "");
        }

        public async Task Download()
        {
            if (IsOnline)
            {
                YoutubeDL youtube = new YoutubeDL(ID);
                youtube.progressBar = progressBar;
                if (File.Exists("x.m4a"))
                    File.Delete("x.m4a");

                    String Title = await youtube.Download("-o x.m4a");
                String OutputPath = GetDefaultDirectory() + "\\" + SanitizeFilename(Title) + ".m4a";
                if (File.Exists("x.m4a"))
                {
                    if (File.Exists(OutputPath))
                        File.Delete(OutputPath);

                    this.path = "x.m4a";
                    File.Move(this.path, OutputPath);
                    this.path = OutputPath;
                }
            }
            GetMetadata();
        }

        public String GetDefaultDirectory()
        {
            MainWindow window = Application.OpenForms[0] as MainWindow;
            return window.options.SelectedDirectory;
        }

        //public bool SetPath(String NewPath)
        //{
        //    String newPath = NewPath + "\\" + SanitizeFilename(Title) + Path.GetExtension(path);
        //    try
        //    {
        //        if (File.Exists(newPath))
        //        {
        //            File.Delete(newPath);
        //        }
        //        File.Move(path, newPath);
        //        if (File.Exists(newPath))
        //        {
        //            path = newPath;
        //        }

        //    }
        //    catch(Exception e)
        //    {
        //        Debug.WriteLine(e.ToString());
        //        return false;
        //    }
        //    return true;
        //}

        public ListViewItem GetListViewItem(ListView PlaylistView)
        {
            ListViewItem item = new ListViewItem()
            {
                Text = Title
            };

            item.SubItems.Add(this.Artist);
            item.SubItems.Add(this.Album);
            item.SubItems.Add(this.Number);
            item.SubItems.Add(this.Status.ToString());
            item.SubItems.Add(this.path);
            item.SubItems.Add(this.ID);


            //foreach (ColumnHeader Column in PlaylistView.Columns)
            //{
                //ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                //subItem.Name = Column.Name;
                //subItem.Text = track.GetValue(Column.Text);
                //item.SubItems.Insert(Column.Index, subItem);
            //}


            return item;
        }

    }
}
