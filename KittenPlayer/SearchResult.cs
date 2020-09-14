using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KittenPlayer
{
    public class SearchResult
    {
        private string _name { get; }

        public SearchResult(string name)
        {
            this._name = name;
        }

        public static async Task<string> Download(string name)
        {
            var request = WebRequest.Create(@"https://www.youtube.com/results?search_query=" + name) as HttpWebRequest;
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            var response = await request.GetResponseAsync() as HttpWebResponse;
            var receiveStream = response.GetResponseStream();
            var readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var stream = readStream.ReadToEnd();
            response.Close();
            readStream.Close();
            return stream;
        }

        public async Task<List<Result>> GetResults()
        {
            var data = await Download(_name);
            var lines = Regex.Split(data, @"\n");
            var tracks = new List<Result>();
            foreach (var str in lines)
            {
                var track = new Result(str);
                if (track.Type != EType.None) tracks.Add(track);
            }
            return tracks;
        }
    }
}