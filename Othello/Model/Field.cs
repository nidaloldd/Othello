using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    class Field
    {
        Color color;
        int posX,posY;
        public Field()
        {
            this.color = Color.Empty;
            this.posX = -1;
            this.posY = -1;
        }
        public Field(Color color,int posX,int posY) {
            this.color = color;
            this.posX = posX;
            this.posY = posY;
        }

        public void Reverse() { 
         // ToDO
        }
        public Color GetColor() {
            return color;
        }
        public Field[] getNeighbors()
        {
            // ToDO
            return null;
        }
        public void getUp()
        {
            // ToDO
        }
        public void getDown()
        {
            // ToDO
        }
        public void getLeft()
        {
            // ToDO
        }
        public void getRight()
        {
            // ToDO
        }
    }
    public enum Color { 
       Empty,
       Black,
       White
    }
}
