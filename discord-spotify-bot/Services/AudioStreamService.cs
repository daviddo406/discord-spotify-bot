using Discord.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Services
{
    public class AudioStreamService
    {
        public static bool IsPlaying { get; set; }

        private static Queue<string> SongQueue = new Queue<string>();
        private static AudioOutStream AudioOutStream { get; set; }
        public static async Task SendStream(IAudioClient client)
        {
            string hash = SongQueue.Dequeue();
            using (var ffmpeg = CreateStream(hash))
            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (AudioOutStream = client.CreatePCMStream(AudioApplication.Mixed))
            {
                try
                {
                    IsPlaying = true;
                    await output.CopyToAsync(AudioOutStream);
                }
                finally
                {
                    await AudioOutStream.FlushAsync();
                    IsPlaying = false;
                    File.Delete(hash);
                }
            }
        }

        public static Task ClearStream(IAudioClient client)
        {
            AudioOutStream.Dispose();
            return Task.CompletedTask;
        }

        public static void Push(string song)
        {
            SongQueue.Enqueue(song);
        }

        public bool IsEmpty()
        {
            return SongQueue.Count == 0;
        }
        private static Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });
        }
    }

}
