using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Othello.Model
{
    class ModelMain
    {
        static public void main()
        {
            Player player1 = new Player(PlayerType.Human, "Player1");
            Player player2 = new Player(PlayerType.Human, "Player2");

            Player AIplayer = new Player(PlayerType.AI, "AIplayer");
            Player AIplayer2 = new Player(PlayerType.AI, "AIplayer");

            Table table = new Table(player1, player2);

            // For AI vs AI
            // table.makeMove();

            // For testing PassMove
            /*
            Trace.WriteLine("-- 1 --");
            table.PrintTable();
            table.makeMove(new Position(3, 2));
            table.PassMove();
            table.PassMove();
            table.PassMove();
            table.PassMove();
            table.makeMove(new Position(2, 2));
            */

            // For PLayer vs PLayer
            
            Trace.WriteLine("-- 1 --");
            table.PrintTable();
            table.makeMove(new Position(3, 2)); table.makeMove(new Position(2, 2));
            Trace.WriteLine("-- 2 --");
            table.makeMove(new Position(1, 2)); table.makeMove(new Position(5, 3));
            Trace.WriteLine("-- 3 --");
            table.makeMove(new Position(5, 4)); table.makeMove(new Position(3, 1));
            Trace.WriteLine("-- 4 --");
            table.makeMove(new Position(3, 0)); table.makeMove(new Position(3, 5));
            Trace.WriteLine("-- 5 --");
            table.makeMove(new Position(5, 2)); table.makeMove(new Position(2, 4));
            Trace.WriteLine("-- 6 --");
            table.makeMove(new Position(1, 5)); table.makeMove(new Position(6, 2));
            Trace.WriteLine("-- 7 --");
            table.makeMove(new Position(3, 6)); table.makeMove(new Position(5, 5));
            Trace.WriteLine("-- 8 --");
            table.makeMove(new Position(7, 1)); table.makeMove(new Position(2, 1));
            Trace.WriteLine("-- 9 --");
            table.makeMove(new Position(5, 6)); table.makeMove(new Position(0, 2));
            Trace.WriteLine("-- 10 --");
            table.makeMove(new Position(1, 0)); table.makeMove(new Position(4, 2));
            Trace.WriteLine("-- 11 --");
            table.makeMove(new Position(0, 3)); table.makeMove(new Position(4, 0));
            Trace.WriteLine("-- 12 --");
            table.makeMove(new Position(4, 1)); table.makeMove(new Position(5, 0));
            Trace.WriteLine("-- 13 --");
            table.makeMove(new Position(6, 0)); table.makeMove(new Position(1, 1));
            Trace.WriteLine("-- 14 --");
            table.makeMove(new Position(0, 0)); table.makeMove(new Position(0, 4));
            Trace.WriteLine("-- 15 --");
            table.makeMove(new Position(2, 3)); table.makeMove(new Position(5, 1));
            Trace.WriteLine("-- 16 --");
            table.makeMove(new Position(6, 1)); table.makeMove(new Position(2, 6));
            Trace.WriteLine("-- 17 --");
            table.makeMove(new Position(1, 7)); table.makeMove(new Position(7, 2));
            Trace.WriteLine("-- 18 --");
            table.makeMove(new Position(7, 3)); table.makeMove(new Position(6, 6));
            Trace.WriteLine("-- 19 --");
            table.makeMove(new Position(7, 7)); table.makeMove(new Position(1, 4));
            Trace.WriteLine("-- 20 --");
            table.makeMove(new Position(0, 5)); table.makeMove(new Position(1, 3));
            Trace.WriteLine("-- 21 --");
            table.makeMove(new Position(1, 6)); table.makeMove(new Position(4, 5));
            Trace.WriteLine("-- 22 --");
            table.makeMove(new Position(4, 6)); table.makeMove(new Position(0, 6));
            Trace.WriteLine("-- 23 --");
            table.makeMove(new Position(6, 3)); table.makeMove(new Position(5, 7));
            Trace.WriteLine("-- 24 --");
            table.makeMove(new Position(3, 7)); table.makeMove(new Position(7, 6));
            Trace.WriteLine("-- 25 --");
            table.makeMove(new Position(7, 5)); table.makeMove(new Position(6, 5));
            Trace.WriteLine("-- 26 --");
            table.makeMove(new Position(6, 7)); table.makeMove(new Position(6, 4));
            Trace.WriteLine("-- 27 --");
            table.makeMove(new Position(7, 4)); table.makeMove(new Position(2, 5));
            Trace.WriteLine("-- 28 --");
            table.makeMove(new Position(0, 7)); table.makeMove(new Position(2, 0));
            Trace.WriteLine("-- 29 --");
            table.makeMove(new Position(0, 1)); table.makeMove(new Position(7, 0));
            Trace.WriteLine("-- 30 --");
            table.makeMove(new Position(2, 7)); table.makeMove(new Position(4, 7));
            Trace.WriteLine("-- 31 --");

            Trace.WriteLine("Player1 Score: " + player1.getScore());
            Trace.WriteLine("Player2 Score: " + player2.getScore());
            
        }


    }
            

}
