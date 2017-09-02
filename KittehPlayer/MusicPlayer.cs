using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace KittehPlayer
{
    public partial class MusicPlayer
    {

        private WMPLib.WindowsMediaPlayer WMPlayer = new WMPLib.WindowsMediaPlayer();
        private static MusicPlayer Instance = null;

        /// <summary> 
        /// Class is a singleton, so it's instanced by static method, not a constructor.
        /// </summary> 

        private MusicPlayer() { }

        /// <summary> 
        /// Static method for instancing a new MusicPlayer object.
        /// </summary> 

        public static MusicPlayer NewMusicPlayer()
        {
            if (Instance == null)
            {
                Instance = new MusicPlayer();
            }
            return Instance;
        }

        /// <summary> 
        /// Starts playing new file, automatically stops the old file.
        /// </summary>

        bool IsPaused = false;

        public void Play(String File)
        {
            if (!IsPaused)
            {
                Stop();
                WMPlayer.URL = File;
            }
            WMPlayer.controls.play();
        }
        
        /// <summary> 
        /// Pauses the file.
        /// </summary> 

        public void Pause()
        {
            WMPlayer.controls.pause();
            this.IsPaused = true;
        }
        
        /// <summary> 
        /// Stops playing the file.
        /// </summary> 

        public void Stop()
        {
            WMPlayer.controls.stop();
        }
    }
}
