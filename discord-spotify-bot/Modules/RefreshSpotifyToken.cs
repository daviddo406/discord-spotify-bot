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
using Newtonsoft.Json.Linq;


//Token Documentation https://johnnycrazy.github.io/SpotifyAPI-NET/docs/client_credentials
namespace Discord_Spotify_Bot.Modules
{
    public class RefreshSpotifyToken : ModuleBase<SocketCommandContext> {

        public static async Task GetTokenAsync()
        {
            LoadSecrets.LoadJson();

            var id = LoadSecrets.spotifyClientId;
            var secret = LoadSecrets.spotifyClientSecret;

                var config = SpotifyClientConfig.CreateDefault();
                var request = new ClientCredentialsRequest(id, secret);
                var response = await new OAuthClient(config).RequestToken(request);
                var spotify = new SpotifyClient(config.WithToken(response.AccessToken));
            // File.WriteAllText("spotifyToken.txt", response.AccessToken);
            // Writing JSON files: https://stackoverflow.com/questions/21695185/change-values-in-json-file-writing-files

            string jsonString = File.ReadAllText("secrets.json");
            // Convert the JSON string to a JObject:
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString) as JObject;
            // Select a nested property using a single string:
            JToken jToken = jObject.SelectToken("spotifyToken");
            // Update the value of the property: 
            jToken.Replace(response.AccessToken);
            // Convert the JObject back to a string:
            string updatedJsonString = jObject.ToString();
            File.WriteAllText("secrets.json", updatedJsonString);
        }
    }
}