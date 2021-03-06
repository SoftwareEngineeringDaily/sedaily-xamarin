﻿using Newtonsoft.Json;
using System;
using System.ComponentModel;


namespace SeDailyXamarin.PageModels
{

    public class FeedItem
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

        public string Heading
        {
            get
            {
                return Title.Rendered;
            }
        }
        public string FormattedDate
        {
            get
            {
                if (DateTime.TryParse(Date, out DateTime dateValue))
                {
                    return String.Format("{0:dddd, MMMM d, yyyy}", dateValue); ;
                }
                return "";
            }
        }
    }
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
