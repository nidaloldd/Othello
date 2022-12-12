using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Othello.Model
{
    class Position
    {
        private int X { get; set; }
        private int Y { get; set; }

        

        public int getX() {
            return X;
        }
        public int getY() {
            return Y;
        }

        public Position()
        {

            this.X = 0;
            this.Y = 0;
        }
        public Position(int X, int Y) {

            this.X = X;
            this.Y = Y;
        }

        public static Position[] Directions = new Position[8] { 
            new Position().Up(),
            new Position().DiagonalUpRight(),
            new Position().Right(),
            new Position().DiagonalDownRight(),
            new Position().Down(),
            new Position().DiagonalDownLeft(),
            new Position().Left(),
            new Position().DiagonalUpLeft()

        };

        /*
        public static Position Up = new Position(-1, 0);
        public static Position DiagonalUpRight = new Position(-1, 1);
        public static Position Right = new Position(1, 0);
        public static Position DiagonalDownRight = new Position(1, 1);
        public static Position Down = new Position(1, 0);
        public static Position DiagonalDownLeft = new Position(1, -1);
        public static Position Left = new Position(-1, 0);
        public static Position DiagonalUpLeft = new Position(-1, -1);
        */
        
        public Position Up()
        {
            Y -= 1;
            return this;
        }
        public Position DiagonalUpRight()
        {
            Y -= 1;
            X += 1;
            return this;
        }
        public Position Right()
        {
            X += 1;
            return this;
        }
        public Position DiagonalDownRight()
        {
            Y += 1;
            X += 1;
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
            Y -= 1;
            X -= 1;
            return this;
        }
        public Position opposite() {

            Y *= -1;
            X *= -1;
            return this;
        }
        
        public String write() {
            return ("X :" + getX() + " Y :"+getY());
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1.getX()==pos2.getX() && pos1.getY() == pos2.getY();
        }
        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position( pos1.getX() + pos2.getX() , pos1.getY() + pos2.getY());
        }
        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1.getX() == pos2.getX() && pos1.getY() == pos2.getY());
        }

    }
}
