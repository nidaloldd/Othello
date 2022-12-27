using System.Text.RegularExpressions;
using System.Windows;

namespace Othello
{
    public partial class PvPWindow : Window
    {

        public PvPWindow() => InitializeComponent();
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (string.IsNullOrWhiteSpace(playerOneName.Text) || !regexItem.IsMatch(playerOneName.Text))
            {
                _ = MessageBox.Show("Player 1's name is not valid!");
                playerOneName.Text = "";
            }
            else if (string.IsNullOrWhiteSpace(playerTwoName.Text) || !regexItem.IsMatch(playerTwoName.Text))
            {
                _ = MessageBox.Show("Player 2's name is not valid!");
                playerOneName.Text = "";
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
        private void QuitGame_Click(object sender, RoutedEventArgs e) => Close();
    }
}