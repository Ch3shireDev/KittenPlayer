using System.Collections.Generic;
using System.Threading;

namespace KittenPlayer
{
    internal class DownloadManager
    {
        private static bool downloadAgain;
        private static DownloadManager Instance;

        public static int Counter;
        private List<Track> TracksToDownload;

        private DownloadManager()
        {
        }

        public static void JustDownload(List<Track> tracks)
        {
            downloadAgain = false;
            AddToDownload(tracks);
        }

        public static void DownloadAgain(List<Track> tracks)
        {
            downloadAgain = true;
            AddToDownload(tracks);
        }

        private static void AddToDownload(List<Track> tracks)
        {
            if (Instance == null)
                Instance = new DownloadManager();
            if (Instance.TracksToDownload == null)
                Instance.TracksToDownload = new List<Track>();
            Instance.TracksToDownload.AddRange(tracks);
            Instance.Download();
        }

#if DEBUG

        private void Download()
#else
        private async Task Download()
#endif
        {
            while (TracksToDownload.Count > 0)
            {
                var track = TracksToDownload[0];
                if (downloadAgain)
                {
                    track.filePath = "";
                    track.UpdateItem();
                }
#if DEBUG
                YoutubeDL.DownloadTrack(track);
#else
                if (Counter < 3)
                    YoutubeDL.DownloadTrack(track);
                else
                    await YoutubeDL.DownloadTrack(track);
#endif
                TracksToDownload.Remove(track);
            }
        }

        internal static async void PlayAfterDownload(List<Track> tracks)
        {
            if (tracks.Count == 0) return;
            var track = tracks[0];
#if DEBUG
            YoutubeDL.DownloadTrack(track);
#else
            await YoutubeDL.DownloadTrack(track);
#endif
            track.MusicTab.Play(track);
            while (!MusicPlayer.Instance.IsPlaying)
                //await Task.Delay(200);
                Thread.Sleep(200);
            AddToDownload(tracks.GetRange(1, tracks.Count - 1));
        }
    }
}