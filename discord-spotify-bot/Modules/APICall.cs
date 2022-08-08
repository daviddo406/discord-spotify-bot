using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Discord_Spotify_Bot.Models;
using System.IO;

//Token Documentation https://johnnycrazy.github.io/SpotifyAPI-NET/docs/client_credentials
namespace Discord_Spotify_Bot.Modules
{
        public class APICall : ModuleBase<SocketCommandContext>
    {

        [Command("tracks")]
        [Summary("get tracks, this is for api testing")]
        public async Task GetTracksAsync(){
            using (var client = new HttpClient()){ // using keyword is used for closing the httpClient connection when no longer in use https://www.automationmission.com/2020/02/15/clean-up-your-net-with-the-dispose-pattern/
                var id = "12uzJ2TWi4IoxssgaNY8MO";
                var token = File.ReadAllText(@".\spotifyToken.txt");
                var endpoint = client.BaseAddress = new Uri($"https://api.spotify.com/v1/tracks/{id}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = client.GetAsync(endpoint).Result;
                if (result.IsSuccessStatusCode) {
                SpotifySongDetail json = result.Content.ReadAsAsync<SpotifySongDetail>().Result;
                }
                else {
                    await RefreshSpotifyToken.GetTokenAsync();
                }
        }
        }
}
}