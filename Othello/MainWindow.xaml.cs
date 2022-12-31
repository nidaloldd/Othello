using System.Windows;

namespace Othello
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void PlayerSelect_Click(object sender, RoutedEventArgs e)
        {
            PlayerSelectWindow psWindow = new PlayerSelectWindow();
            psWindow.Show();
            Close();
        }

        private void OpenScores_Click(object sender, RoutedEventArgs e)
        {
            ScoreboardWindow scoreboardWindow = new ScoreboardWindow();
            scoreboardWindow.Show();
            Close();
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e) => Close();
    }
}