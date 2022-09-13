using System;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace Discord_Spotify_Bot.Services
{
    public class YoutubeExplodeService
    {
        public async Task Download(string path)
        {
            var youtube = new YoutubeClient();

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(path);

            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            Console.WriteLine("Starting Download...");
            //var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            await youtube.Videos.Streams.DownloadAsync(streamInfo, @"D:\General\work\audio.mp3");

            Console.WriteLine("Download Complete!");
        }
    }
}
