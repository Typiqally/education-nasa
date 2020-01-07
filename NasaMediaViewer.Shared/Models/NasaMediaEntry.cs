using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using NasaMediaViewer.Shared.Extensions;

namespace NasaMediaViewer.Shared.Models
{
    public class NasaMediaEntry
    {
        [JsonPropertyName("data")]
        public Item[] Items { get; set; }

        [JsonPropertyName("href")]
        public Uri Href { get; set; }

        public class Item
        {
            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("date_created")]
            public DateTime DateCreated { get; set; }

            [JsonPropertyName("center")]
            public string Center { get; set; }

            [JsonPropertyName("keywords")]
            public string[] Keywords { get; set; }

            [JsonPropertyName("description_508")]
            public string Summary { get; set; } = "No description";

            [JsonPropertyName("nasa_id")]
            public string NasaId { get; set; }

            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("secondary_creator")]
            [CanBeNull]
            public string SecondaryCreator { get; set; }

            [JsonPropertyName("media_type")]
            public MediaType MediaType { get; set; }

            public IEnumerable<NasaAssetEntry> Assets { get; set; }

            public Uri Thumbnail => Assets?.FindFirst("thumb").Href;
        }

        public class ItemEventArgs : EventArgs
        {
            public ItemEventArgs(Item item)
            {
                Item = item;
            }
            
            public Item Item { get; set; }
        }
    }
}