using _2048.WPF.Commands;
using _2048.WPF.Stores;

namespace _2048.WPF.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel => NavigationStore.Instance!.CurrentViewModel;

    public MainViewModel()
    {
        NavigationStore.Instance!.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
