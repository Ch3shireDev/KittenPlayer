using System;
using System.IO;

namespace KittenPlayer
{
    [Serializable]
    public class Track
    {
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
            return Extension.Equals(".mp3", StringComparison.CurrentCultureIgnoreCase);
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
    }
}
