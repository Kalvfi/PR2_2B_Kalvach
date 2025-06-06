using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _05_WPF_07_Pexeso
{
    enum GameStage { Config, GuessFirst, GuessSecond, WaitForFlipBack, Finished }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameStage _gameStage = GameStage.Config;
        private int _rows;
        private int _cols;
        private Card _firstCard;
        private Card _secondCard;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            DisplayPanels();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(600);
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _timer.Stop();
            if (_secondCard.Content == _firstCard.Content)
            {
                Board.Children.Remove(_secondCard);
                Board.Children.Remove(_firstCard);
            }
            else
            {
                _secondCard.Flip();
                _firstCard.Flip();
            }
        }

        private void DisplayPanels() 
        {
            switch (_gameStage)
            {
                case GameStage.Config:
                    ConfigPanel.Visibility = Visibility.Visible;
                    GamePanel.Visibility = Visibility.Hidden;
                    ResultsPanel.Visibility = Visibility.Hidden;
                    break;
                case GameStage.GuessFirst:
                case GameStage.GuessSecond:
                    ConfigPanel.Visibility = Visibility.Hidden;
                    GamePanel.Visibility = Visibility.Visible;
                    ResultsPanel.Visibility = Visibility.Hidden;
                    break;
                case GameStage.Finished:
                    ConfigPanel.Visibility = Visibility.Hidden;
                    GamePanel.Visibility = Visibility.Hidden;
                    ResultsPanel.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void NextStage() 
        {
            switch (_gameStage)
            {
                case GameStage.Config:
                    Deal();
                    _gameStage = GameStage.GuessFirst;
                    break;
                case GameStage.GuessFirst:
                    _gameStage = GameStage.GuessSecond;
                    break;
                case GameStage.GuessSecond:
                    _gameStage = GameStage.WaitForFlipBack;
                    break;
                case GameStage.WaitForFlipBack:
                    _timer.Start();

                    break;
                case GameStage.Finished:
                    break;
                default:
                    break;
            }
            DisplayPanels();
        }

        private void Deal()
        {
            Board.Children.Clear();
            Board.ColumnDefinitions.Clear();
            Board.RowDefinitions.Clear();

            for (int i = 0; i < _cols; i++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < _rows; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Card card = new Card();
                    card.MouseDown += Card_MouseDown;
                    Grid.SetRow(card, i);
                    Grid.SetColumn(card, j);
                    Board.Children.Add(card);
                }
            }

            int[] nums = new int[_rows * _cols];
            for (int i = 0; i < _rows*_cols/2; i++)
            {
                nums[i] = i + 1;
                nums[i + _rows * _cols / 2] = i + 1;
            }

            Random rnd = new Random();
            nums = nums.OrderBy(x => rnd.Next()).ToArray();
            //Array.Sort(nums, new Comparison<int>((a, b) => rnd.Next(-1, 1)));

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Card card = (Card)Board.Children[i*_cols + j];
                    card.Symbol = nums[i*_cols+j].ToString();
                }
            }
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Card card = (Card)sender;
            if (card.IsFlipped)
                return;

            card.Flip();

            if (_gameStage == GameStage.GuessFirst)
            {
                _firstCard = card;
                _gameStage = GameStage.GuessSecond;
            }
            else if (_gameStage == GameStage.GuessSecond) 
            {
                _secondCard = card;
            }
            NextStage();
        }

        private void ConfigButton_Click(object sender, RoutedEventArgs e)
        {
            string btnText = ((Button)sender).Content.ToString()!;
            switch (btnText)
            {
                case "2x2":
                    _rows = 2;
                    _cols = 2;
                    break;
                case "4x4":
                    _rows = 4;
                    _cols = 4;
                    break;
                case "6x4":
                    _rows = 6;
                    _cols = 4;
                    break;
            }
            NextStage();
        }
    }
}