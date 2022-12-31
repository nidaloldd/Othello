using System.Text.RegularExpressions;
using System.Windows;

namespace Othello
{
    public partial class PvPWindow : Window
    {
        public PvPWindow() => InitializeComponent();

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Regexp pattern to filter user input for special characters and whitespaces.
            Regex regexItem = new Regex("^[a-zA-Z0-9]*$");

            /* In case the name fields are empty, have whitespaces, or contain special characters,
             * warn the users and tell them to only use letters and numbers.
             */
            if (string.IsNullOrWhiteSpace(playerOneName.Text) || !regexItem.IsMatch(playerOneName.Text))
            {
                _ = MessageBox.Show("Player 1's name is not valid!\nNames can only contain letters and numbers.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (string.IsNullOrWhiteSpace(playerTwoName.Text) || !regexItem.IsMatch(playerTwoName.Text))
            {
                _ = MessageBox.Show("Player 2's name is not valid!\nNames can only contain letters and numbers.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                GameWindow gameWindow = new GameWindow(playerOneName.Text, playerTwoName.Text, true);
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