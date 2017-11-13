using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public MusicTab MusicTab = null;

        public ProgressBar progressBar;

        public String Artist { get => GetValue("Artist"); set => SetValue("Artist", value); }
        public String Album { get => GetValue("Album"); set => SetValue("Album", value); }
        public String Title { get => GetValue("Title"); set => SetValue("Title", value); }
        public String Number { get => GetValue("Number"); set => SetValue("Number", value); }
        public String Duration { get => GetValue("Duration"); set => SetValue("Duration", value); }

        public TagLib.Tag Tag { get; set; }

        internal void ConvertToMp3()
        {
            FFmpeg.ConvertToMp3(this);
        }

        public void Download()
        {
            MusicTab.DownloadTrack(this);
        }

        public String GetValue(String Key)
        {
            if (IsOnline)
            {
                String Value = Info[Key] as String;
                if (Value == null) return "";
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

        public Track()
        {
        }

        public Track(String filePath, String ID = "")
        {
            this.filePath = filePath;
            this.ID = ID;

            GetMetadata();
        }

        private bool IsBroken
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

        public StatusType GetStatus()
        {
            if (String.IsNullOrWhiteSpace(ID))
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

        private bool IsOnlineTrack()
        {
            if (filePath == "") return false;
            if (filePath.Contains(".")) return false;
            if (filePath.Contains("/")) return false;
            return true;
        }

        private bool CheckExtension(String ext)
        {
            String Extension = Path.GetExtension(filePath);
            return Extension.Equals(ext, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool IsMp3 { get { return CheckExtension(".mp3"); } }
        public bool IsM4a { get { return CheckExtension(".m4a"); } }

        public bool IsValid()
        {
            if (File.Exists(filePath)) return true;
            else if (!String.IsNullOrWhiteSpace(ID)) return true;
            return IsMp3 || IsM4a;
        }

        public Dictionary<String, String> Properties = new Dictionary<String, String>();

        private TagLib.TagTypes Type
        {
            get
            {
                if (IsMp3) return TagLib.TagTypes.Id3v2;
                else if (IsM4a) return TagLib.TagTypes.Apple;
                else return TagLib.TagTypes.None;
            }
        }

        public bool Writeable = false;

        private void GetMetadata()
        {
            if (!File.Exists(filePath)) return;
            if (MusicTab.IsDirectory(filePath)) return;
            if (!IsValid()) return;
            TagLib.File f = null;

            try { f = TagLib.File.Create(filePath); }
            catch { return; }

            Writeable = f.Writeable;
            if (Type is TagLib.TagTypes.None) return;

            Tag = f.GetTag(Type, true);

            Properties.Clear();

            Properties.Add("Artist", Tag.FirstPerformer);
            Properties.Add("Album", Tag.Album);
            if (String.IsNullOrWhiteSpace(Tag.Title))
                Path.GetFileNameWithoutExtension(filePath);
            else Properties.Add("Title", Tag.Title);

            String Number = Tag.Track == 0 ? "" : Tag.Track.ToString();
            Properties.Add("Number", Number);

            Duration = GetDuration(f);
            Properties["Duration"] = GetDuration(f);
        }

        private String GetDuration(TagLib.File f = null)
        {
            if (f == null) return "00:00";
            if (IsLocal || IsOffline)
            {
                String Hours = f.Properties.Duration.Hours.ToString("D2");
                String Minutes = f.Properties.Duration.Minutes.ToString("D2");
                String Seconds = f.Properties.Duration.Seconds.ToString("D2");

                String Duration = Minutes + ":" + Seconds;
                if (Hours != "00") Duration = Hours + ":" + Duration;
                return Duration;
            }
            else return "00:00";
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
            catch (Exception e) { Debug.WriteLine(e.ToString()); return; }

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

#if DEBUG
        public void GetOnlineTitle()
        {
            Title = YoutubeDL.GetOnlineTitle(this);
#else

        public async Task GetOnlineTitle()
        {
            Title = await YoutubeDL.GetOnlineTitle(this);
#endif
            Item.Text = Title;
        }

        private String SanitizeFilename(String name)
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

        public String GetDefaultDirectory()
        {
            MainWindow window = MainWindow.Instance;
            return window.Options.DefaultDirectory;
        }

        public ListViewItem GetListViewItem(ListView PlaylistView)
        {
            ListViewItem item = new ListViewItem()
            {
                Text = Title
            };

            item.SubItems.Add(Artist);
            item.SubItems.Add(Album);
            item.SubItems.Add(Number);
            item.SubItems.Add(Status.ToString());
            item.SubItems.Add(filePath);
            item.SubItems.Add(ID);
            item.SubItems.Add(Duration);

            Item = item;

            return item;
        }

        public void OfflineToLocalData()
        {
            Debug.WriteLine(this.Info["Artist"] as String);
            List<String> Keys = new List<String> { "Artist", "Title", "Album", "Number" };
            foreach (String key in Keys)
            {
                String Value = Info[key] as String;
                if (String.IsNullOrWhiteSpace(Value)) continue;
                SetValue(key, Value);
            }
            GetMetadata();
        }

        public void UpdateItem()
        {
            if (Item == null) return;
            Item.SubItems[0].Text = Title;
            Item.SubItems[1].Text = Artist;
            Item.SubItems[2].Text = Album;
            Item.SubItems[3].Text = Number;
            Item.SubItems[4].Text = Status.ToString();
            Item.SubItems[5].Text = filePath;
            Item.SubItems[6].Text = ID;
            Item.SubItems[7].Text = Duration;
        }
    }
}