using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Discord_Spotify_Bot.Modules
{
        public class APICall : ModuleBase<SocketCommandContext>
    {

        [Command("tracks")]
        [Summary("get tracks, this is for api testing")]
        public async Task GetTracksAsync(){
            using(var client = new HttpClient()){ // using keyword is used for closing the httpClient connection when no longer in use https://www.automationmission.com/2020/02/15/clean-up-your-net-with-the-dispose-pattern/
                var id = "11dFghVXANMlKmJXsNCbNl";
                var token = "BQDFjexA-x445OLXJheFy8mh1m3VEth45bJU0PXr46fG_BuTLu6P621IHqZURVtrHLiAfDrfAT7RMnCWubnFIsFqM_E4v5eterAgj20A_TwpwR78X0lNczvv2QKQo1eYDUNbiFLsGMFz2piOAXKCpfhJqi0js84K9mLODtrUqPc_ouY";
                var endpoint = client.BaseAddress = new Uri($"https://api.spotify.com/v1/tracks/{id}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
        }
        }
}
}