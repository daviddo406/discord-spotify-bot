using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Discord_Spotify_Bot.Models;
using System.IO;
using Newtonsoft.Json;
using Discord;

namespace Discord_Spotify_Bot.Modules
{
    public class SearchSong : ModuleBase<SocketCommandContext>
    {
        int errorCounter = 0;

        [Command("song")]
        [Summary("Search for a song from Spotify")]
        public async Task SearchSongAsync([Remainder] string command)
        {

            using (var client = new HttpClient()){
                string token = "";
                try
                {
                    token = File.ReadAllText(@".\spotifyToken.txt");
                }
                catch (FileNotFoundException)
                {
                    await RefreshSpotifyToken.GetTokenAsync();
                }
                var q = command.Replace(" ", "%20");
                var type = "track";
                var market = "ES";
                var limit = 1;
                var endpoint = client.BaseAddress = new Uri($"https://api.spotify.com/v1/search?q={q}&type={type}&market={market}&limit={limit}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = client.GetAsync(endpoint).Result;
                
                if (result.IsSuccessStatusCode) {
                    // var json = result.Content.ReadAsStringAsync().Result;
                    Track json = result.Content.ReadAsAsync<Track>().Result;
                    // await ReplyAsync(json.Albums.SpotifyLink);
                    var embed = new EmbedBuilder
                    {
                        Title = json.Tracks.Items[0].Name,
                        Url = json.Tracks.Items[0].ExternalUrls.Spotify,
                        ThumbnailUrl = json.Tracks.Items[0].Album.Images[0].Url,
                        Description = $"Album: {json.Tracks.Items[0].Album.Name}"
                    };
                    embed.WithCurrentTimestamp().WithColor(Color.Green);
                    await ReplyAsync(embed: embed.Build());
                }
                else {
                    if (errorCounter == 0)
                    {
                    errorCounter++;
                    await RefreshSpotifyToken.GetTokenAsync();
                    await SearchSongAsync(command);
                    }
                    else
                    {
                        await ReplyAsync("We are sorry. Looks like something went wrong with our server, our Devs are on the case!");
                    }
                }

        }
            
        }
    }
}
