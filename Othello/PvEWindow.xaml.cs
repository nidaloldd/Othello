using System.Text.RegularExpressions;
using System.Windows;

namespace Othello
{
    public partial class PvEWindow : Window
    {
        public PvEWindow() => InitializeComponent();

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Regexp pattern to filter user input for special characters and whitespaces.
            Regex regexItem = new Regex("^[a-zA-Z0-9]*$");

            /* In case the name field is empty, has whitespaces, or contains special characters,
             * warn the user and tell them to only use letters and numbers.
             */
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