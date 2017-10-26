using System;
using Newtonsoft.Json;

namespace SeDailyXamarin.Services
{
    public class PodCastStore
    {
 
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("categories")]
        public long[] Categories { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("score")]
        public long Score { get; set; }

        [JsonProperty("featuredImage")]
        public string FeaturedImage { get; set; }

        [JsonProperty("mp3")]
        public string Mp3 { get; set; }

        [JsonProperty("tags")]
        public long[] Tags { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }
    }
    public class Content
    {
        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("rendered")]
        public string Rendered { get; set; }
    }

    public class Title
    {
        [JsonProperty("rendered")]
        public string Rendered { get; set; }
    }
}
