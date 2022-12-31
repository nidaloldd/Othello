using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    public class Player
    {
        private string _name;
        private int _score;
        public Color color;
        public PlayerType playerType;
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        public void SetScore(int score)
        {
            _score = score;
        }
        public void AddScore(int score)
        {
            _score += score;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public PlayerType GetPlayerType()
        {
            return playerType;
        }

        public Color GetColor()
        {
            return color;
        }
        public Color GetEnemyColor()
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            if (color == Color.Black)
            {
                return Color.White;
            }
            return Color.Empty;
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }

        public Player(PlayerType playerType, string name)
        {
            this.playerType = playerType;
            _name = name;
        }
    }

    public enum PlayerType
    {
        Human,
        AI
    }
}
