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

   
        public Player(PlayerType playerType, String name) {
            this.playerType = playerType;
            this.name = name;
        }

        public void MakeMove(Field field) { 
             // TODO
        }
    }

    public enum PlayerType { 
        Human,
        AI
    }
}
