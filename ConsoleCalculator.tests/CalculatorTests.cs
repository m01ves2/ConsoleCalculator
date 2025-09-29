using Xunit;
using ConsoleCalculator;
using ConsoleCalculator.Exceptions;

namespace ConsoleCalculator.Tests
{
    public class CalculatorTests
    {
        public static IEnumerable<object[]> TestCasesSimple => new List<object[]>
        {
            new object[] { new List<string>{ "4", "-", "3" }, 1 },
            new object[] { new List<string>{ "2", "+", "5" }, 7 },
            new object[] { new List<string>{ "2", "*", "5" }, 10 },
            new object[] { new List<string>{ "20", "/", "5" }, 4 }
        };
        [Theory]
        [MemberData(nameof(TestCasesSimple))]
        public void Evaluate_SimpleOperations_ReturnsCorrectResult(List<string> tokens, double expected)
        {
            // Arrange
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(expected, result);
        }


        public static IEnumerable<object[]> TestCasesPriorities => new List<object[]>
        {
            new object[] { new List<string>{ "2", "+", "3", "*", "4" }, 14 },
            new object[] { new List<string>{ "10", "-", "2", "*", "3" }, 4 },
            new object[] { new List<string>{ "100", "/", "10", "+", "5" }, 15 },
            new object[] { new List<string>{ "10", "+", "20", "/", "5" }, 14 }
        };
        [Theory]
        [MemberData(nameof(TestCasesPriorities))]
        public void Evaluate_Priorities_ReturnsCorrectResult(List<string> tokens, double expected)
        {
            // Arrange
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(expected, result);
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

        [Fact]
        public void Evaluate_EmptyString_ThrowsException()
        {
            var tokens = new List<string> { "" };
            var calculator = new Calculator(tokens);

            Assert.Throws<InvalidNumberException>(() => calculator.Evaluate());
        }

        [Fact]
        public void Evaluate_OverflowLarge_Returns()
        {
            // Arrange
            var tokens = new List<string> { "1e308", "+", "1e308" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(Double.PositiveInfinity, result);
        }

        [Fact]
        public void Evaluate_OverflowSmall_Returns()
        {
            // Arrange
            var tokens = new List<string> { "1e-308", "/", "2" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.InRange(result, 4.999999e-309, 5.000001e-309);
        }

        [Fact]
        public void Evaluate_NegativeNumber_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<string> { "-5", "+", "3" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(-2, result);
        }

        [Fact]
        public void Evaluate_RatioNumber_ReturnsCorrectResult()
        {
            // Arrange
            var tokens = new List<string> { "-2.5", "*", "4" };
            var calculator = new Calculator(tokens);

            // Act
            var result = calculator.Evaluate();

            // Assert
            Assert.Equal(-10, result);
        }
    }
}