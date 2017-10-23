using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace KittenPlayer
{
    class TrackObject
    {
        public String ID;
        public String Title;
    }

    class YoutubeDL
    {
        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            FileName = "cmd.exe",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        
        Process process = new Process();

        public YoutubeDL(String URL)
        {
            startInfo.Arguments = "/C youtube-dl " + URL;
        }

        StreamReader Start(String Arguments)
        {
            process.StartInfo = startInfo;
            process.StartInfo.Arguments += " "+ Arguments;
            process.Start();
            return process.StandardOutput; 
        }

        public List<Track> GetData()
        {
            String output = Start("-j --flat-playlist").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Track> Tracks = new List<Track>();
            foreach (String line in Lines)
            {
                JObject jObject = JObject.Parse(line);
                jObject.TryGetValue("title", out JToken title);
                jObject.TryGetValue("url", out JToken URL);
                if (title != null && URL != null)
                    Tracks.Add(new Track("", title.ToString(), URL.ToString()));
            }
            return Tracks;
        }
        


        public List<TrackObject> Search(String str)
        {
            String output = Start("-j --flat-playlist").ReadToEnd();
            string[] Lines = output.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<TrackObject> Out = new List<TrackObject>();

            foreach (String line in Lines)
            {
                //Debug.WriteLine(line);
                JObject jObject = JObject.Parse(line);
                jObject.TryGetValue("id", out JToken URL);
                if (URL != null)
                {
                    //Debug.WriteLine(line);
                    TrackObject track = new TrackObject() { ID = URL.ToString() };
                    Out.Add(track);
                }
            }
            return Out;
        }
    }
}
