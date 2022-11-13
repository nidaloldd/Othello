using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Othello.Model
{
    class Table
    {
        Player player1, player2;
        private const int with =8;
        private const int height = 8;

        Field[,] fields = new Field[8, 8];
        // Timer Clock

        public Field[] getValidMoves(Player pl) {

            //TODO
            return null;
        }

        public Table(Player pl1, Player pl2){
            this.player1 = pl1;
            this.player2 = pl2;
     
            for (int i = 0; i < height; i++){
                for (int j = 0; j < with; j++){
                    fields[i, j] = new Field(Color.Empty);
                }
            }
            fields[3, 3] = new Field(Color.White);
            fields[4, 4] = new Field(Color.White);
            fields[3, 4] = new Field(Color.Black);
            fields[4, 3] = new Field(Color.Black);
        }


        public void PrintTable() {

            for (int i = 0;i< height; i++){
                for (int j = 0; j < with; j++){
                    switch (fields[i,j].GetColor())
                    {
                        case Color.Black:
                            Trace.Write(" B ");
                            break;
                        case Color.White:
                            Trace.Write(" W ");
                            break;
                        case Color.Empty:
                            Trace.Write(" E ");
                            break;
                    }
                }
                Trace.WriteLine("");
            }
        }

   
    }
}
