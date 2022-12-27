using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Othello
{
    public partial class MainWindow : Window
    {
        private int _score1, _score2 = 0;
        public string _player1, _player2 = "";

        public MainWindow()
        {
            InitializeComponent();
            Model.ModelMain.main();
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
