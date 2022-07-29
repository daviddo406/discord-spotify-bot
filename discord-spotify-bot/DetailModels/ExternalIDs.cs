using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Models
{
    public class ExternalIDs
    {
        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }
}