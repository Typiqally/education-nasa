using System;
using System.Text.Json.Serialization;

namespace NasaMediaViewer.Shared.Models
{
    public class NasaAssetEntry
    {
        [JsonPropertyName("href")]
        public Uri Href { get; set; }
    }
}