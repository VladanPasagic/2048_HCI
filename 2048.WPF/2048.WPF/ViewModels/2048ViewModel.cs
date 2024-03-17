using _2048.WPF.Commands;
using _2048.WPF.Stores;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace _2048.WPF.ViewModels;

public class _2048ViewModel : ViewModelBase, IModalViewModel, IErrorViewModel, ILoadingViewModel
{
    private bool _isOpen = false;
    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            OnPropertyChanged(nameof(IsOpen));
        }
    }

    public int Size { get; private set; } = 4;

    public bool HasStarted { get; set; }

    public object lockObject = new();

    private readonly Dictionary<int, SolidColorBrush> tileColors = new()
    {
        {2, new SolidColorBrush(Color.FromRgb(229, 192, 81))},
        {4, new SolidColorBrush(Color.FromRgb(224, 181, 50)) },
        {8, new SolidColorBrush(Color.FromRgb(219,167, 13)) },
        {16, new SolidColorBrush(Color.FromRgb(86, 151, 171)) },
        {32, new SolidColorBrush(Color.FromRgb(49, 128, 153)) },
        {64, new SolidColorBrush(Color.FromRgb(19,110, 138)) },
        {128, new SolidColorBrush(Color.FromRgb(233, 106, 137)) },
        {256, new SolidColorBrush(Color.FromRgb(228,75,113)) },
        {512, new SolidColorBrush(Color.FromRgb(219,13,64)) },
        {1024, new SolidColorBrush(Color.FromRgb(88, 48, 80)) },
        {2048, new SolidColorBrush(Color.FromRgb(35, 64, 55)) },
        {4096, new SolidColorBrush(Color.FromRgb(5, 41, 31)) }
    };

    public int CurrentEmptyTiles { get; set; }

    public SingleTileControlViewModel[][] SingleTileControlViewModel { get; set; }

    public ICommand KeyDownCommand { get; }

    public ICommand KeySCommand { get; }

    public ICommand KeyUpCommand { get; }

    public ICommand KeyWCommand { get; }

    public ICommand KeyLeftCommand { get; }

    public ICommand KeyACommand { get; }

    public ICommand KeyRightCommand { get; }

    public ICommand KeyDCommand { get; }

    public ScoreboardViewModel Scoreboard { get; }

    public ICommand StartGameCommand { get; }

    public ICommand ResetGameCommand { get; }

    public ICommand ViewLeaderboardCommand { get; }

    public LeaderboardModalViewModel LeaderboardModalViewModel { get; }
    public string? ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool HasErrorMessage => throw new NotImplementedException();

    public bool IsLoading { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public _2048ViewModel()
    {
        KeyDownCommand = new KeyPressedCommand(this, Key.Down);
        KeySCommand = new KeyPressedCommand(this, Key.S);
        KeyUpCommand = new KeyPressedCommand(this, Key.Up);
        KeyWCommand = new KeyPressedCommand(this, Key.W);
        KeyRightCommand = new KeyPressedCommand(this, Key.Right);
        KeyDCommand = new KeyPressedCommand(this, Key.D);
        KeyLeftCommand = new KeyPressedCommand(this, Key.Left);
        KeyACommand = new KeyPressedCommand(this, Key.A);
        StartGameCommand = new StartGameCommand(this);
        ResetGameCommand = new ResetGameCommand(this);

        LeaderboardModalViewModel = new(new CloseModalCommand(this));
        ViewLeaderboardCommand = new ViewLeaderboardCommand(this);

        Scoreboard = new();
        LeaderboardStore.Instance!.LeaderboardRead += _2048ViewModel_LeaderboardRead;
        new LoadAllLeaderboardEntriesCommand(this, this).Execute(null);

        CurrentEmptyTiles = Size * Size;
        SingleTileControlViewModel = new SingleTileControlViewModel[Size][];

        for (int i = 0; i < Size; i++)
        {
            SingleTileControlViewModel[i] = new SingleTileControlViewModel[Size];
            for (int j = 0; j < Size; j++)
            {
                SingleTileControlViewModel[i][j] = new();
            }
        }
    }

    private void _2048ViewModel_LeaderboardRead()
    {
        var entries = LeaderboardStore.Instance!.Entries;

        Scoreboard.HighScore = entries.First().Score;
    }

    public void FIllTile()
    {
        lock (lockObject)
        {
            Random rand = new();
            int num = rand.Next(CurrentEmptyTiles);
            foreach (var tileArray in SingleTileControlViewModel)
            {
                foreach (var tile in tileArray)
                {
                    if (string.IsNullOrEmpty(tile.Value))
                    {
                        if (num != 0)
                        {
                            num--;
                        }
                        else
                        {
                            double fraction = rand.NextDouble();
                            if (fraction < 0.1)
                            {
                                tile.Color = tileColors[4];
                                tile.Value = "4";
                            }
                            else
                            {
                                tile.Color = tileColors[2];
                                tile.Value = "2";
                            }
                            CurrentEmptyTiles--;
                            return;
                        }
                    }
                }
            }
        }
    }

    public async Task<bool> MergeAndDouble(SingleTileControlViewModel first, SingleTileControlViewModel second)
    {
        if (!string.IsNullOrEmpty(first.Value) && first.Value.Equals(second.Value))
        {
            int value = int.Parse(first.Value);
            value *= 2;

            first.Color = tileColors[value];
            second.Color = new SolidColorBrush(Colors.White);

            Scoreboard.CurrentScore += value;
            if (Scoreboard.CurrentScore >= Scoreboard.HighScore)
            {
                Scoreboard.HighScore = Scoreboard.CurrentScore;
            }

            first.Value = value.ToString();
            second.Value = string.Empty;
            CurrentEmptyTiles++;

            if (value == 2048)
            {
                MessageBox.Show("Congratulations. You Won.");
                HasStarted = false;
                await LeaderboardStore.Instance!.Create(new Domain.Models.LeaderboardEntry()
                {
                    User = UserStore.Instance!.ActiveUser!,
                    Score = Scoreboard.CurrentScore,
                    Id = new Guid(),
                });
                return true;
            }

            return true;
        }
        return false;
    }

    public void MoveRight(int j)
    {
        List<SingleTileControlViewModel> singleTileControls = [];
        int i = Size - 1;
        for (; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(SingleTileControlViewModel[i][j].Value))
            {
                singleTileControls.Add(SingleTileControlViewModel[i][j]);
            }
        }

        i = Size - 1;
        foreach (var singleTileControl in singleTileControls)
        {
            SingleTileControlViewModel[i][j].Value = singleTileControl.Value;
            SingleTileControlViewModel[i][j].Color = singleTileControl.Color;
            i--;
        }

        for (; i >= 0; i--)
        {
            SingleTileControlViewModel[i][j].Value = string.Empty;
            SingleTileControlViewModel[i][j].Color = new SolidColorBrush(Colors.White);
        }
    }

    public void MoveLeft(int j)
    {
        List<SingleTileControlViewModel> singleTileControls = [];
        int i = 0;
        for (; i < Size; i++)
        {
            if (!string.IsNullOrEmpty(SingleTileControlViewModel[i][j].Value))
            {
                singleTileControls.Add(SingleTileControlViewModel[i][j]);
            }
        }

        i = 0;
        foreach (var singleTileControl in singleTileControls)
        {
            SingleTileControlViewModel[i][j].Value = singleTileControl.Value;
            SingleTileControlViewModel[i][j].Color = singleTileControl.Color;
            i++;
        }

        for (; i < Size; i++)
        {
            SingleTileControlViewModel[i][j].Value = string.Empty;
            SingleTileControlViewModel[i][j].Color = new SolidColorBrush(Colors.White);
        }
    }

    public void MoveDown(int i)
    {
        List<SingleTileControlViewModel> singleTileControls = [];
        singleTileControls = SingleTileControlViewModel[i].Where((item) => !string.IsNullOrEmpty(item.Value)).Reverse().ToList();
        int j = Size - 1;
        foreach (var singleTileControl in singleTileControls)
        {
            SingleTileControlViewModel[i][j].Value = singleTileControl.Value;
            SingleTileControlViewModel[i][j].Color = singleTileControl.Color;
            j--;
        }

        for (; j >= 0; j--)
        {
            SingleTileControlViewModel[i][j].Value = string.Empty;
            SingleTileControlViewModel[i][j].Color = new SolidColorBrush(Colors.White);
        }
    }

    public void MoveUp(int i)
    {
        List<SingleTileControlViewModel> singleTileControls = [];
        singleTileControls = SingleTileControlViewModel[i].Where((item) => !string.IsNullOrEmpty(item.Value)).ToList();
        int j = 0;
        foreach (var singleTileControl in singleTileControls)
        {
            SingleTileControlViewModel[i][j].Value = singleTileControl.Value;
            SingleTileControlViewModel[i][j].Color = singleTileControl.Color;
            j++;
        }

        for (; j < Size; j++)
        {
            SingleTileControlViewModel[i][j].Value = string.Empty;
            SingleTileControlViewModel[i][j].Color = new SolidColorBrush(Colors.White);
        }
    }

    public bool CheckDidMove(List<List<string>> matrix)
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (!matrix[i][j].Equals(SingleTileControlViewModel[i][j].Value))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CheckIfCanContinue()
    {
        var matrix = SingleTileControlViewModel;
        bool hasSimilar = false;
        if (HasStarted && CurrentEmptyTiles == 0)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size - 1; j++)
                {
                    if (matrix[i][j].Value == matrix[i][j + 1].Value)
                        hasSimilar = true;
                }
            }

            for (int i = 0; i < Size - 1; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (matrix[i][j].Value == matrix[i + 1][j].Value)
                        hasSimilar = true;
                }
            }
            if (!hasSimilar)
                EndGame();
        }
    }

    private async void EndGame()
    {
        HasStarted = false;
        MessageBox.Show("Game Over");
        await LeaderboardStore.Instance!.Create(new Domain.Models.LeaderboardEntry()
        {
            User = UserStore.Instance!.ActiveUser!,
            Score = Scoreboard.CurrentScore,
            Id = new Guid(),
        });
    }

}
