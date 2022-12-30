using System.Linq;
using System.Windows;

namespace Othello
{
    public partial class ScoreboardWindow : Window
    {
        public ScoreboardWindow()
        {
            InitializeComponent();
            var sortedData = Utility.ReadScores().OrderByDescending(d => d.Score1).ThenByDescending(d => d.Score2).ToList();
            highScores.ItemsSource = sortedData;
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void ClearScores_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear all scores?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Utility.ClearScores();
                highScores.ItemsSource = Utility.ReadScores();
            }
            else if (result == MessageBoxResult.No)
            {
                return;
            }
        }
        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}