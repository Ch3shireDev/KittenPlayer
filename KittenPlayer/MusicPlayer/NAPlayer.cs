using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KittenPlayer
{
    public class NAPlayer : Player
    {
        public override double Volume { get; set; }
        public override double Progress { get; set; }
        public override double TotalMilliseconds { get; }
        public override bool IsPlaying { get; set; }
        public override bool IsPaused { get; set; }
        public override void Load(Track track)
        {

            throw new NotImplementedException();
        }

        public override void Play()
        {
            throw new NotImplementedException();
        }

        public override void Pause()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void Resume()
        {
            throw new NotImplementedException();
        }

        public override event EventHandler OnTrackEnded;
    }
}
