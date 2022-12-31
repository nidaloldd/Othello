using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Othello.Model
{
    public class Position
    {
        private int X { get; set; }
        private int Y { get; set; }
        public int GetX()
        {
            return X;
        }
        public int GetY()
        {
            return Y;
        }
        public Position()
        {
            this.X = 0;
            this.Y = 0;
        }
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /*
         * An Array that contains all possible directions
         */
        public static Position[] Directions = new Position[8]
        {
            new Position().Up(),
            new Position().DiagonalUpRight(),
            new Position().Right(),
            new Position().DiagonalDownRight(),
            new Position().Down(),
            new Position().DiagonalDownLeft(),
            new Position().Left(),
            new Position().DiagonalUpLeft()
        };

        public Position Up()
        {
            Y -= 1;
            return this;
        }
        public Position DiagonalUpRight()
        {
            X += 1;
            Y -= 1;
            return this;
        }
        public Position Right()
        {
            X += 1;
            return this;
        }
        public Position DiagonalDownRight()
        {
            X += 1;
            Y += 1;
            return this;
        }
        public Position Down()
        {
            Y += 1;
            return this;
        }
        public Position DiagonalDownLeft()
        {
            Y += 1;
            X -= 1;
            return this;
        }
        public Position Left()
        {
            X -= 1;
            return this;
        }
        public Position DiagonalUpLeft()
        {
            X -= 1;
            Y -= 1;
            return this;
        }
        /*
         *  returns the Position as a string
         */
        public string Write()
        {
            return ("X :" + GetX() + " Y :" + GetY());
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.GetX() == pos2.GetX() && pos1.GetY() == pos2.GetY();
        }
        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position(pos1.GetX() + pos2.GetX(), pos1.GetY() + pos2.GetY());
        }
        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1.GetX() == pos2.GetX() && pos1.GetY() == pos2.GetY());
        }


    }
}
