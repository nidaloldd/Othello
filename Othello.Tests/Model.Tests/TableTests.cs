using Microsoft.VisualStudio.TestTools.UnitTesting;
using Othello.Model;
using System.Linq;

namespace Othello.Tests.Model.Tests
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void TestIsPositionValid()
        {
            //Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            //Act
            /**/

            //Assert
            Assert.IsTrue(table.IsPositionValid(new Position(0, 0)));
            Assert.IsTrue(table.IsPositionValid(new Position(7, 7)));
            Assert.IsFalse(table.IsPositionValid(new Position(-1, 0)));
            Assert.IsFalse(table.IsPositionValid(new Position(8, 0)));
        }

        [TestMethod]
        public void TestGetValidMoves()
        {
            //Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            //Act
            table.fields[3, 3].SetColor(player1.GetColor()); 
            table.fields[4, 3].SetColor(player2.GetColor());
            table.fields[2, 4].SetColor(player2.GetColor());
            table.fields[4, 2].SetColor(player2.GetColor());

            //Assert
            table.GetValidMoves(player1);
            Assert.IsTrue(table.ValidMoves.Any(x => x.GetX() == 5 && x.GetY() == 5));
            Assert.IsFalse(table.ValidMoves.Any(x => x.GetX() == 4 && x.GetY() == 3));
            Assert.IsFalse(table.ValidMoves.Any(x => x.GetX() == 3 && x.GetY() == 4));

            //Assert
            table.GetValidMoves(player2);
            Assert.IsTrue(table.ValidMoves.Any(x => x.GetX() == 2 && x.GetY() == 2 ));
            Assert.IsFalse(table.ValidMoves.Any(x => x.GetX() == 3 && x.GetY() == 3 ));
            Assert.IsFalse(table.ValidMoves.Any(x => x.GetX() == 5 && x.GetY() == 5 ));
        }

        [TestMethod]
        public void TestMakeMove()
        {
            //Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            //Act
            table.fields[3, 3].SetColor(player1.GetColor());
            table.fields[4, 4].SetColor(player1.GetColor());
            table.fields[2, 4].SetColor(player2.GetColor());
            table.fields[4, 2].SetColor(player2.GetColor());

            table.MakeMove(new Position(3, 2));

            //Assert
            Assert.AreEqual(player1.GetColor(), table.GetFieldOn(new Position(3, 4)).GetColor());
            Assert.AreNotEqual(player2.GetColor(), table.GetFieldOn(new Position(3, 3)).GetColor());
            Assert.AreEqual(player1.GetColor(), table.GetFieldOn(new Position(4, 4)).GetColor());

            Assert.AreEqual(0, player1.GetScore());

        }
    }
}
