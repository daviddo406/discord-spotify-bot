using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Modules
{
    public class SongEmbedModule : ModuleBase<SocketCommandContext>
    {
        [Command("song")]
        [Summary("Displays an embedded url to spotify")]
        public async Task SendSongEmbedAsync()
        {
            var embed = new EmbedBuilder
            {
                Title = "Song here!",
                Description = "More text here"
            };

            embed.WithCurrentTimestamp().WithColor(Color.Green);
            await ReplyAsync(embed: embed.Build());
        }
    }
}
