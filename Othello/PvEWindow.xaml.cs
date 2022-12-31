using System.Text.RegularExpressions;
using System.Windows;

namespace Othello
{
    public partial class PvEWindow : Window
    {
        public PvEWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9]*$");

            if (string.IsNullOrWhiteSpace(playerOneName.Text) || !regexItem.IsMatch(playerOneName.Text))
            {
                _ = MessageBox.Show("Player's name is not valid!\nNames can only contain letters and numbers.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                GameWindow gameWindow = new GameWindow(playerOneName.Text, "Computer", false);
                gameWindow.Show();
                Close();
            }
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            PlayerSelectWindow psWindow = new PlayerSelectWindow();
            psWindow.Show();
            Close();
        }
    }
}