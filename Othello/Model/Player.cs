using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    class Player
    {
        PlayerType playerType;
        String name;
        int score;
        Color color;
        public int getScore() {
            return score;
        }
        public void setScore(int score){
            this.score = score;
        }
        public void addScore(int score)
        {
            this.score += score;
        }
        public string getName() {
            return name;
        }

        public PlayerType getPlayerType()
        {
            return playerType;
        }

        public Color getColor(){
            return color;
        }
        public void setColor(Color color){
            this.color = color;
        }

        public Player(PlayerType playerType, String name) {
            this.playerType = playerType;
            this.name = name;
        }

    }

    public enum PlayerType { 
        Human,
        AI
    }
}
