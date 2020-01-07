using NasaMediaViewer.Shared;
using NasaMediaViewer.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NasaMediaViewer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopularMediaPage : MediaPage
    {
        private readonly NasaClient _client;
        private NasaLazyLoader _loader;

        public PopularMediaPage()
        {
            InitializeComponent();

            _client = NasaClient.Default();
            Loader = new NasaLazyLoader(_client, x => x.MediaType == MediaType.Image);
        }

        public override async void OnActivate()
        {
            if (Loader.CanInitialize())
            {
                Loader.IsRefreshing = true;

                // Request sorted media then finally initialize it
                var collection = await _client.RequestSortedMediaAsync(OrderType.Popular);
                await Loader.Initialize(collection);

                Loader.IsRefreshing = false;
            }

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