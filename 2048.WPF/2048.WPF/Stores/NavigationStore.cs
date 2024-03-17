using _2048.WPF.ViewModels;

namespace _2048.WPF.Stores;

public class NavigationStore
{
    public static NavigationStore? Instance { get; set; }

    public static void CreateInstance()
    {
        Instance ??= new();
    }

    private NavigationStore() { }

    public event Action CurrentViewModelChanged;

    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

}
