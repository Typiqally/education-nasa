using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace NasaMediaViewer.Shared.Models
{
    public class NasaCollection<T>
    {
        [JsonPropertyName("items")]
        public T[] Entries { get; set; }

        [JsonPropertyName("href")]
        public Uri Href { get; set; }

        [JsonPropertyName("metadata")]
        [CanBeNull]
        public dynamic MetaData { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}