﻿using System.Text.Json.Serialization;

 namespace NasaMediaViewer.Shared.Models
{
    public class NasaCollectionDto<T>
    {
        [JsonPropertyName("collection")]
        public NasaCollection<T> Collection { get; set; }
    }
}