using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Othello.Model
{
    public class Table
    {
        private Player _player1;
        private Player _player2;
        private Player _activePlayer;
        public Player Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }
        public Player Player2
        {
            get { return _player2; }
            set { _player1 = value; }
        }
        public Player ActivePlayer
        {
            get { return _activePlayer; }
            set { _activePlayer = value; }
        }

        private const int Width = 8;
        private const int Height = 8;
        private const int PointValue = 10;

        public readonly Field[,] Fields = new Field[8, 8];
        public List<Position> ValidMoves = new List<Position>();

        /*
         *  returns the right Field based on the given Position
         */
        public Field GetFieldOn(Position position)
        {
            return Fields[position.X, position.Y];
        }

        /*
         *  returns if the given position is a valid position
         */
        public bool IsPositionValid(Position position)
        {
            if (position.X > -1 && position.X < Width &&
                position.Y > -1 && position.Y < Height)
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
                    if (Fields[i, j].GetColor() == player.GetColor())
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
            Position position = new Position(startPos.X, startPos.Y);

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
            // return if the given position is empty
            if (GetFieldOn(pos).GetColor() == Color.Empty)
            {
                return;
            }

            Position startPosition = new Position(pos.X, pos.Y);

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
            Position startPos = new Position(pos.X, pos.Y);

            // set the Empty Color to the activePlayers color
            GetFieldOn(startPos).SetColor(_activePlayer.GetColor());

            foreach (Position dir in Position.Directions)
            {
                // set the position to the starting values
                pos = new Position(startPos.X, startPos.Y);
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

        // returns the bonus multiplier depend on which Angle changed by the move of the player
        private int GetAngleBonus(List<Position> directions)
        {
            int bonus = 0;
            Trace.WriteLine("UP " + new Position().Up().Write());
            foreach (Position p in directions)
            {
                Trace.WriteLine(p.Write());
            }
            // Bonus for Vertical Angle
            if (directions.Any(x => x == new Position().Up()) || directions.Any(x => x == new Position().Down()))
            {
                bonus++;
            }
            // Bonus for Horizontal Angle
            if (directions.Any(x => x == new Position().Left()) || directions.Any(x => x == new Position().Right()))
            {
                bonus++;
            }
            // Bonus for Diagonal Angle
            if (directions.Any(x => x == new Position().DiagonalUpLeft()) || directions.Any(x => x == new Position().DiagonalUpRight()) ||
                directions.Any(x => x == new Position().DiagonalDownLeft()) || directions.Any(x => x == new Position().DiagonalDownRight()))
            {
                bonus++;
            }

            return bonus;
        }

        // returns a list of directions that changed by the move of the player
        private List<Position> GetAngleDirections(Position pos)
        {
            List<Position> angleDirections = new List<Position>();
            Position startPos = new Position(pos.X, pos.Y);

            // set the Empty Color to the activPlayers color
            GetFieldOn(startPos).SetColor(_activePlayer.GetColor());
            foreach (Position dir in Position.Directions)
            {
                // set the position to the starting values
                int count = 0;
                pos = new Position(startPos.X, startPos.Y);
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

        // returns a random a Starting PLayer
        public Player GetStartingPlayer()
        {
            Random rnd = new Random();
            double choice = rnd.NextDouble();
            if (choice <= 0.5)
            {
                return _player1;
            }
            else
            {
                return _player2;
            }
        }

        // Constructor For the Table
        public Table(Player pl1, Player pl2)
        {
            _player1 = pl1;
            _player2 = pl2;
            _player1.SetColor(Color.Black);
            _player2.SetColor(Color.White);

            // set up which Player will start the game.
            // who makes the first move
            _activePlayer = GetStartingPlayer();

            // Set up the fields of the Table
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Fields[j, i] = new Field(new Position(j, i), Color.Empty);
                }
            }
            Fields[3, 3].SetColor(Color.White);
            Fields[4, 4].SetColor(Color.White);
            Fields[3, 4].SetColor(Color.Black);
            Fields[4, 3].SetColor(Color.Black);
            GetValidMoves(_activePlayer);
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

        // The activ Player make the move to the given position
        public void MakeMove(Position position)
        {
            if (IsGameOver())
            {
                return;
            }
            Trace.WriteLine(_activePlayer.GetColor() + "'s Turn");

            // throw an Error message if the given position is not a valid Move
            if (!InvalidMoves(position))
            {
                Trace.WriteLine("InvalidMove From " + _activePlayer.GetColor() + " " + position.Write());
                return;
            }

            // set the Empty Color to the activPlayers color
            GetFieldOn(position).SetColor(_activePlayer.GetColor());

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
            GetFieldOn(position).SetColor(_activePlayer.GetColor());

            CalculateScore(_activePlayer, countRev, bonus);

            SwitchActivePlayer();
            GetValidMoves(_activePlayer);

            PrintTable();
        }

        // return the count of the figures of the given color
        private int CountColorOnTable(Color color)
        {
            int count = 0;
            foreach (Field f in Fields)
            {
                if (f.GetColor() == color)
                {
                    count++;
                }
            }
            return count;
        }
        // return if there is at least one figure of the given color
        private bool IsColorOnTableExist(Color color)
        {
            foreach (Field f in Fields)
            {
                if (f.GetColor() == color)
                {
                    return true;
                }
            }
            return false;
        }

        // the activ player dont make any move
        public void PassMove()
        {
            SwitchActivePlayer();
            GetValidMoves(_activePlayer);

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

        // Calculate and add the earned scores to the given Player
        private void CalculateScore(Player player, int numberOfRevers, int bonusForAngle)
        {
            Trace.WriteLine("CountingRev: " + numberOfRevers);
            Trace.WriteLine("BonusAngle" + bonusForAngle);
            int score = 0;
            for (int n = 1; n <= numberOfRevers; n++)
            {
                score += n * PointValue;
            }
            score *= bonusForAngle;

            if (!IsColorOnTableExist(player.GetEnemyColor()))
            {
                score += 100 * PointValue * CountColorOnTable(player.GetColor());
            }

            player.AddScore(score);
        }

        // returns if there is no valid move for the activ player
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

        // returns the Position of move that will revers the most of the enemy figure
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

        // switch the active Player
        private void SwitchActivePlayer()
        {
            if (_activePlayer == _player1)
            {
                _activePlayer = _player2;
            }
            else if (_activePlayer == _player2)
            {
                _activePlayer = _player1;
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
                    switch (Fields[i, j].GetColor())
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
                symbols[pos.X, pos.Y] = " V ";
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
