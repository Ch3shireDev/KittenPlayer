using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace KittenPlayer
{
    
    public partial class Track
    {
        public ListViewItem Item = null;

        public MusicPlayer Player
        {
            get { return MusicPlayer.Instance; }
        }

        public String filePath;
        public String ID;
        public Hashtable Info = new Hashtable();

        public ProgressBar progressBar;

        public String Artist { get => GetValue("Artist"); set => SetValue("Artist", value); }
        public String Album { get => GetValue("Album"); set => SetValue("Album", value); }
        public String Title { get => GetValue("Title"); set => SetValue("Title", value); }
        public String Number { get => GetValue("Number"); set => SetValue("Number", value); }

        public TagLib.Tag Tag { get; set; }

        public String GetValue(String Key)
        {
            if (IsOnline)
            {
                String Value = Info[Key] as String;
                if (Value is null) return "";
                else return Value;
            }
            else
            {
                if (Properties.Count == 0) GetMetadata();
                if (!Properties.ContainsKey(Key)) return "";
                else return Properties[Key];
            }
        }

        public void SetValue(String Key, String Value)
        {
            if (IsOnline)
            {
                Info[Key] = Value;
            }
            else
            {
                Properties[Key] = Value;
                SaveMetadata();
            }
        }

        public Track() { }

        public Track(String filePath, String ID = "")
        {
            this.filePath = filePath;
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
                    return !File.Exists(filePath);
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
                if (!String.IsNullOrEmpty(filePath))
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
            if (filePath == "") return false;
            if (filePath.Contains(".")) return false;
            if (filePath.Contains("/")) return false;
            return true;
        }


        bool CheckExtension(String ext)
        {
            String Extension = Path.GetExtension(filePath);
            return Extension.Equals(ext, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool IsMp3 { get { return CheckExtension(".mp3"); } }
        public bool IsM4a { get { return CheckExtension(".m4a"); } }

        public bool IsValid()
        {
            if (File.Exists(filePath)) return true;
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
            if (!File.Exists(filePath)) return;
            if (MusicTab.IsDirectory(filePath)) return;
            if (!IsValid()) return;
            TagLib.File f = TagLib.File.Create(filePath);

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

        public void SetMetadata()
        {
            Artist = Item.SubItems[1].Text;
            Album = Item.SubItems[2].Text;
            Title = Item.Text;
            Number = Item.SubItems[3].Text;
        }

        public void SaveMetadata()
        {
            if (filePath == "") return;
            TagLib.File f = TagLib.File.Create(filePath);
            if (f == null) return;
            if (f.Tag == null) return;

            f.Tag.Title = Title;
            f.Tag.Album = Album;
            f.Tag.Performers = new string[] { Artist };

            uint.TryParse(Number, out uint n);
            f.Tag.Track = n;

            try { f.Save(); }
            catch (Exception e){ Debug.WriteLine(e.ToString()); return; }

            Debug.WriteLine("Succesfull save of " + filePath);
        }

        public void Pause() => Player.Pause();

        public void Stop() => Player.Stop();

        public NAudio.Wave.MediaFoundationReader Load()
        {
            NAudio.Wave.MediaFoundationReader reader;
            reader = new NAudio.Wave.MediaFoundationReader(filePath);
            return reader;
        }

        public async Task<String> GetOnlineTitle()
        {
            YoutubeDL youtube = new YoutubeDL(ID);
            String output = await youtube.Download("--get-title");
            var match = Regex.Match(output, @"(.*)\s*$");
            if (match.Success)
            {
                var groups = match.Groups;
                return groups[1].Value;
            }
            else return "";
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
                YoutubeDL youtube = new YoutubeDL(ID) { progressBar = progressBar };
                if (File.Exists("x.m4a"))
                    File.Delete("x.m4a");

                    String Title = await youtube.Download("-o x.m4a");
                String OutputPath = GetDefaultDirectory() + "\\" + SanitizeFilename(Title) + ".m4a";
                if (File.Exists("x.m4a"))
                {
                    if (File.Exists(OutputPath))
                        File.Delete(OutputPath);

                    this.filePath = "x.m4a";
                    File.Move(this.filePath, OutputPath);
                    this.filePath = OutputPath;
                    OfflineToLocalData();
                    SaveMetadata();

                    if (SendToArtistAlbum) SetArtistAlbumDir();
                }
            }
        }

        public void OfflineToLocalData()
        {
            List<String> Keys = new List<String> { "Title", "Album", "Author", "Number" };
            foreach (String key in Keys)
            {
                String Value = Info[key] as String;
                if (String.IsNullOrWhiteSpace(Value)) continue;
                Properties[key] = Value;
            }
        }

        public String GetDefaultDirectory()
        {
            MainWindow window = MainWindow.Instance;
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
            item.SubItems.Add(this.filePath);
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
