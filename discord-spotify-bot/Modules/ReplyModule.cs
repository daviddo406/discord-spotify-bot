using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Modules
{
    public class ReplyModule : ModuleBase<SocketCommandContext>
    {
        [Command("link")]
        [Summary("Display a link from spotify")]

        public Task SayAsync([Remainder][Summary("The link to display")] string link)
            => ReplyAsync("https://open.spotify.com/track/7a86XRg84qjasly9f6bPSD?si=cd0cdb460741405e");
    }
}
