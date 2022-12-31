using System.Linq;
using System.Windows;

namespace Othello
{
    public partial class ScoreboardWindow : Window
    {
        public ScoreboardWindow()
        {
            InitializeComponent();

            // Read data from the JSON, then put it in a descending order based on scores.
            var sortedData = Utility.ReadScores().OrderByDescending(d => System.Math.Max(d.Score1, d.Score2)).ToList();

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
            // Ask the user if they want to clear all high scores.
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

        private void QuitGame_Click(object sender, RoutedEventArgs e) => Close();
    }
}