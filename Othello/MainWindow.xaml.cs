using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Othello
{
    public partial class MainWindow : Window
    {
        private int _score1;
        private int _score2;
        public MainWindow()
        {
            InitializeComponent();

            Model.ModelMain.main();
           
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
