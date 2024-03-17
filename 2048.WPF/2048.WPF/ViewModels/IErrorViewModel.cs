namespace _2048.WPF.ViewModels;

public interface IErrorViewModel
{
    public string? ErrorMessage { get; set; }

    public bool HasErrorMessage { get; }
}
