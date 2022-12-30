using Microsoft.VisualStudio.TestTools.UnitTesting;
using Othello.Model;

namespace Othello.Tests.Model.Tests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void ReverseColor_WhiteField_ChangesColorToBlack()
        {
            // Arrange
            var field = new Field(new Position(0, 0), Color.White);

            // Act
            field.ReverseColor();

            // Assert
            Assert.AreEqual(Color.Black, field.GetColor());
        }

        [TestMethod]
        public void ReverseColor_BlackField_ChangesColorToWhite()
        {
            // Arrange
            var field = new Field(new Position(0, 0), Color.Black);

            // Act
            field.ReverseColor();

            // Assert
            Assert.AreEqual(Color.White, field.GetColor());
        }

        [TestMethod]
        public void ReverseColor_EmptyField_DoesNotChangeColor()
        {
            // Arrange
            var field = new Field(new Position(0, 0), Color.Empty);

            // Act
            field.ReverseColor();

            // Assert
            Assert.AreEqual(Color.Empty, field.GetColor());
        }

        [TestMethod]
        public void GetReverseColor_WhiteField_ReturnsBlack()
        {
            // Arrange
            var field = new Field(new Position(0, 0), Color.White);

            // Act
            var result = field.GetReverseColor();

            // Assert
            Assert.AreEqual(Color.Black, result);
        }

        [TestMethod]
        public void GetReverseColor_BlackField_ReturnsWhite()
        {
            // Arrange
            var field = new Field(new Position(0, 0), Color.Black);

            // Act
            var result = field.GetReverseColor();

            // Assert
            Assert.AreEqual(Color.White, result);
        }

        [TestMethod]
        public void GetReverseColor_EmptyField_ReturnsEmpty()
        {
            // Arrange
            var field = new Field(new Position(0, 0), Color.Empty);

            // Act
            var result = field.GetReverseColor();

            // Assert
            Assert.AreEqual(Color.Empty, result);
        }
    }
}
