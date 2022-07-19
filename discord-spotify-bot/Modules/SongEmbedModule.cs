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
            String url = "https://open.spotify.com/track/27NovPIUIRrOZoCHxABJwK?si=c532afb86b6b4446";
            String thumbnailUrl = "https://i.scdn.co/image/ab67616d0000b273ba26678947112dff3c3158bf";
            var embed = new EmbedBuilder
            {
                Title = "INDUSTRY BABY (feat. Jack Harlow)",
                Url = url,
                ThumbnailUrl  = thumbnailUrl
            };
            embed.WithCurrentTimestamp().WithColor(Color.Green);
            await ReplyAsync(embed: embed.Build());
        }
    }
}
