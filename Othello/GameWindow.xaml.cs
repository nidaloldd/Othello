using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Othello
{
    public partial class GameWindow : Window
    {
        private int _score1, _score2;
        public string _player1, _player2;
        private bool _pvp;
        private DispatcherTimer timer;
        private Stopwatch stopWatch;

        public GameWindow(string player1, string player2, bool pvp)
        {
            InitializeComponent();
            this._player1 = player1;
            this._player2 = player2;
            this._pvp = pvp;
            Model.ModelMain.main();
            StartTimer();
        }
        public void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromSeconds(1);
            stopWatch = new Stopwatch();
            stopWatch.Start();
            timer.Start();
        }
        void TimerTick(object sender, EventArgs e)
        {
            elapsedTime.Text = stopWatch.Elapsed.ToString(@"mm\:ss");
        }

        private void TbScore1_Loaded(object sender, RoutedEventArgs e) { ((TextBlock)sender).Text = "0"; }

        private void TbScore2_Loaded(object sender, RoutedEventArgs e) { ((TextBlock)sender).Text = "0"; }

        private void TbTurnIndicator_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBlock)sender).Text = _player1 + "'s turn";
        }

        private void WhiteDisks_Loaded(object sender, RoutedEventArgs e)
        {
            ((Ellipse)sender).Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFF");
            ((Ellipse)sender).Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#000");
        }

        private void BlackDisks_Loaded(object sender, RoutedEventArgs e)
        {
            ((Ellipse)sender).Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#000");
        }

        private void ClearDisks_Loaded(object sender, RoutedEventArgs e)
        {
            ((Ellipse)sender).Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#0000");
            ((Ellipse)sender).Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString("#000");
        }

        private void Timer_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBlock)sender).Text = "00:00";
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            //TBA
        }

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

    }
}
