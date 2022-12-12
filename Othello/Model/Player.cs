using System;
using System.Collections.Generic;
using System.Text;

namespace Othello.Model
{
    class Player
    {
        PlayerType playerType { get; }
        String name { get;}
        int Score { get; set; }
        Color color { get; set; }
        public bool isPlayerTurn = false;

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
