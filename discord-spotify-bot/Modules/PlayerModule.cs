using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord_Spotify_Bot.Models;
using Discord_Spotify_Bot.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        public static IAudioClient AudioClient { get; private set; }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument.");
                return;
            }
            AudioClient = await channel.ConnectAsync();
        }

        [Command("play", RunMode = RunMode.Async)]
        public async Task PlaySong([Remainder] string path)
        {
            Console.WriteLine("Play Received!");
            string file = @"D:\General\work\audio.mp3";

            YoutubeExplodeService youtubeExplodeService = new YoutubeExplodeService();
            await youtubeExplodeService.Download(path);
            await Play(file);
        }

        private async Task Play(string file)
        {
            // For the next step with transmitting audio, you would want to pass this Audio Client in to a service.

            AudioStreamService audioStreamService = new AudioStreamService();
            await audioStreamService.SendStream(AudioClient, file);
        }


    }
}
