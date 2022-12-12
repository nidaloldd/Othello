using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Othello.Model
{
    class Table
    {
        Player player1, player2;
        Player activPlayer;
        private const int with = 8;
        private const int height = 8;

        private Field[,] fields = new Field[8, 8];
        public List<Position> ValidMoves = new List<Position>();

        public Field getFieldOn(Position position)
        {
    
            return fields[position.getX(), position.getY()];
            
        }
        public Field[,] getFields() {
            return fields;
        }

        public bool isPositionValid(Position position) {
            if (position.getX() > -1 && position.getX() < with &&
                position.getY() > -1 && position.getY() < height)
            {
                return true;
            }
            return false;
        }

        // Timer Clock

        public void getValidMoves(Player player) {
            ValidMoves = new List<Position>();

            for (int i = 0; i < height; i++) {
                for (int j = 0; j < with; j++) {
                    if (fields[i, j].GetColor() == player.getColor()) {

                        getValiddMovesFrom(i, j);
 
                    }
                }
            } 
        }

        // mod =>  "forValidMoves" "forMove" "goodDir"
        private void getValidMovesFrom(Position startPos,Position direction) {
            Position position = new Position(startPos.getX(), startPos.getY());
            //Trace.WriteLine("+1: "+position.write());

            if (!isPositionValid(position + direction)) 
            { return; }
            if (getFieldOn(position+direction).GetColor() != getFieldOn(startPos).GetReverseColor())
            { return; }
            //Trace.WriteLine("+2: " + position.write());

            while (true)
            {
                position += direction;
                if (!isPositionValid(position)) { break; }

                // HA ugyan azzal a szinnel találkozik
                if (
                (getFieldOn(position).GetColor() == getFieldOn(startPos).GetColor())) {break; 
                }
                else if (getFieldOn(position).GetColor() == Color.Empty)
                {
                    ValidMoves.Add(position);
                    break;
                }
            }
        }

        private void getValiddMovesFrom(int X,int Y){

            if (fields[X, Y].GetColor() == Color.Empty) { return; }
            Position startPosition = new Position(X, Y);

            foreach (Position dir in Position.Directions)
            {
                getValidMovesFrom(startPosition, dir);
            }
        }

        private List<Position> getReversedPositions(Position pos) {
            List<Position> positions = new List<Position>();
            List<Position> posforOneDir = new List<Position>();
            Position startPos = new Position(pos.getX(), pos.getY());
                
            foreach (Position dir in Position.Directions)
            {
                pos = new Position(startPos.getX(), startPos.getY());
                posforOneDir = new List<Position>();
                while (true)
                {
                    pos += dir;
                    
                    if (!isPositionValid(pos)) {  break; }
                    if (getFieldOn(pos).GetColor() == Color.Empty)
                    {  break; }

                    if (getFieldOn(pos).GetColor() == getFieldOn(startPos).GetColor())
                    {
                        foreach (Position p in posforOneDir) {
                            positions.Add(p);
                        }
                        
                        break;
                    }
                    else {
                        
                        posforOneDir.Add(pos);
                    }
                }
            }

            return positions;
        }

        public Table(Player pl1, Player pl2){
            this.player1 = pl1;
            this.player2 = pl2;
            player1.setColor(Color.Black);
            player2.setColor(Color.White);
            activPlayer = player1;
            for (int i = 0; i < height; i++){
                for (int j = 0; j < with; j++){
                    fields[j, i] = new Field(new Position(j,i), Color.Empty);
                }
            }
            fields[3, 3].SetColor(Color.White);
            fields[4, 4].SetColor(Color.White);
            fields[3, 4].SetColor(Color.Black);
            fields[4, 3].SetColor(Color.Black);
            getValidMoves(activPlayer);
        }

        private bool inValidMoves(Position position) {
            foreach (Position p in ValidMoves)
            {
                //Trace.WriteLine(p.write());
                if (p == position) { return true; }
            }
            return false;
        }
        
        public void makeMove(Position position){
            
            if (!inValidMoves(position)) {
                Trace.WriteLine("InvalidMove From "+activPlayer.getColor()+" "+position.write());
                return; 
            }

            getFieldOn(position).SetColor(activPlayer.getColor());
            Trace.WriteLine(getReversedPositions(position).Count);
            foreach (Position p in getReversedPositions(position)) {

                Trace.WriteLine("+" + p.write());
                getFieldOn(p).ReverseColor();
            }
            

            PrintTable();
            switchPlayer();
            getValidMoves(activPlayer);
        }

        private void switchPlayer() {
            if (activPlayer == player1) {
                activPlayer = player2;
            }
            else if (activPlayer == player2)
            {
                activPlayer = player1;
            }
        }


        public void PrintTable()
        {
            String[,] symbols = new String[8, 8];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < with; j++)
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
                for (int j = 0; j < with; j++)
                {
                    Trace.Write(symbols[j, i]);
                }
                Trace.WriteLine("");
            }
            Trace.WriteLine("--------------------------");

            }
        }
    }
