using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Spotify_Bot.Models
{
    public class Track
    {
        [JsonProperty("tracks")]
        public TrackDetails Tracks { get; set; }
    }
}
