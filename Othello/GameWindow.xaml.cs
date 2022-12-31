using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Othello
{
    public partial class GameWindow : Window
    {
        private int _score1, _score2;
        public int Score1
        {
            get { return _score1; }
            set
            {
                _score1 = value;
                tbScore1.Text = _score1.ToString();
            }
        }
        public int Score2
        {
            get { return _score2; }
            set
            {
                _score2 = value;
                tbScore2.Text = _score2.ToString();
            }
        }
        private string _player1, _player2;
        private string _winner;
        private bool _pvp;
        private DispatcherTimer timer;
        private Stopwatch stopWatch;
        private Model.Player playerOne;
        private Model.Player playerTwo;
        private Model.Table table;

        public GameWindow(string player1, string player2, bool pvp)
        {
            InitializeComponent();
            _player1 = player1;
            _player2 = player2;
            _pvp = pvp;

            playerOne = new Model.Player(Model.PlayerType.Human, _player1);
            if (pvp)
            {
                playerTwo = new Model.Player(Model.PlayerType.Human, _player2);
            }
            else
            {
                playerTwo = new Model.Player(Model.PlayerType.AI, "Computer");
            }
            table = new Model.Table(playerOne, playerTwo);

            // Draw the disks and update scoreboard.
            Draw();

            // If the starting player is AI, tell it to make the first move.
            if (table.ActivePlayer.GetPlayerType() == Model.PlayerType.AI)
            {
                AiMove();
            }

            StartTimer();
        }

        /** <summary>
         * Method used to draw the disks and update scoreboard after every move or pass.
         * </summary>
         */
        private void Draw()
        {
            /* Iterate backwards through all children of the grid
             * and remove all ellipses that are wider than 10.
             *
             * This is needed because otherwise this method would
             * draw new ellipses on top of the existing ones.
             *
             * The type casting and size check is in place because
             * there are other elements present that we do not want
             * to remove.
             */
            for (int i = gameField.Children.Count - 1; i >= 0; i--)
            {
                UIElement child = gameField.Children[i];
                if (child is Ellipse ellipse && ellipse.Width > 10)
                {
                    gameField.Children.RemoveAt(i);
                }
            }
            // Create the ellipses and set their properties.
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Fill = Brushes.Transparent;
                    ellipse.Width = 50;
                    ellipse.Height = 50;

                    Grid.SetRow(ellipse, row);
                    Grid.SetColumn(ellipse, col);

                    switch (table.Fields[row, col].GetColor())
                    {
                        case Model.Color.Empty:
                            ellipse.Fill = Brushes.Transparent;
                            break;
                        case Model.Color.Black:
                            ellipse.Fill = Brushes.Black;
                            break;
                        case Model.Color.White:
                            ellipse.Fill = Brushes.White;
                            ellipse.Stroke = Brushes.Black;
                            break;
                    }

                    // Check for valid moves and set the ellipse properties accordingly.
                    if (table.ValidMoves.Any(x => x.X == row && x.Y == col))
                    {
                        ellipse.Fill = Brushes.Transparent;
                        ellipse.Stroke = Brushes.Black;
                    }

                    // Set the click event handler for all ellipses, and add them to the grid.
                    ellipse.MouseDown += Ellipse_MouseDown;
                    gameField.Children.Add(ellipse);
                }
            }
            // Update scoreboard text.
            Score1 = table.Player1.Score;
            Score2 = table.Player2.Score;
            tbTurnIndicator.Text = table.ActivePlayer.Name + "'s turn";
        }

        /** <summary>
        * Interaction logic for disks on the board.
        * </summary>
        */
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            int row = Grid.GetRow(ellipse);
            int col = Grid.GetColumn(ellipse);
            bool isValid = false;
            if (table.ValidMoves.Any(x => x.X == row && x.Y == col))
            {
                isValid = true;
            }
            if (isValid && table.ActivePlayer.GetPlayerType() == Model.PlayerType.Human)
            {
                Model.Position pos = new Model.Position(row, col);
                table.MakeMove(pos);
                Draw();
                if (table.ActivePlayer.GetPlayerType() == Model.PlayerType.AI)
                {
                    AiMove();
                }
                if (table.IsGameOver())
                {
                    EndGame();
                }
            }
        }

        /** <summary>
         * Method for the AI's turn, with an async delay to simulate it "thinking".
         * </summary>
         */
        private async void AiMove()
        {
            if (table.IsGameOver())
            {
                return;
            }

            await Task.Run(async () =>
            {
                await Task.Delay(1000);
            });
            table.MakeMove(table.GetBestValidMove());

            Draw();

            if (table.IsGameOver())
            {
                EndGame();
                return;
            }
        }

        // Create and start a stopwatch for elapsed time display.
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            timer.Start();
        }

        // Update the timer TextBlock on each tick.
        private void TimerTick(object sender, EventArgs e)
        {
            elapsedTime.Text = stopWatch.Elapsed.ToString(@"mm\:ss");
        }

        // Menu button click event handler.
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        // Pass button click event handler.
        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            /* If the game isn't over and it's either a
             * player vs. player game, or the active player
             * is a Human, pass their turn, and update the window.
             */
            if (!table.IsGameOver())
            {
                if (_pvp)
                {
                    table.PassMove();
                    Draw();
                }
                else if (!_pvp && table.ActivePlayer.playerType == Model.PlayerType.Human)
                {
                    table.PassMove();
                    AiMove();
                    Draw();
                }
            }
            // Check if the game is over after a passed turn.
            if (table.IsGameOver())
            {
                EndGame();
            }
            else
            {
                return;
            }
        }

        /** <summary>
         * Method to save scores and notify player(s) that the game has ended.
         * </summary>
         */
        private void EndGame()
        {
            stopWatch.Stop();
            timer.Stop();
            tbTurnIndicator.Text = "Game finished";
            if (_score1 > _score2)
            {
                _winner = _player1;
            }
            else
            {
                _winner = _player2;
            }
            Utility.SaveScores(_player1, _player2, _score1, _score2, _winner, elapsedTime.Text);
            MessageBoxResult result = MessageBox.Show(
                _winner + " has won.\nWould you like to start a new game?\n(Pressing no will take you to the high scores)",
                "Game over!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
            {
                PlayerSelectWindow psWindow = new PlayerSelectWindow();
                psWindow.Show();
                Close();
            }
            else if (result == MessageBoxResult.No)
            {
                ScoreboardWindow scoreboardWindow = new ScoreboardWindow();
                scoreboardWindow.Show();
                Close();
            }
        }
    }
}
