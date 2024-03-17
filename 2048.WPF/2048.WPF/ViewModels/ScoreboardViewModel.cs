namespace _2048.WPF.ViewModels;

public class ScoreboardViewModel : ViewModelBase
{
    private int _currentScore = 0;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            OnPropertyChanged(nameof(CurrentScore));
        }
    }

    private int _highScore = 0;

    public int HighScore
    {
        get => _highScore;
        set
        {
            _highScore = value;
            OnPropertyChanged(nameof(HighScore));
        }
    }
}
