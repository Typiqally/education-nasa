using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NasaMediaViewer
{
    public abstract class MediaPage : ContentPage
    {
        public abstract void OnActivate();
    }
}