using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TabbedPage = NasaMediaViewer.Pages.TabbedPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace NasaMediaViewer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabbedPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}