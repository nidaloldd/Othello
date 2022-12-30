using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    public class Player
    {
        private PlayerType playerType;
        private string name;
        private int score;
        private Color color;
        public int GetScore()
        {
            return score;
        }
        public void SetScore(int score)
        {
            this.score = score;
        }
        public void AddScore(int score)
        {
            this.score += score;
        }
        public string GetName()
        {
            return name;
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
            this.name = name;
        }
    }

    public enum PlayerType
    {
        Human,
        AI
    }
}
