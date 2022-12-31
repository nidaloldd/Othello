namespace Othello.Tests.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Othello.Model;

    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void GetScore_PlayerHas10Points_Returns10()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");
            player.SetScore(10);

            // Act
            var result = player.Score;

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void SetScore_PlayerHas5Points_SetsScoreTo7()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");
            player.SetScore(5);

            // Act
            player.SetScore(7);

            // Assert
            Assert.AreEqual(7, player.Score);
        }

        [TestMethod]
        public void AddScore_PlayerHas5Points_Adds3Points()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");
            player.SetScore(5);

            // Act
            player.AddScore(3);

            // Assert
            Assert.AreEqual(8, player.Score);
        }

        [TestMethod]
        public void GetName_PlayerNameIsJohn_ReturnsJohn()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");

            // Act
            var result = player.Name;

            // Assert
            Assert.AreEqual("John", result);
        }

        [TestMethod]
        public void GetPlayerType_PlayerTypeIsAI_ReturnsAI()
        {
            // Arrange
            var player = new Player(PlayerType.AI, "John");

            // Act
            var result = player.GetPlayerType();

            // Assert
            Assert.AreEqual(PlayerType.AI, result);
        }

        [TestMethod]
        public void GetColor_PlayerColorIsWhite_ReturnsWhite()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");
            player.SetColor(Color.White);

            // Act
            var result = player.GetColor();

            // Assert
            Assert.AreEqual(Color.White, result);
        }

        [TestMethod]
        public void GetEnemyColor_PlayerColorIsWhite_ReturnsBlack()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");
            player.SetColor(Color.White);

            // Act
            var result = player.GetEnemyColor();

            // Assert
            Assert.AreEqual(Color.Black, result);
        }

        [TestMethod]
        public void GetEnemyColor_PlayerColorIsBlack_ReturnsWhite()
        {
            // Arrange
            var player = new Player(PlayerType.Human, "John");
            player.SetColor(Color.Black);

            // Act
            var result = player.GetEnemyColor();

            // Assert
            Assert.AreEqual(Color.White, result);
        }
    }
}