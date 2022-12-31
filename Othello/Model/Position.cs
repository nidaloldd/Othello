using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Othello.Model
{
    public class Position
    {
        private int _x;
        private int _y;
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public Position()
        {
            _x = 0;
            _y = 0;
        }
        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

        // An Array that contains all possible directions.
        public static readonly Position[] Directions = new Position[8]
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
            _y -= 1;
            return this;
        }
        public Position DiagonalUpRight()
        {
            _x += 1;
            _y -= 1;
            return this;
        }
        public Position Right()
        {
            _x += 1;
            return this;
        }
        public Position DiagonalDownRight()
        {
            _x += 1;
            _y += 1;
            return this;
        }
        public Position Down()
        {
            _y += 1;
            return this;
        }
        public Position DiagonalDownLeft()
        {
            _y += 1;
            _x -= 1;
            return this;
        }
        public Position Left()
        {
            _x -= 1;
            return this;
        }
        public Position DiagonalUpLeft()
        {
            _x -= 1;
            _y -= 1;
            return this;
        }

        // Returns the Position as a string.
        public string Write()
        {
            return ("X :" + _x + " Y :" + _y);
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return pos1._x == pos2._x && pos1._y == pos2._y;
        }
        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position(pos1._x + pos2._x, pos1._y + pos2._y);
        }
        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1._x == pos2._x && pos1._y == pos2._y);
        }
    }
}
