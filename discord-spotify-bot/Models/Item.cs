using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Models
{
    public class Item
    {
        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("duration_ms")]
        public int Duration { get; set; }
    }
}
