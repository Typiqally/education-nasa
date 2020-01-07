using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared.Extensions
{
    public static class NasaMediaEntryExtensions
    {
        public static IEnumerable<NasaMediaEntry.Item> Filter(this IEnumerable<NasaMediaEntry> entries, Func<NasaMediaEntry.Item, bool> predicate)
        {
            return entries
                .SelectMany(x => x.Items)
                .Where(predicate);
        }

        public static IEnumerable<NasaMediaEntry.Item> Filter(this IEnumerable<NasaMediaEntry.Item> items, Func<NasaMediaEntry.Item, bool> predicate)
        {
            return items.Where(predicate);
        }

        public static async Task<IEnumerable<NasaMediaEntry.Item>> UpdateAssets(this IEnumerable<NasaMediaEntry.Item> items, INasaClient nasaClient)
        {
            var updateAssets = items as NasaMediaEntry.Item[] ?? items.ToArray();

            foreach (var item in updateAssets)
            {
                item.Assets = (await nasaClient.RequestAssetsAsync(item.NasaId)).Entries;
            }

            return updateAssets;
        }
    }
}