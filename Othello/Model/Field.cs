using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    class Field
    {
        Color color;
        Position position;

        public Field(Position position , Color color) {
            this.position = position;
            this.color = color;
        }

        public void ReverseColor(){
            if (this.color == Color.White) { this.color = Color.Black; }
            else if (this.color == Color.Black) { this.color = Color.White; }
        }
        public Color GetReverseColor()
        {
            if (this.color == Color.White) { return Color.Black; }
            else if (this.color == Color.Black) { return Color.White; }
            else return Color.Empty;
        }
        public Color GetColor(){
            return color;
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }



    }
    public enum Color { 
       Empty,
       Black,
       White,
    }
}
