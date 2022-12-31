namespace Othello.Model
{
    public class Field
    {
        private Color color;
        public Position position;

        public Field(Position position, Color color)
        {
            this.position = position;
            this.color = color;
        }

        /*
         * Set the Color property to the opposite Color
         * Black->White
         * White->Black
         * In case of Empty Color no change occurs
        */
        public void ReverseColor()
        {
            if (this.color == Color.White)
            {
                this.color = Color.Black;
            }
            else if (this.color == Color.Black)
            {
                this.color = Color.White;
            }
        }

        /*
         * returns the opposite Color
         * In case of Empty returns Empty Color
         */
        public Color GetReverseColor()
        {
            if (this.color == Color.White)
            {
                return Color.Black;
            }
            else if (this.color == Color.Black)
            {
                return Color.White;
            }
            else
            {
                return Color.Empty;
            }
        }
        public Color GetColor()
        {
            return color;
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }
    }

    public enum Color
    {
       Empty,
       Black,
       White,
    }
}
