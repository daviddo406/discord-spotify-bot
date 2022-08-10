using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord_Spotify_Bot.Models;
using System.IO;


namespace Discord_Spotify_Bot.Modules
{
    public class APICall : ModuleBase<SocketCommandContext>
    {
        int errorCounter = 0;

        [Command("tracks")]
        [Summary("get tracks, this is for api testing")]
        public async Task GetTracksAsync()
        {
            using (var client = new HttpClient())
            { // using keyword is used for closing the httpClient connection when no longer in use https://www.automationmission.com/2020/02/15/clean-up-your-net-with-the-dispose-pattern/
                var id = "12uzJ2TWi4IoxssgaNY8MO";
                string token = "";
                try
                {
                    token = File.ReadAllText(@".\spotifyToken.txt");
                }
                catch (FileNotFoundException)
                {
                    await RefreshSpotifyToken.GetTokenAsync();
                }
                var endpoint = client.BaseAddress = new Uri($"https://api.spotify.com/v1/tracks/{id}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = client.GetAsync(endpoint).Result;
                if (result.IsSuccessStatusCode) {
                SpotifySongDetail json = result.Content.ReadAsAsync<SpotifySongDetail>().Result;
                await ReplyAsync("https://open.spotify.com/track/7a86XRg84qjasly9f6bPSD?si=cd0cdb460741405e");
                }
                else {
                    if (errorCounter == 0)
                    {
                        errorCounter++;
                        await RefreshSpotifyToken.GetTokenAsync();
                        await GetTracksAsync();
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
