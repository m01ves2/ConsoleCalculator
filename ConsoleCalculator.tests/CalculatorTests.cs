using Xunit;
using ConsoleCalculator;
using ConsoleCalculator.Exceptions;

namespace ConsoleCalculator.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Evaluate_SimpleAddition_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<string> { "2", "+", "3" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Evaluate_SimpleSubstraction_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<string> { "2", "-", "3" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void Evaluate_SimpleMultiplication_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<string> { "2", "*", "3" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void Evaluate_SimpleDivision_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<string> { "6", "/", "3" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Evaluate_DivisionByZero_ThrowsException()
        {
            // Arrange
            var tokens = new List<string> { "10", "/", "0" };
            var calculator = new Calculator(tokens);

            // Act + Assert
            Assert.Throws<DivideByZeroCalculatorException>(() => calculator.Evaluate());
        }

        [Fact]
        public void Evaluate_InvalidNumber_ThrowsException()
        {
            var tokens = new List<string> { "2a", "+", "3" };
            var calculator = new Calculator(tokens);

            Assert.Throws<InvalidNumberException>(() => calculator.Evaluate());
        }
    }
}