using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NasaMediaViewer.Shared.Extensions;
using NasaMediaViewer.Shared.Models;
using Xamarin.Forms;

namespace NasaMediaViewer.Shared
{
    public sealed class NasaLazyLoader : ILazyLoader<NasaMediaEntry>
    {
        private readonly INasaClient _client;
        private readonly Func<NasaMediaEntry.Item, bool> _predicate;

        private NasaCollection<NasaMediaEntry> _collection;
        private IEnumerable<NasaMediaEntry.Item> _items;

        private int _loadedPreCount;
        private bool _isLoading, _isRefreshing;

        public NasaLazyLoader(INasaClient client, Func<NasaMediaEntry.Item, bool> predicate)
        {
            _client = client;
            _predicate = predicate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Task Initialize(NasaCollection<NasaMediaEntry> collection)
        {
            // Clear the loaded list and set the pre count to zero
            Loaded.Clear();
            _loadedPreCount = 0;

            _collection = collection;
            _items = collection.Entries
                .SelectMany(x => x.Items)
                .Where(_predicate);

            return Task.CompletedTask;
        }

        public async Task LoadAsync(int take = 10)
        {
            if (_collection == null)
            {
                throw new NullReferenceException("The collection has not been initialized.");
            }

            IsLoading = true;

            // Skip the items that are already loaded/being loaded and take the defined amount
            var itemsToLoad = _items
                .Skip(_loadedPreCount)
                .Take(take)
                .ToArray();

            // Increment the pre count
            _loadedPreCount += itemsToLoad.Length;

            // Update the assets and then finally add to the loaded list
            await itemsToLoad.UpdateAssets(_client);
            Loaded.AddRange(itemsToLoad);

            IsLoading = false;
        }

        public async Task RefreshAsync()
        {
            IsRefreshing = true;

            // Request media using the href from the current collection (a.k.a. the same collection) and then finally initialize it
            var collection = await _client.RequestMediaAsync(_collection.Href.ToString());
            await Initialize(collection);

            IsRefreshing = false;
        }

        public Command RefreshAsyncCommand => new Command(async () =>
        {
            await RefreshAsync();
            await LoadAsync();
        });

        public ObservableCollection<NasaMediaEntry.Item> Loaded { get; } = new ObservableCollection<NasaMediaEntry.Item>();

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool CanInitialize()
        {
            return _collection == null || _items == null;
        }
    }
}