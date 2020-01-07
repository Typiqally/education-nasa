﻿using System.Text.Json.Serialization;

 namespace NasaMediaViewer.Shared.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MediaType
    {
        Image,
        Video,
        Audio
    }
}