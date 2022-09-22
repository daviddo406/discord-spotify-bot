using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord_Spotify_Bot.Services;
using System;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Modules
{
    public class PlayerModule : ModuleBase<SocketCommandContext>
    {
        public static IAudioClient AudioClient { get; private set; }

        private static AudioStreamService AudioStreamService
        {
            get 
            {
                return AudioStreamService; 
            } 
            set
            {
                if (AudioStreamService == null)
                {
                    AudioStreamService = new AudioStreamService();
                }
            }
        }

        public PlayerModule (AudioStreamService audioStreamService = null)
        {
            AudioStreamService = audioStreamService;
        }

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

            YoutubeExplodeService youtubeExplodeService = new YoutubeExplodeService();

            AudioStreamService.Push(await youtubeExplodeService.Download(path));
            if (AudioStreamService.IsPlaying == false)
            {
                do
                {
                    await Play();
                }
                while (AudioStreamService.IsEmpty() == false);
            }
            else
            {
                Console.WriteLine("Song has been added to queue");
                await ReplyAsync("Song has been added to queue!");
            }
        }

        [Command("skip", RunMode = RunMode.Async)]
        public async Task SkipSong()
        {
            await AudioStreamService.ClearStream(AudioClient);
            await Play();
        }

        private async Task Play()
        {
            await AudioStreamService.SendStream(AudioClient);
        }

    }
}
