namespace Othello.Tests.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Othello.Model;

    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void GetX_PositionXIs5_Returns5()
        {
            // Arrange
            var position = new Position(5, 0);

            // Act
            var result = position.X;

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void GetY_PositionYIs3_Returns3()
        {
            // Arrange
            var pos = new Position(0, 3);

            // Act
            var result = pos.Y;

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Up_PositionYIs5_DecrementsYBy1()
        {
            // Arrange
            var position = new Position();

            // Act
            var result = position.Up();

            // Assert
            Assert.AreEqual(-1, result.Y);
        }

        [TestMethod]
        public void DiagonalUpRight_PositionYIs2AndXIs1_DecrementsYBy1AndIncrementsXBy1()
        {
            // Arrange
            var pos = new Position(0, 0);

            // Act
            var result = pos.DiagonalUpRight();

            // Assert
            Assert.AreEqual(1, result.X);
            Assert.AreEqual(-1, result.Y);
        }

        [TestMethod]
        public void Right_PositionXIs5_IncrementsXBy1()
        {
            // Arrange
            var position = new Position(5, 0);

            // Act
            var result = position.Right();

            // Assert
            Assert.AreEqual(6, result.X);
        }

        [TestMethod]
        public void DiagonalDownRight_PositionYIs5AndXIs3_IncrementsYBy1AndIncrementsXBy1()
        {
            // Arrange
            var pos = new Position(5, 3);

            // Act
            var result = pos.DiagonalDownRight();

            // Assert
            Assert.AreEqual(4, result.Y);
            Assert.AreEqual(6, result.X);
        }

        [TestMethod]
        public void Down_PositionYIs5_IncrementsYBy1()
        {
            // Arrange
            var position = new Position(5, 0);

            // Act
            var result = position.Down();

            // Assert
            Assert.AreEqual(1, result.Y);
        }

        [TestMethod]
        public void DiagonalDownLeft_PositionYIs5AndXIs3_IncrementsYBy1AndDecrementsXBy1()
        {
            // Arrange
            var position = new Position(3, 5);

            // Act
            var result = position.DiagonalDownLeft();

            // Assert
            Assert.AreEqual(6, result.Y);
            Assert.AreEqual(2, result.X);
        }

        [TestMethod]
        public void Left_PositionXIs5_DecrementsXBy1()
        {
            // Arrange
            var position = new Position(5, 0);

            // Act
            var result = position.Left();

            // Assert
            Assert.AreEqual(4, result.X);
        }

        [TestMethod]
        public void DiagonalUpLeft_PositionYIs3AndXIs5_DecrementsYBy1AndDecrementsXBy1()
        {
            // Arrange
            var position = new Position(0, 0);

            // Act
            var result = position.DiagonalUpLeft();

            // Assert
            Assert.AreEqual(-1, result.X);
            Assert.AreEqual(-1, result.Y);
        }

        [TestMethod]
        public void Write_PositionXIs5AndYIs3_ReturnsStringRepresentationOfPosition()
        {
            // Arrange
            var position = new Position(3, 5);

            // Act
            var result = position.Write();

            // Assert
            Assert.AreEqual("X :3 Y :5", result);
        }

        [TestMethod]
        public void EqualityOperator_PositionsAreEqual_ReturnsTrue()
        {
            // Arrange
            var position1 = new Position(5, 3);
            var position2 = new Position(5, 3);

            // Act
            var result = position1 == position2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InequalityOperator_PositionsAreNotEqual_ReturnsTrue()
        {
            // Arrange
            var position1 = new Position(5, 3);
            var position2 = new Position(6, 3);

            // Act
            var result = position1 != position2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AdditionOperator_PositionsAreAdded_ReturnsNewPositionWithSumOfCoordinates()
        {
            // Arrange
            var position1 = new Position(5, 3);
            var position2 = new Position(2, 4);

            // Act
            var result = position1 + position2;

            // Assert
            Assert.AreEqual(7, result.X);
            Assert.AreEqual(7, result.Y);
        }
    }
}