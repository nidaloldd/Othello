using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Othello.Model
{
    public static class ModelMain
    {
        public static void Main()
        {
            Player player1 = new Player(PlayerType.Human, "Player1");
            Player player2 = new Player(PlayerType.Human, "Player2");

            Player aiPlayer1 = new Player(PlayerType.AI, "AIplayer");
            Player aiPlayer2 = new Player(PlayerType.AI, "AIplayer");

            Table table = new Table(player1, player2);

            // For AI vs AI
            // table.MakeMove();

            // For testing PassMove
            /*
            Trace.WriteLine("-- 1 --");
            table.PrintTable();
            table.MakeMove(new Position(3, 2));
            table.PassMove();
            table.PassMove();
            table.PassMove();
            table.PassMove();
            table.MakeMove(new Position(2, 2));
            */

            // For Player vs Player
            Trace.WriteLine("-- 1 --");
            table.PrintTable();
            table.MakeMove(new Position(3, 2));
            table.MakeMove(new Position(2, 2));
            Trace.WriteLine("-- 2 --");
            table.MakeMove(new Position(1, 2));
            table.MakeMove(new Position(5, 3));
            Trace.WriteLine("-- 3 --");
            table.MakeMove(new Position(5, 4));
            table.MakeMove(new Position(3, 1));
            Trace.WriteLine("-- 4 --");
            table.MakeMove(new Position(3, 0));
            table.MakeMove(new Position(3, 5));
            Trace.WriteLine("-- 5 --");
            table.MakeMove(new Position(5, 2));
            table.MakeMove(new Position(2, 4));
            Trace.WriteLine("-- 6 --");
            table.MakeMove(new Position(1, 5));
            table.MakeMove(new Position(6, 2));
            Trace.WriteLine("-- 7 --");
            table.MakeMove(new Position(3, 6));
            table.MakeMove(new Position(5, 5));
            Trace.WriteLine("-- 8 --");
            table.MakeMove(new Position(7, 1));
            table.MakeMove(new Position(2, 1));
            Trace.WriteLine("-- 9 --");
            table.MakeMove(new Position(5, 6));
            table.MakeMove(new Position(0, 2));
            Trace.WriteLine("-- 10 --");
            table.MakeMove(new Position(1, 0));
            table.MakeMove(new Position(4, 2));
            Trace.WriteLine("-- 11 --");
            table.MakeMove(new Position(0, 3));
            table.MakeMove(new Position(4, 0));
            Trace.WriteLine("-- 12 --");
            table.MakeMove(new Position(4, 1));
            table.MakeMove(new Position(5, 0));
            Trace.WriteLine("-- 13 --");
            table.MakeMove(new Position(6, 0));
            table.MakeMove(new Position(1, 1));
            Trace.WriteLine("-- 14 --");
            table.MakeMove(new Position(0, 0));
            table.MakeMove(new Position(0, 4));
            Trace.WriteLine("-- 15 --");
            table.MakeMove(new Position(2, 3));
            table.MakeMove(new Position(5, 1));
            Trace.WriteLine("-- 16 --");
            table.MakeMove(new Position(6, 1));
            table.MakeMove(new Position(2, 6));
            Trace.WriteLine("-- 17 --");
            table.MakeMove(new Position(1, 7));
            table.MakeMove(new Position(7, 2));
            Trace.WriteLine("-- 18 --");
            table.MakeMove(new Position(7, 3));
            table.MakeMove(new Position(6, 6));
            Trace.WriteLine("-- 19 --");
            table.MakeMove(new Position(7, 7));
            table.MakeMove(new Position(1, 4));
            Trace.WriteLine("-- 20 --");
            table.MakeMove(new Position(0, 5));
            table.MakeMove(new Position(1, 3));
            Trace.WriteLine("-- 21 --");
            table.MakeMove(new Position(1, 6));
            table.MakeMove(new Position(4, 5));
            Trace.WriteLine("-- 22 --");
            table.MakeMove(new Position(4, 6));
            table.MakeMove(new Position(0, 6));
            Trace.WriteLine("-- 23 --");
            table.MakeMove(new Position(6, 3));
            table.MakeMove(new Position(5, 7));
            Trace.WriteLine("-- 24 --");
            table.MakeMove(new Position(3, 7));
            table.MakeMove(new Position(7, 6));
            Trace.WriteLine("-- 25 --");
            table.MakeMove(new Position(7, 5));
            table.MakeMove(new Position(6, 5));
            Trace.WriteLine("-- 26 --");
            table.MakeMove(new Position(6, 7));
            table.MakeMove(new Position(6, 4));
            Trace.WriteLine("-- 27 --");
            table.MakeMove(new Position(7, 4));
            table.MakeMove(new Position(2, 5));
            Trace.WriteLine("-- 28 --");
            table.MakeMove(new Position(0, 7));
            table.MakeMove(new Position(2, 0));
            Trace.WriteLine("-- 29 --");
            table.MakeMove(new Position(0, 1));
            table.MakeMove(new Position(7, 0));
            Trace.WriteLine("-- 30 --");
            table.MakeMove(new Position(2, 7));
            table.MakeMove(new Position(4, 7));
            Trace.WriteLine("-- 31 --");

            Trace.WriteLine("Player1 Score: " + player1.GetScore());
            Trace.WriteLine("Player2 Score: " + player2.GetScore());
        }
    }
}
