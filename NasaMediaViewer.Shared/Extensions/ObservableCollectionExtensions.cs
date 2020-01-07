using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NasaMediaViewer.Shared.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> collection)
        {
            foreach (var element in collection)
            {
                observableCollection.Add(element);
            }
        }
    }
}