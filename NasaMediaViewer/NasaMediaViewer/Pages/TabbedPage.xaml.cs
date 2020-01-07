using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NasaMediaViewer.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class TabbedPage : Xamarin.Forms.TabbedPage
    {
        public TabbedPage()
        {
            InitializeComponent();
        }

        protected override async void OnCurrentPageChanged()
        {
            // Invoke base method
            base.OnCurrentPageChanged();

            // If the current page is not a navigation page; return
            if (!(CurrentPage is NavigationPage page))
            {
                return;
            }

            // If the root page of the current navigation page is a media page
            if (page.RootPage is MediaPage mediaPage)
            {
                // Invoke the OnActivate event
                await Task.Run(() => mediaPage.OnActivate());
            }
        }
    }
}