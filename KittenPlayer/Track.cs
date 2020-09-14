using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using TagLib;
using File = TagLib.File;

namespace KittenPlayer
{
    public class Track
    {
        public enum StatusType
        {
            Local, //file is only on disk
            Offline, //file is both on disk and on the internet
            Online //file is only on the internet
        }

        public Track()
        {
        }

        public Track(string filePath, string ID = "")
        {
            this.filePath = filePath;
            this.ID = ID;

            GetMetadata();
        }

        public string filePath { get; set; }
        public string ID { get; set; }
        public Hashtable Info { get; set; } = new Hashtable();
        public ListViewItem Item { get; set; }
        public MusicTab MusicTab { get; set; } = null;

        public ProgressBar progressBar { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public bool SendToArtistAlbum { get; set; }

        public bool Writeable { get; set; }

        public bool IsPlaying => MusicPlayer.Instance.CurrentTrack == this;

        public MusicPlayer Player => MusicPlayer.Instance;

        public string Artist
        {
            get => GetValue("Artist");
            set => SetValue("Artist", value);
        }

        public string Album
        {
            get => GetValue("Album");
            set => SetValue("Album", value);
        }

        public string Title
        {
            get => GetValue("Title");
            set => SetValue("Title", value);
        }

        public string Number
        {
            get => GetValue("Number");
            set => SetValue("Number", value);
        }

        public string Duration
        {
            get => GetValue("Duration");
            set => SetValue("Duration", value);
        }

        public Tag Tag { get; set; }

        private bool IsBroken
        {
            get
            {
                if (Status is StatusType.Online)
                    return false;
                return !System.IO.File.Exists(filePath);
            }
        }

        public StatusType Status => GetStatus();

        public bool IsLocal => Status == StatusType.Local;
        public bool IsOffline => Status == StatusType.Offline;
        public bool IsOnline => Status == StatusType.Online;

        public bool IsMp3 => CheckExtension(".mp3");
        public bool IsM4a => CheckExtension(".m4a");

        private TagTypes Type
        {
            get
            {
                if (IsMp3) return TagTypes.Id3v2;
                if (IsM4a) return TagTypes.Apple;
                return TagTypes.None;
            }
        }

        public void SetArtistAlbumDir()
        {
            if (IsOnline) SendToArtistAlbum = true;
            if (!IsOffline) return;

            var NameDir = Path.GetFileName(filePath);

            var OutputDir = MainWindow.Instance.Options.DefaultDirectory;

            OutputDir += "\\x\\" + NameDir;

            if (!System.IO.File.Exists(OutputDir))
                System.IO.File.Move(filePath, OutputDir);
            else
                System.IO.File.Delete(filePath);
            if (System.IO.File.Exists(OutputDir))
                filePath = OutputDir;

            if (Item != null) Item.SubItems[5].Text = filePath;
            Debug.WriteLine("Moved to " + filePath);
        }

        private static string SanitizeName(string Name)
        {
            return Name;
        }

        internal void ConvertToMp3()
        {
            FFmpeg.ConvertToMp3(this);
        }

        public void Download()
        {
            MusicTab.DownloadTrack(this);
        }

        public string GetValue(string Key)
        {
            if (IsOnline)
            {
                var Value = Info[Key] as string;
                if (Value == null) return "";
                return Value;
            }

            if (Properties.Count == 0) GetMetadata();
            if (!Properties.ContainsKey(Key)) return "";
            return Properties[Key];
        }

        public void SetValue(string Key, string Value)
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

        public StatusType GetStatus()
        {
            if (string.IsNullOrWhiteSpace(ID))
            {
                Debug.WriteLine(ID);
                return StatusType.Local;
            }

            if (!string.IsNullOrEmpty(filePath))
                return StatusType.Offline;
            return StatusType.Online;
        }

        private bool IsOnlineTrack()
        {
            if (filePath == "") return false;
            if (filePath.Contains(".")) return false;
            if (filePath.Contains("/")) return false;
            return true;
        }

        private bool CheckExtension(string ext)
        {
            var Extension = Path.GetExtension(filePath);
            return Extension.Equals(ext, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool IsValid()
        {
            if (System.IO.File.Exists(filePath)) return true;
            if (!string.IsNullOrWhiteSpace(ID)) return true;
            return IsMp3 || IsM4a;
        }

        private void GetMetadata()
        {
            if (!System.IO.File.Exists(filePath)) return;
            if (MusicTab.IsDirectory(filePath)) return;
            if (!IsValid()) return;
            File f = null;

            try
            {
                f = File.Create(filePath);
            }
            catch
            {
                return;
            }

            Writeable = f.Writeable;
            if (Type is TagTypes.None) return;

            Tag = f.GetTag(Type, true);

            Properties.Clear();

            Properties.Add("Artist", Tag.FirstPerformer);
            Properties.Add("Album", Tag.Album);
            if (string.IsNullOrWhiteSpace(Tag.Title))
                Path.GetFileNameWithoutExtension(filePath);
            else Properties.Add("Title", Tag.Title);

            var Number = Tag.Track == 0 ? "" : Tag.Track.ToString();
            Properties.Add("Number", Number);

            Duration = GetDuration(f);
            Properties["Duration"] = GetDuration(f);
        }

        private string GetDuration(File f = null)
        {
            if (f == null) return "00:00";
            if (IsLocal || IsOffline)
            {
                var Hours = f.Properties.Duration.Hours.ToString("D2");
                var Minutes = f.Properties.Duration.Minutes.ToString("D2");
                var Seconds = f.Properties.Duration.Seconds.ToString("D2");

                var Duration = Minutes + ":" + Seconds;
                if (Hours != "00") Duration = Hours + ":" + Duration;
                return Duration;
            }

            return "00:00";
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
            var f = File.Create(filePath);
            if (f?.Tag == null) return;

            f.Tag.Title = Title;
            f.Tag.Album = Album;
            f.Tag.Performers = new[] {Artist};

            uint.TryParse(Number, out var n);
            f.Tag.Track = n;

            try
            {
                f.Save();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            Debug.WriteLine("Succesfull save of " + filePath);
        }

        public void Pause()
        {
            Player.Pause();
        }

        public void Stop()
        {
            Player.Stop();
        }

        public MediaFoundationReader Load()
        {
            return new MediaFoundationReader(filePath);
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

        private string SanitizeFilename(string name)
        {
            if (string.IsNullOrEmpty(name)) return "unnamed";
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

        public string GetDefaultDirectory()
        {
            var window = MainWindow.Instance;
            return window.Options.DefaultDirectory;
        }

        public ListViewItem GetListViewItem(ListView PlaylistView)
        {
            var item = new ListViewItem
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
            Debug.WriteLine(Info["Artist"] as string);
            var Keys = new List<string> {"Artist", "Title", "Album", "Number"};
            foreach (var key in Keys)
            {
                var Value = Info[key] as string;
                if (string.IsNullOrWhiteSpace(Value)) continue;
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