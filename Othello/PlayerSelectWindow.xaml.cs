using System.Windows;

namespace Othello
{
    public partial class PlayerSelectWindow : Window
    {
        public PlayerSelectWindow()
        {
            InitializeComponent();
        }
        private void PveSelect_Click(object sender, RoutedEventArgs e)
        {
            PvEWindow pve = new PvEWindow();
            pve.Show();
            Close();
        }
        private void PvpSelect_Click(object sender, RoutedEventArgs e)
        {
            PvPWindow pvp = new PvPWindow();
            pvp.Show();
            Close();
        }
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}