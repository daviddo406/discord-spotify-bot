using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Discord_Spotify_Bot.Models;
using System.IO;
using Newtonsoft.Json;
using Discord;
using Serilog;

namespace Discord_Spotify_Bot.Modules
{
    public class SearchSong : ModuleBase<SocketCommandContext>
    {
        int errorCounter = 0;

        [Command("song")]
        [Summary("Search for a song from Spotify")]
        public async Task SearchSongAsync([Remainder] string command)
        {
            LoadSecrets.LoadJson();
            using (var client = new HttpClient()){
                string token;
                try
                {
                    token = LoadSecrets.spotifyToken;

                }
                catch (FileNotFoundException)
                {
                    await RefreshSpotifyToken.GetTokenAsync();
                    token = LoadSecrets.spotifyToken;
                }
                var q = command.Replace(" ", "%20");
                var type = "track";
                var market = "ES";
                var limit = 1;
                var endpoint = client.BaseAddress = new Uri($"https://api.spotify.com/v1/search?q={q}&type={type}&market={market}&limit={limit}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = client.GetAsync(endpoint).Result;
                
                if (result.IsSuccessStatusCode) {
                    Track json = result.Content.ReadAsAsync<Track>().Result;
                    var displayedSeconds = "";
                    var duration = json.Tracks.Items[0].Duration;
                    var overallSeconds = duration / 1000;
                    var minutes = overallSeconds / 60;
                    int seconds = overallSeconds - (minutes * 60);
                    if (seconds < 10)
                    {
                        displayedSeconds = seconds.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        displayedSeconds = seconds.ToString();
                    }


                    var embed = new EmbedBuilder
                    {
                        Title = json.Tracks.Items[0].Name,
                        Url = json.Tracks.Items[0].ExternalUrls.Spotify,
                        ThumbnailUrl = json.Tracks.Items[0].Album.Images[0].Url,
                        Description = $"Album: {json.Tracks.Items[0].Album.Name}\nArtist: {json.Tracks.Items[0].Artists[0].Name}\nDuration: {minutes}:{displayedSeconds}"
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
                        var today = DateTime.Now.ToString("M-d-yyyy");
                        Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .WriteTo.File(@"C:\Users\xseam\Desktop\" + today + ".txt", rollingInterval: RollingInterval.Day)
                            .CreateLogger();
                        Log.Error(result.Content.ReadAsStringAsync().Result);
                        await ReplyAsync("We are sorry. Looks like something went wrong with our server, our Devs are on the case!");
                    }
                }

        }
            
        }
    }
}
