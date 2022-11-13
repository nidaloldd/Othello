using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    class ModelMain
    {
        static public void AIN()
        {
            Player player1 = new Player(PlayerType.Human, "PLayer1");
            Player player2 = new Player(PlayerType.Human, "PLayer2");

            Table table = new Table(player1, player2);

            table.PrintTable();

        }
      
    }

}
