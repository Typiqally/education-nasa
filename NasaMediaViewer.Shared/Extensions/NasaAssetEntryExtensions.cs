using System.Collections.Generic;
using System.Linq;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared.Extensions
{
    public static class NasaAssetEntryExtensions
    {
        public static NasaAssetEntry FindFirst(this IEnumerable<NasaAssetEntry> assets, string name, string extension = null)
        {
            return assets.FirstOrDefault(x =>
            {
                var href = x.Href.ToString();
                return href.Contains(name) && (extension == null || href.EndsWith(extension));
            });
        }
    }
}