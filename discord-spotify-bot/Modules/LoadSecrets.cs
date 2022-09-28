using Discord_Spotify_Bot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Discord_Spotify_Bot.Modules
{
    public static class LoadSecrets
    {
        public static string spotifyClientId;
        public static string spotifyClientSecret;
        public static string spotifyToken;
        public static string discordToken;
        public static void LoadJson()
        {
            var counter = 0;
            foreach (string line in File.ReadLines("secrets.json")) {
            if (String.Equals(line, '}')) {
                break;
            }
            if (counter != 0)
                {
                    Console.WriteLine(line);
                    string[] parts = line.Split(':');
                    string key = parts[0];
                    string value = parts[1];
                    switch (key)
                    {
                        case ("spotifyClientID"):
                            spotifyClientId = value;
                            break;

                        case ("spotifyClientSecret"):
                            spotifyClientSecret = value;
                            break;

                        case ("spotifyToken"):
                            spotifyToken = value;
                            break;

                        case ("discordToken"):
                            discordToken = value;
                            break;
                    }
                }
                counter++;
            }
        }
    }
}
