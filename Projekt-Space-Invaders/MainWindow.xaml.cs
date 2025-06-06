using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

namespace Projekt_Space_Invaders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string filePath = "data\\leaderboards.json";

        private PlayerShip player;
        private DispatcherTimer gameTimer;
        private bool isLeftPressed;
        private bool isRightPressed;
        private List<Bullet> activeBullets = new List<Bullet>();
        private List<EnemyShip> activeEnemies = new List<EnemyShip>();
        private Random gen = new Random();
        private int enemyShootTimer = 0;

        public int Points
        {
            get { return (int)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        //-------------------------------------------< Constructor >-------------------------------------------

        public MainWindow()
        {
            InitializeComponent();

            gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            gameTimer.Tick += GameLoop;
        }

        //---------------------------------------------< Buttons >---------------------------------------------

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Visible;
            GameOver.Visibility = Visibility.Collapsed;
            Leaderboard.Visibility = Visibility.Collapsed;

            RenderGame();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Visible;
            Game.Visibility = Visibility.Collapsed;
            GameOver.Visibility = Visibility.Collapsed;
            Leaderboard.Visibility = Visibility.Collapsed;
        }

        private void BoardButton_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Collapsed;
            GameOver.Visibility = Visibility.Collapsed;
            Leaderboard.Visibility = Visibility.Visible;
            RenderBoard();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //-------------------------------------------< Game Logic >--------------------------------------------

        private void RenderGame()
        {
            Game.Children.Clear();
            activeBullets.Clear();
            activeEnemies.Clear();
            Points = 0;

            TextBlock scoreLabel = new TextBlock
            {
                FontSize = 20,
                Foreground = Brushes.White
            };

            // Set up the binding
            Binding scoreBinding = new Binding("Points")
            {
                Source = this,
                StringFormat = "Score: {0}"
            };
            scoreLabel.SetBinding(TextBlock.TextProperty, scoreBinding);

            Canvas.SetLeft(scoreLabel, 10);
            Canvas.SetTop(scoreLabel, 10);
            Game.Children.Add(scoreLabel);

            player = new PlayerShip();
            Game.Children.Add(player);

            player.Loaded += (s, e) =>
            {
                double startX = (Game.ActualWidth - player.ActualWidth) / 2;
                double startY = Game.ActualHeight - player.ActualHeight - 20;
                Canvas.SetLeft(player, startX);
                Canvas.SetTop(player, startY);
                player.UpdateHitbox();
            };

            GenerateEnemies();

            Game.Focus();
            gameTimer.Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            //Player movement
            double left = Canvas.GetLeft(player);
            if (isLeftPressed)
            {
                Canvas.SetLeft(player, Math.Max(0, left - player.Speed));
                player.UpdateHitbox();
            }
            if (isRightPressed)
            {
                Canvas.SetLeft(player, Math.Min(Game.ActualWidth - player.ActualWidth, left + player.Speed));
                player.UpdateHitbox();
            }

            //Enemy movement
            bool anyEnemyOutOfBounds = false;
            for (int i = activeEnemies.Count - 1; i >= 0; i--)
            {
                EnemyShip enemy = activeEnemies[i];
                enemy.Move();

                if (enemy.IsOutOfBounds(Game.ActualWidth))
                {
                    anyEnemyOutOfBounds = true;
                }
            }

            if (anyEnemyOutOfBounds)
            {
                foreach (EnemyShip enemyShip in activeEnemies)
                {
                    enemyShip.MoveDown();
                    enemyShip.SwitchDirection();
                }
            }

            foreach (EnemyShip enemy in activeEnemies)
            {
                if (enemy.Hitbox.IntersectsWith(player.Hitbox))
                {
                    EndGame();
                    return;
                }
            }

            HandleEnemyShooting();

            //Bullet movement
            for (int i = activeBullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = activeBullets[i];
                bullet.Move();

                if (bullet.IsFromPlayer)
                {
                    //Check collision with enemies
                    for (int j = activeEnemies.Count - 1; j >= 0; j--)
                    {
                        EnemyShip enemy = activeEnemies[j];
                        if (bullet.Hitbox.IntersectsWith(enemy.Hitbox))
                        {
                            Points += enemy.Value;
                            Game.Children.Remove(enemy);
                            activeEnemies.RemoveAt(j);
                            Game.Children.Remove(bullet);
                            activeBullets.RemoveAt(i);
                            break;
                        }
                    }
                }
                else
                {
                    //Check collision with player
                    if (bullet.Hitbox.IntersectsWith(player.Hitbox))
                    {
                        Game.Children.Remove(bullet);
                        activeBullets.RemoveAt(i);

                        EndGame();
                        return;
                    }
                }

                //Remove bullet if out of bounds
                if (bullet.IsOutOfBounds(Game.ActualHeight))
                {
                    Game.Children.Remove(bullet);
                    activeBullets.RemoveAt(i);
                }
            }

            if (activeEnemies.Count == 0)
            {
                Points += 500;
                GenerateEnemies();
            }
        }

        private void GenerateEnemies()
        {
            const double startX = 30;
            const double startY = 30;
            const double columnSpacing = 40;
            const double rowSpacing = 30;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    int row = i;
                    int col = j;

                    EnemyShip enemy = row switch
                    {
                        0 => new EnemyShip(ShipType.Squid),
                        1 or 2 => new EnemyShip(ShipType.Crab),
                        3 or 4 => new EnemyShip(ShipType.Octopus)
                    };
                    Game.Children.Add(enemy);

                    enemy.Loaded += (s, e) =>
                    {
                        double left = startX + (col * columnSpacing) + (columnSpacing / 2) - (enemy.Width / 2);
                        double top = startY + (row * rowSpacing);

                        Canvas.SetLeft(enemy, left);
                        Canvas.SetTop(enemy, top);

                        enemy.Hitbox = new Rect(
                            Canvas.GetLeft(enemy),
                            Canvas.GetTop(enemy),
                            enemy.Width,
                            20);
                        };

                    activeEnemies.Add(enemy);
                }
            }
        }

        private void HandleEnemyShooting()
        {
            enemyShootTimer++;

            if (enemyShootTimer == 30)
            {
                enemyShootTimer = 0;

                //List<EnemyShip> candidates = GetBottomEnemies();

                //int shooterCount = Math.Min(gen.Next(1, 4), candidates.Count);
                //List <EnemyShip> shooters = candidates.OrderBy(x => gen.Next()).Take(shooterCount).ToList();

                //foreach (EnemyShip enemy in shooters)
                //{
                //    Shoot(enemy);
                //}

                List<EnemyShip> candidates = activeEnemies.GroupBy(enemy => Math.Round(Canvas.GetLeft(enemy) / 40)).Select(column => column.OrderByDescending(Canvas.GetTop).First()).ToList();
                List<EnemyShip> shooters = candidates.OrderBy(x => gen.Next()).Take(Math.Min(gen.Next(1, 4), candidates.Count)).ToList();
                shooters.ForEach(Shoot);

            }

        }
        //private List<EnemyShip> GetBottomEnemies()
        //{
        //    List<EnemyShip> bottomEnemies = new List<EnemyShip>();

        //    List<IGrouping<double, EnemyShip>> columns = activeEnemies.GroupBy(enemy => Math.Round(Canvas.GetLeft(enemy) / 40)).ToList();
        //    foreach (IGrouping<double, EnemyShip> column in columns)
        //    {
        //        EnemyShip bottomEnemy = column.OrderByDescending(enemy => Canvas.GetTop(enemy)).First();
        //        bottomEnemies.Add(bottomEnemy);
        //    }

        //    return bottomEnemies;
        //}

        private void Shoot()
        {
            if (activeBullets.Any(bullet => bullet.IsFromPlayer)) return;

            Bullet bullet = new Bullet(true);
            Game.Children.Add(bullet);

            bullet.Loaded += (s, e) =>
            {
                Canvas.SetLeft(bullet, Canvas.GetLeft(player) + (player.ActualWidth - bullet.ActualWidth) / 2);
                Canvas.SetTop(bullet, Canvas.GetTop(player) - bullet.ActualHeight);

                bullet.UpdateHitbox();
            };

            activeBullets.Add(bullet);
        }
        private void Shoot(EnemyShip enemy)
        {
            Bullet bullet = new Bullet(false);
            Game.Children.Add(bullet);

            bullet.Loaded += (s, e) =>
            {
                Canvas.SetLeft(bullet, Canvas.GetLeft(enemy) + (enemy.ActualWidth - bullet.ActualWidth) / 2);
                Canvas.SetTop(bullet, Canvas.GetTop(enemy) + enemy.ActualHeight);
                bullet.UpdateHitbox();
            };

            activeBullets.Add(bullet);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                isLeftPressed = true;
            if (e.Key == Key.Right)
                isRightPressed = true;
            if (e.Key == Key.Space)
                Shoot();
        }
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                isLeftPressed = false;
            if (e.Key == Key.Right)
                isRightPressed = false;
        }

        //--------------------------------< Game Over & Leaderboard Managing >---------------------------------

        private void EndGame()
        {
            gameTimer.Stop();
            Game.Children.Clear();
            activeBullets.Clear();
            activeEnemies.Clear();

            Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Collapsed;
            GameOver.Visibility = Visibility.Visible;
            Leaderboard.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = PlayerNameBox.Text;
            SaveScore(playerName, Points);

            Points = 0;
            PlayerNameBox.Text = "";

            Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Collapsed;
            GameOver.Visibility = Visibility.Collapsed;
            Leaderboard.Visibility = Visibility.Visible;
            RenderBoard();
        }
        private void SaveScore(string playerName, int score)
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    leaderboard = JsonSerializer.Deserialize<List<LeaderboardEntry>>(json) ?? new List<LeaderboardEntry>();
                }
            }

            leaderboard.Add(new LeaderboardEntry { Name = playerName, Score = score });

            string updatedJson = JsonSerializer.Serialize(leaderboard, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);
        }

        private void RenderBoard()
        {
            const int lineLength = 55;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Leaderboard file not found.");

                Menu.Visibility = Visibility.Visible;
                Game.Visibility = Visibility.Collapsed;
                GameOver.Visibility = Visibility.Collapsed;
                Leaderboard.Visibility = Visibility.Collapsed;

                return;
            }

            string json = File.ReadAllText(filePath);
            List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

            if (!string.IsNullOrWhiteSpace(json))
            {
                leaderboard = JsonSerializer.Deserialize<List<LeaderboardEntry>>(json) ?? new List<LeaderboardEntry>();
            }

            leaderboard = leaderboard.OrderByDescending(entry => entry.Score).ToList();

            BoardListBox.Items.Clear();
            for (int i = 0; i < 30; i++)
            {
                if (i < leaderboard.Count)
                {
                    int dotsCount = lineLength - leaderboard[i].Name.Length - leaderboard[i].Score.ToString().Length;
                    BoardListBox.Items.Add($"{leaderboard[i].Name}{new string('.', dotsCount)}{leaderboard[i].Score}");
                }
                else
                {
                    BoardListBox.Items.Add(new string('.', lineLength));
                }
            }
        }

        private class LeaderboardEntry
        {
            public string Name { get; set; }
            public int Score { get; set; }
        }
    }
}