using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using SpotifyAPI.Web;


namespace Discord_Spotify_Bot.Modules
{
        public class RefreshSpotifyToken : ModuleBase<SocketCommandContext> {

        public static async Task GetTokenAsync(){

                var id = File.ReadAllText(@".\clientID.txt");
                var secret = File.ReadAllText(@".\clientSecret.txt");

                var config = SpotifyClientConfig.CreateDefault();
                var request = new ClientCredentialsRequest(id, secret);
                var response = await new OAuthClient(config).RequestToken(request);
                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));
                File.WriteAllText("spotifyToken.txt", response.AccessToken);
        }
}
}