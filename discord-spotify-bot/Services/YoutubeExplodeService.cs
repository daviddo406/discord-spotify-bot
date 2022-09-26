using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Discord_Spotify_Bot.Services
{
    public class YoutubeExplodeService
    {
        public async Task<string> Download(string path)
        {
            if (Directory.Exists(@".\songs") == false)
            {
                Directory.CreateDirectory(@".\songs");
            }
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(path);
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            string fullHash = streamInfo.GetHashCode().ToString() + DateTime.Now.ToString("s").Replace(":", "-"); //2000-08-17T16-32-32
            string filePath = $@".\songs\{fullHash}.mp3";

            Console.WriteLine("Starting Download...");
            await youtube.Videos.Streams.DownloadAsync(streamInfo, filePath);

            Console.WriteLine("Download Complete!");
            return filePath;
        }
    }
}
