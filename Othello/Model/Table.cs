using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace Othello.Model
{
    class Table
    {
        Player player1, player2;
        Player activePlayer;
        private const int width = 8;
        private const int height = 8;
        private const int PointValue = 10;

        private Field[,] fields = new Field[8, 8];
        public List<Position> ValidMoves = new List<Position>();

        /**
         *  returns the right Field based on the given Position  
         */
        public Field getFieldOn(Position position)
        {
            return fields[position.getX(), position.getY()];
        }

        /**
         *  returns if the given position is a valid position 
         */
        public bool isPositionValid(Position position)
        {
            if (position.getX() > -1 && position.getX() < width &&
                position.getY() > -1 && position.getY() < height)
            {
                return true;
            }
            return false;
        }

        /**
         *  fill up the ValidMoves array with the actual Valid moves
         *  of the given Player
         */
        public void getValidMoves(Player player)
        {
            ValidMoves = new List<Position>();

            /**
             * 
             * Find the figures with the same Color as The Player
             * and get the possible valid moves from them
             */
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (fields[i, j].GetColor() == player.getColor())
                    {

                        getValiddMovesFrom(new Position(i, j));

                    }
                }
            }
        }

        /**
         * 
         *  Add a valid move to the ValidMoves array based on the given
         *  Position and a Direction
         */
        private void getValidMovesFrom(Position startPos, Position direction)
        {
            Position position = new Position(startPos.getX(), startPos.getY());

            // returns if the next step to a direction is invalid
            if (!isPositionValid(position + direction)) { return; }

            // returns if the next step Color is the same as the starting color
            if (getFieldOn(position + direction).GetColor() != getFieldOn(startPos).GetReverseColor())
            { return; }

            while (true)
            {
                // make a step
                position += direction;
                if (!isPositionValid(position)) { break; }

                // Returns if the next step has a the same color as the starting color
                if ((getFieldOn(position).GetColor() == getFieldOn(startPos).GetColor()))
                { break; }
                // If the next step is Empty add a valid position  
                else if (getFieldOn(position).GetColor() == Color.Empty)
                {
                    ValidMoves.Add(position);
                    break;
                }
            }
        }

        /**
         * 
         * Get the possible valid moves from a Position
         */
        private void getValiddMovesFrom(Position pos)
        {

            if (getFieldOn(pos).GetColor() == Color.Empty) { return; }
            Position startPosition = new Position(pos.getX(), pos.getY());

            // call this method for all the Directions
            foreach (Position dir in Position.Directions)
            {
                getValidMovesFrom(startPosition, dir);
            }
        }


        /**
         * returns the position that needs to reverse its Color 
         */
        private List<Position> getReversedPositions(Position pos)
        {
            List<Position> positions = new List<Position>();
            List<Position> posforOneDir = new List<Position>();
            Position startPos = new Position(pos.getX(), pos.getY());

            // set the Empty Color to the activPlayers color
            getFieldOn(startPos).SetColor(activePlayer.getColor());

            foreach (Position dir in Position.Directions)
            {
                // set the position to the starting values
                pos = new Position(startPos.getX(), startPos.getY());
                // initialize a List to store the right Positions of one direction
                posforOneDir = new List<Position>();
                while (true)
                {
                    // step to a direction
                    pos += dir;

                    // break if the next position is invalid
                    if (!isPositionValid(pos)) { break; }
                    // break if the next position is Empty
                    if (getFieldOn(pos).GetColor() == Color.Empty)
                    { break; }

                    // if the starting color is the same the next position
                    // that means that this is a valid direction
                    if (getFieldOn(pos).GetColor() == getFieldOn(startPos).GetColor())
                    {
                        // so add them to the List Which will return
                        foreach (Position p in posforOneDir)
                        {
                            positions.Add(p);
                        }
                        break;
                    }
                    // if the next step has the opposite Color as the starting position
                    else
                    {
                        // these positions needs to be reversed
                        posforOneDir.Add(pos);
                    }
                }
            }
            

            // set the Empty Color to the activPlayers color
            getFieldOn(startPos).SetColor(Color.Empty);

            return positions;
        }
        private int getAngleBonus(Position pos)
        {
            Position startPos = new Position(pos.getX(), pos.getY());

            // set the Empty Color to the activPlayers color
            getFieldOn(startPos).SetColor(activePlayer.getColor());
            int bonus = 0;
            foreach (Position dir in Position.Directions)
            {
                // set the position to the starting values
                int count = 0;
                pos = new Position(startPos.getX(), startPos.getY());
                while (true)
                {
                    // step to a direction
                    pos += dir;
                    // break if the next position is invalid
                    if (!isPositionValid(pos)) { break; }
                    // break if the next position is Empty
                    if (getFieldOn(pos).GetColor() == Color.Empty)
                    { break; }

                    
                    if (getFieldOn(pos).GetColor() == getFieldOn(startPos).GetColor())
                    {
                        if (count > 0) {
                            bonus++;
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
            getFieldOn(startPos).SetColor(Color.Empty);
            return bonus;
        }

        private Player GetStartingPlayer()
        {
            Random rnd = new Random();
            if (rnd.Next(1) == 0) { return player1; }
            else { return player2; }
        }

        // Constructor For the Table
        public Table(Player pl1, Player pl2)
        {
            this.player1 = pl1;
            this.player2 = pl2;
            player1.setColor(Color.Black);
            player2.setColor(Color.White);

            // set up which Player will start the game.
            // who makes the first move
            activePlayer = GetStartingPlayer();

            // Set up the fields of the Table
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    fields[j, i] = new Field(new Position(j, i), Color.Empty);
                }
            }
            fields[3, 3].SetColor(Color.White);
            fields[4, 4].SetColor(Color.White);
            fields[3, 4].SetColor(Color.Black);
            fields[4, 3].SetColor(Color.Black);
            getValidMoves(activePlayer);
        }

        /**
         * returns if the move to the given Position is valid
         */
        private bool inValidMoves(Position position)
        {
            foreach (Position p in ValidMoves)
            {
                if (p == position) { return true; }
            }
            return false;
        }


        public void makeMove(Position position)
        {
            if (isGameOver()) 
            {
                return; 
            }
            Trace.WriteLine(activePlayer.getColor() + "'s Turn");

            // throw an Error message if the given position is not a valid Move
            if (!inValidMoves(position))
            {
                Trace.WriteLine("InvalidMove From " + activePlayer.getColor() + " " + position.write());
                return;
            }

            // set the Empty Color to the activPlayers color
            getFieldOn(position).SetColor(activePlayer.getColor());

            // Variable for counting reverses
            int countRev = 0;

            int Bonus = getAngleBonus(position);
            // Reverse the Colors 
            foreach (Position p in getReversedPositions(position))
            { 
                getFieldOn(p).ReverseColor();
                countRev++;
            }
            
            // set the Empty Color to the activPlayers color
            getFieldOn(position).SetColor(activePlayer.getColor());

            calculateScore(activePlayer, countRev, Bonus);

            switchPlayer();
            getValidMoves(activePlayer);

            PrintTable();
            if (activePlayer.getPlayerType() == PlayerType.AI) {
                if (isGameOver()) { return; }
                makeMove();
            }
        }
        public void makeMove() {
            makeMove(getBestValidMove());
        }

        private int countColorOnTable(Color color) {
            int count = 0;
            foreach (Field f in fields) {
                if (f.GetColor() == color) { count++; }
            }
            return count;
        }
        private bool isColorOnTableExist(Color color){
            foreach (Field f in fields){
                if (f.GetColor() == color) { return true; }
            }
            return false;
        }

        public void PassMove() {
            switchPlayer();
            getValidMoves(activePlayer);

            PrintTable();
            if (activePlayer.getPlayerType() == PlayerType.AI)
            {
                if (isGameOver()) { return; }
                makeMove();
            }
        }

        private void calculateScore(Player player, int n,int bonusForAngle) {
            Trace.WriteLine("CountingRev: "+n);
            Trace.WriteLine("BonusAngle"+ bonusForAngle);
            int score = 0;
            for (int i = 1; i <= n; i++) {
                score += i* PointValue;
            }
            score *= bonusForAngle;


            if (!isColorOnTableExist(player.getEnemyColor()))
            {
                score += 100 * PointValue * countColorOnTable(player.getColor());
            }

            player.addScore(score);
        }

        private bool isGameOver() {
            // Game Over
            if (ValidMoves.Count() == 0) {
                Trace.WriteLine("GameOver");
                return true;
            }
            return false;
        }

        private Position getBestValidMove()
        {
            Dictionary<Position, int> moves = new Dictionary<Position, int>();


            foreach (Position p in ValidMoves){
                moves.Add(p, getReversedPositions(p).Count());
            }

            
            int max = moves.Values.Max();

            var sol = from move in moves
                      where move.Value == max
                      select move.Key;

            int randomMovePositioNumber = 0;
            if (sol.Count() > 1) {
                Random random = new Random();
                randomMovePositioNumber = random.Next(sol.Count());
            }
            Trace.WriteLine("maxValue :"+ max);
            Trace.WriteLine("numberOFMaxValue :" + sol.Count());

            return sol.ElementAt(randomMovePositioNumber);

        }

        private void switchPlayer()
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

        /**
         * Print The Table to the Console
         */
        public void PrintTable()
        {
            String[,] symbols = new String[8, 8];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch (fields[j, i].GetColor())
                    {
                        case Color.Black:
                            symbols[j, i] = " B ";
                            break;
                        case Color.White:
                            symbols[j, i] = " W ";
                            break;
                        case Color.Empty:
                            symbols[j, i] = " O ";
                            break;
                    }
                }
            }
            foreach (Position pos in ValidMoves)
            {
                symbols[pos.getX(), pos.getY()] = " V ";
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Trace.Write(symbols[j, i]);
                }
                Trace.WriteLine("");
            }
            Trace.WriteLine("--------------------------");

        }
    }
}
