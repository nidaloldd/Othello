using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Othello.Model
{
    public class Table
    {
        public Player player1, player2;
        public Player activePlayer;

        private const int Width = 8;
        private const int Height = 8;
        private const int PointValue = 10;

        public readonly Field[,] fields = new Field[8, 8];
        public List<Position> ValidMoves = new List<Position>();

        /*
         *  returns the right Field based on the given Position
         */
        public Field GetFieldOn(Position position)
        {
            return fields[position.GetX(), position.GetY()];
        }

        /*
         *  returns if the given position is a valid position
         */
        public bool IsPositionValid(Position position)
        {
            if (position.GetX() > -1 && position.GetX() < Width &&
                position.GetY() > -1 && position.GetY() < Height)
            {
                return true;
            }
            return false;
        }

        /*
         *  fill up the ValidMoves array with the actual Valid moves
         *  of the given Player
         */
        public void GetValidMoves(Player player)
        {
            ValidMoves = new List<Position>();

            /*
             * Find the figures with the same Color as the Player
             * and get the possible valid moves from them
             */
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (fields[i, j].GetColor() == player.GetColor())
                    {
                        GetValidMovesFrom(new Position(i, j));
                    }
                }
            }
        }

        /*
         *  Add a valid move to the ValidMoves array based on the given
         *  Position and a Direction
         */
        private void GetValidMovesFrom(Position startPos, Position direction)
        {
            Position position = new Position(startPos.GetX(), startPos.GetY());

            // returns if the next step to a direction is invalid
            if (!IsPositionValid(position + direction))
            {
                return;
            }

            // returns if the next step Color is the same as the starting color
            if (GetFieldOn(position + direction).GetColor() != GetFieldOn(startPos).GetReverseColor())
            {
                return;
            }

            while (true)
            {
                // make a step
                position += direction;
                if (!IsPositionValid(position))
                {
                    break;
                }

                // Returns if the next step has a the same color as the starting color
                if ((GetFieldOn(position).GetColor() == GetFieldOn(startPos).GetColor()))
                {
                    break;
                }
                // If the next step is Empty add a valid position
                else if (GetFieldOn(position).GetColor() == Color.Empty)
                {
                    ValidMoves.Add(position);
                    break;
                }
            }
        }

        /*
         * Get the possible valid moves from a Position
         */
        private void GetValidMovesFrom(Position pos)
        {
            if (GetFieldOn(pos).GetColor() == Color.Empty)
            {
                return;
            }

            Position startPosition = new Position(pos.GetX(), pos.GetY());

            // call this method for all directions
            foreach (Position dir in Position.Directions)
            {
                GetValidMovesFrom(startPosition, dir);
            }
        }

        /*
         * returns the position that needs to reverse its Color
         */
        private List<Position> GetReversedPositions(Position pos)
        {
            List<Position> positions = new List<Position>();
            List<Position> posForOneDir = new List<Position>();
            Position startPos = new Position(pos.GetX(), pos.GetY());

            // set the Empty Color to the activPlayers color
            GetFieldOn(startPos).SetColor(activePlayer.GetColor());

            foreach (Position dir in Position.Directions)
            {
                // set the position to the starting values
                pos = new Position(startPos.GetX(), startPos.GetY());
                // initialize a List to store the right Positions of one direction
                posForOneDir = new List<Position>();
                while (true)
                {
                    // step to a direction
                    pos += dir;

                    // break if the next position is invalid
                    if (!IsPositionValid(pos))
                    {
                        break;
                    }
                    // break if the next position is Empty
                    if (GetFieldOn(pos).GetColor() == Color.Empty)
                    {
                        break;
                    }

                    // if the starting color is the same the next position
                    // that means that this is a valid direction
                    if (GetFieldOn(pos).GetColor() == GetFieldOn(startPos).GetColor())
                    {
                        // so add them to the List Which will return
                        foreach (Position p in posForOneDir)
                        {
                            positions.Add(p);
                        }
                        break;
                    }
                    // if the next step has the opposite Color as the starting position
                    else
                    {
                        // these positions needs to be reversed
                        posForOneDir.Add(pos);
                    }
                }
            }

            // set the Empty Color to the activPlayers color
            GetFieldOn(startPos).SetColor(Color.Empty);

            return positions;
        }

        private int GetAngleBonus(List<Position> directions) {
            int Bonus = 0;
            Trace.WriteLine("UP " + new Position().Up().Write());
            foreach (Position p in directions) {
                Trace.WriteLine(p.Write());
            }
            // Bonus for Vertical Angle
            if (directions.Any(x => x == new Position().Up()) || directions.Any(x => x == new Position().Down())){
                Bonus++;
            }
            // Bonus for Horizontal Angle
            if (directions.Any(x => x == new Position().Left()) || directions.Any(x => x == new Position().Right()))
            {
                
                Bonus++;
            }
            // Bonus for Diagonal Angle
            if (directions.Any(x => x == new Position().DiagonalUpLeft()) || directions.Any(x => x == new Position().DiagonalUpRight()) ||
                directions.Any(x => x == new Position().DiagonalDownLeft()) || directions.Any(x => x == new Position().DiagonalDownRight()))
            {
                Bonus++;
            }

            return Bonus;
        }
        private List<Position> GetAngleDirections(Position pos)
        {
            List<Position> angleDirections = new List<Position>();
            Position startPos = new Position(pos.GetX(), pos.GetY());

            // set the Empty Color to the activPlayers color
            GetFieldOn(startPos).SetColor(activePlayer.GetColor());
            foreach (Position dir in Position.Directions)
            {
                // set the position to the starting values
                int count = 0;
                pos = new Position(startPos.GetX(), startPos.GetY());
                while (true)
                {
                    // step to a direction
                    pos += dir;

                    // break if the next position is invalid
                    if (!IsPositionValid(pos))
                    {
                        break;
                    }

                    // break if the next position is Empty
                    if (GetFieldOn(pos).GetColor() == Color.Empty)
                    {
                        break;
                    }

                    if (GetFieldOn(pos).GetColor() == GetFieldOn(startPos).GetColor())
                    {
                        if (count > 0)
                        {
                            angleDirections.Add(dir);
                        }

                        break;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            // set the Empty Color to the activPlayers color
            GetFieldOn(startPos).SetColor(Color.Empty);
            return angleDirections;
        }

        public Player GetStartingPlayer()
        {
            Random rnd = new Random();
            double choice = rnd.NextDouble();
            if (choice <= 0.5)
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        // Constructor For the Table
        public Table(Player pl1, Player pl2)
        {
            this.player1 = pl1;
            this.player2 = pl2;
            player1.SetColor(Color.Black);
            player2.SetColor(Color.White);

            // set up which Player will start the game.
            // who makes the first move
            activePlayer = GetStartingPlayer();

            // Set up the fields of the Table
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    fields[j, i] = new Field(new Position(j, i), Color.Empty);
                }
            }
            fields[3, 3].SetColor(Color.White);
            fields[4, 4].SetColor(Color.White);
            fields[3, 4].SetColor(Color.Black);
            fields[4, 3].SetColor(Color.Black);
            GetValidMoves(activePlayer);
        }

        /*
         * returns if the move to the given Position is valid
         */
        private bool InvalidMoves(Position position)
        {
            foreach (Position p in ValidMoves)
            {
                if (p == position)
                {
                    return true;
                }
            }
            return false;
        }

        public void MakeMove(Position position)
        {
            if (IsGameOver())
            {
                return;
            }
            Trace.WriteLine(activePlayer.GetColor() + "'s Turn");

            // throw an Error message if the given position is not a valid Move
            if (!InvalidMoves(position))
            {
                Trace.WriteLine("InvalidMove From " + activePlayer.GetColor() + " " + position.Write());
                return;
            }

            // set the Empty Color to the activPlayers color
            GetFieldOn(position).SetColor(activePlayer.GetColor());

            // Variable for counting reverses
            int countRev = 0;

            int bonus = GetAngleBonus(GetAngleDirections(position));

            // Reverse the Colors
            foreach (Position p in GetReversedPositions(position))
            {
                GetFieldOn(p).ReverseColor();
                countRev++;
            }

            // set the Empty Color to the activPlayers color
            GetFieldOn(position).SetColor(activePlayer.GetColor());

            CalculateScore(activePlayer, countRev, bonus);

            SwitchPlayer();
            GetValidMoves(activePlayer);

            PrintTable();
        }
        public void MakeMove()
        {
            MakeMove(GetBestValidMove());
        }

        private int CountColorOnTable(Color color)
        {
            int count = 0;
            foreach (Field f in fields)
            {
                if (f.GetColor() == color)
                {
                    count++;
                }
            }
            return count;
        }
        private bool IsColorOnTableExist(Color color)
        {
            foreach (Field f in fields)
            {
                if (f.GetColor() == color)
                {
                    return true;
                }
            }
            return false;
        }

        public void PassMove()
        {
            SwitchPlayer();
            GetValidMoves(activePlayer);

            PrintTable();
            /*if (activePlayer.GetPlayerType() == PlayerType.AI)
            {
                if (IsGameOver())
                {
                    return;
                }
                MakeMove();
            }*/
        }

        private void CalculateScore(Player player, int n, int bonusForAngle)
        {
            Trace.WriteLine("CountingRev: " + n);
            Trace.WriteLine("BonusAngle" + bonusForAngle);
            int score = 0;
            for (int i = 1; i <= n; i++)
            {
                score += i * PointValue;
            }
            score *= bonusForAngle;

            if (!IsColorOnTableExist(player.GetEnemyColor()))
            {
                score += 100 * PointValue * CountColorOnTable(player.GetColor());
            }

            player.AddScore(score);
        }

        public bool IsGameOver()
        {
            // Game Over
            if (ValidMoves.Count() == 0)
            {
                Trace.WriteLine("GameOver");
                return true;
            }
            return false;
        }

        public Position GetBestValidMove()
        {
            Dictionary<Position, int> moves = new Dictionary<Position, int>();

            foreach (Position p in ValidMoves)
            {
                moves.Add(p, GetReversedPositions(p).Count());
            }

            int max = moves.Values.Max();

            var sol = from move in moves
                      where move.Value == max
                      select move.Key;

            int randomMovePositioNumber = 0;
            if (sol.Count() > 1)
            {
                Random random = new Random();
                randomMovePositioNumber = random.Next(sol.Count());
            }
            Trace.WriteLine("maxValue :" + max);
            Trace.WriteLine("numberOFMaxValue :" + sol.Count());

            return sol.ElementAt(randomMovePositioNumber);
        }

        private void SwitchPlayer()
        {
            if (activePlayer == player1)
            {
                activePlayer = player2;
            }
            else if (activePlayer == player2)
            {
                activePlayer = player1;
            }
        }

        /*
         * Print The Table to the Console
         */
        public void PrintTable()
        {
            string[,] symbols = new string[8, 8];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    switch (fields[i, j].GetColor())
                    {
                        case Color.Black:
                            symbols[i, j] = " B ";
                            break;
                        case Color.White:
                            symbols[i, j] = " W ";
                            break;
                        case Color.Empty:
                            symbols[i, j] = " O ";
                            break;
                    }
                }
            }
            foreach (Position pos in ValidMoves)
            {
                symbols[pos.GetX(), pos.GetY()] = " V ";
            }
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Trace.Write(symbols[i, j]);
                }
                Trace.WriteLine(string.Empty);
            }
            Trace.WriteLine("--------------------------");
        }
    }
}
