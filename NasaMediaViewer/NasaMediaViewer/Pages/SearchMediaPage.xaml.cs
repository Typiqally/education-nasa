using System;
using NasaMediaViewer.Shared;
using NasaMediaViewer.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NasaMediaViewer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchMediaPage
    {
        private readonly NasaClient _client;
        private NasaLazyLoader _loader;

        public SearchMediaPage()
        {
            InitializeComponent();

            _client = NasaClient.Default();
            Loader = new NasaLazyLoader(_client, x => x.MediaType == MediaType.Image);
        }

        private async void SearchEntry_OnCompleted(object sender, EventArgs e)
        {
            Loader.IsRefreshing = true;

            // Build a search request then finally execute it
            var collection = await _client.BuildSearchRequest()
                .ByQuery(SearchEntry.Text)
                .ExecuteAsync();

            // Initialize the collection
            await Loader.Initialize(collection);

            Loader.IsRefreshing = false;

            // If the loader count is zero then load more images
            if (Loader.Loaded.Count == 0)
            {
                await Loader.LoadAsync();
            }
        }

        private async void MediaListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            // If the item index is equal to the loaded count - 1 (a.k.a. is the last item appearing) then load more images
            if (e.ItemIndex == Loader.Loaded.Count - 1)
            {
                await Loader.LoadAsync();
            }
        }

        public NasaLazyLoader Loader
        {
            get => _loader;
            set
            {
                _loader = value;
                OnPropertyChanged(nameof(Loader));
            }
        }
    }
}