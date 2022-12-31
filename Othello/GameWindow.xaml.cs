using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading.Tasks;

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
        private bool isValidMove = false;

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

            Draw();
            if (table.activePlayer.playerType == Model.PlayerType.AI)
            {
                AiMove();
            }
            //AsyncDraw();
            StartTimer();
        }
        private async void AsyncDraw()
        {
            while (true)
            {
                if (table.IsGameOver())
                {
                    break;
                }
                await Task.Delay(1000);
                Draw();
            }
        }
        private void Draw()
        {
            for (int i = gameField.Children.Count - 1; i >= 0; i--)
            {
                UIElement child = gameField.Children[i];
                if (child is Ellipse ellipse && ellipse.Width>10)
                {
                    gameField.Children.RemoveAt(i);
                }
            }
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

                    switch (table.fields[row, col].GetColor())
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

                    isValidMove = false;
                    foreach (Model.Position p in table.ValidMoves)
                    {
                        if (p.GetX() == row && p.GetY() == col)
                        {
                            isValidMove = true;
                            break;
                        }
                    }
                    if (isValidMove)
                    {
                        ellipse.Fill = Brushes.Transparent;
                        ellipse.Stroke = Brushes.Black;
                    }

                    ellipse.MouseDown += Ellipse_MouseDown;

                    gameField.Children.Add(ellipse);
                }
            }
            Score1 = table.player1.GetScore();
            Score2 = table.player2.GetScore();
            tbTurnIndicator.Text = table.activePlayer.name + "'s turn";
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            int row = Grid.GetRow(ellipse);
            int col = Grid.GetColumn(ellipse);
            bool isValid = false;
            foreach (Model.Position p in table.ValidMoves)
            {
                if (p.GetX() == row && p.GetY() == col)
                {
                    isValid = true;
                    break;
                }
            }
            if (isValid && table.activePlayer.GetPlayerType() == Model.PlayerType.Human)
            {
                Model.Position pos = new Model.Position(row, col);
                table.MakeMove(pos);
                Draw();
                if (table.activePlayer.GetPlayerType() == Model.PlayerType.AI)
                {
                    AiMove();
                }
                if (table.IsGameOver())
                {
                    EndGame();
                }
            }
        }

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

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            elapsedTime.Text = stopWatch.Elapsed.ToString(@"mm\:ss");
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            if (!table.IsGameOver())
            {
                if (_pvp)
                {
                    table.PassMove();
                    Draw();
                }
                else if (!_pvp && table.activePlayer.playerType == Model.PlayerType.Human)
                {
                    table.PassMove();
                    AiMove();
                    Draw();
                }
            }
            else return;
        }

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
            MessageBoxResult result = MessageBox.Show(_winner + " has won.\nWould you like to start a new game?\n(Pressing no will take you to the high scores)",
                "Game over!", MessageBoxButton.YesNo, MessageBoxImage.Information);
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
