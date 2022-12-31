namespace Othello.Tests.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Othello.Model;

    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void TestIsPositionValid()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            // Act
            // ---

            // Assert
            Assert.IsTrue(table.IsPositionValid(new Position(0, 0)));
            Assert.IsTrue(table.IsPositionValid(new Position(7, 7)));
            Assert.IsFalse(table.IsPositionValid(new Position(-1, 0)));
            Assert.IsFalse(table.IsPositionValid(new Position(8, 0)));
        }

        [TestMethod]
        public void TestGetValidMoves()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            // Act
            table.Fields[3, 3].SetColor(player1.GetColor());
            table.Fields[4, 3].SetColor(player2.GetColor());
            table.Fields[2, 4].SetColor(player2.GetColor());
            table.Fields[4, 2].SetColor(player2.GetColor());

            // Assert
            table.GetValidMoves(player1);
            Assert.IsTrue(table.ValidMoves.Any(x => x.X == 5 && x.Y == 5));
            Assert.IsFalse(table.ValidMoves.Any(x => x.X == 4 && x.Y == 3));
            Assert.IsFalse(table.ValidMoves.Any(x => x.X == 3 && x.Y == 4));

            // Assert
            table.GetValidMoves(player2);
            Assert.IsTrue(table.ValidMoves.Any(x => x.X == 2 && x.Y == 2));
            Assert.IsFalse(table.ValidMoves.Any(x => x.X == 3 && x.Y == 3));
            Assert.IsFalse(table.ValidMoves.Any(x => x.X == 5 && x.Y == 5));
        }

        [TestMethod]
        public void TestMakeMove()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            // Act
            table.Fields[3, 3].SetColor(player1.GetColor());
            table.Fields[4, 4].SetColor(player1.GetColor());
            table.Fields[2, 4].SetColor(player2.GetColor());
            table.Fields[4, 2].SetColor(player2.GetColor());

            table.MakeMove(new Position(3, 2));

            // Assert
            Assert.AreEqual(player1.GetColor(), table.GetFieldOn(new Position(3, 4)).GetColor());
            Assert.AreNotEqual(player2.GetColor(), table.GetFieldOn(new Position(3, 3)).GetColor());
            Assert.AreEqual(player1.GetColor(), table.GetFieldOn(new Position(4, 4)).GetColor());

            Assert.AreEqual(0, player1.Score);
        }

        [TestMethod]
        public void TestIsGameOver_False()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            // Act
            table.Fields[3, 3].SetColor(player1.GetColor());
            table.Fields[4, 4].SetColor(player1.GetColor());
            table.Fields[2, 4].SetColor(player2.GetColor());
            table.Fields[4, 2].SetColor(player2.GetColor());

            // Assert
            Assert.IsFalse(table.IsGameOver());
        }

        [TestMethod]
        public void TestisGameOver_true()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);

            // Act
            foreach (var field in table.Fields)
            {
                field.SetColor(player1.GetColor());
            }

            // Assert
            Assert.IsTrue(table.IsGameOver());
        }
        
        [TestMethod]
        public void TestMakeMoveMethod()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);
            Position startPos = new Position(3, 3);
            Position endPos = new Position(4, 3);
            table.Fields[3, 3] = new Field(startPos, Color.Black);
            table.Fields[4, 3] = new Field(endPos, Color.Empty);
            table.ActivePlayer = player1;
            table.ValidMoves.Add(endPos);

            // Act
            table.MakeMove(endPos);

            // Assert
            Assert.AreEqual(Color.Black, table.GetFieldOn(endPos).GetColor());
            Assert.AreEqual(Color.Black, table.GetFieldOn(startPos).GetColor());
            Assert.AreEqual(Color.White, table.ActivePlayer.GetColor());
        }

        [TestMethod]
        public void TestGetBestValidMoveMethod()
        {
            // Arrange
            Player player1 = new Player(PlayerType.Human, "player1");
            Player player2 = new Player(PlayerType.AI, "player2");
            var table = new Table(player1, player2);
            var move1 = new Position(3, 4);
            table.GetFieldOn(move1).SetColor(Color.White);
            var move2 = new Position(4, 4);
            table.GetFieldOn(move2).SetColor(Color.White);
            table.GetValidMoves(player2);
            
            // Act
            var result1 = table.GetBestValidMove();
            var result2 = table.GetBestValidMove();

            // Assert
            Assert.IsTrue(result1 == move1 || result1 == move2);
            Assert.IsTrue(result2 == move1 || result2 == move2);
            Assert.AreNotEqual(result1, result2);

      }
    }
}
