using System.ComponentModel;
using System.Threading.Tasks;
using NasaMediaViewer.Shared.Models;

namespace NasaMediaViewer.Shared
{
    public interface ILazyLoader<T> : INotifyPropertyChanged
    {
        Task Initialize(NasaCollection<T> collection);
        
        Task LoadAsync(int take);

        Task RefreshAsync();
    }
}