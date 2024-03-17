namespace _2048.WPF.Commands;

public abstract class AsyncCommandBase : CommandBase
{
    private bool _isExecuting;

    public bool IsExecuting
    {
        get => _isExecuting;
        set
        {
            _isExecuting = value;
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter)
    {
        _isExecuting = true;
        OnCanExecuteChanged();
        try
        {
            await ExecuteAsync(parameter);
        }
        catch
        {

        }
        finally
        {
            IsExecuting = false;
        }
    }

    public abstract Task ExecuteAsync(object? parameter);
}
