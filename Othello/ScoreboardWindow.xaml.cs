using System.Windows;

namespace Othello
{
    public partial class ScoreboardWindow : Window
    {
        public ScoreboardWindow() => InitializeComponent();

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void QuitGame_Click(object sender, RoutedEventArgs e) => Close();
    }
}