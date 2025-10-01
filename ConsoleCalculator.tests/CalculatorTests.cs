using Xunit;
using ConsoleCalculator;
using ConsoleCalculator.Exceptions;

namespace ConsoleCalculator.Tests
{
    public class CalculatorTests
    {
        #region BasicOperationsTests
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
        #endregion

        #region PrioritiesTests
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

        #endregion

        #region ExceptionTests
        [Fact]
        public void Evaluate_InvalidNumber_ThrowsException()
        {
            var tokens = new List<string> { "2a", "+", "3" };
            var calculator = new Calculator(tokens);

            Assert.Throws<InvalidTokenException>(() => calculator.Evaluate());
        }

        [Fact]
        public void Evaluate_EmptyString_ThrowsException()
        {
            var tokens = new List<string> { "" };
            var calculator = new Calculator(tokens);

            Assert.Throws<InvalidTokenException>(() => calculator.Evaluate());
        }

        public static IEnumerable<object[]> TestCasesExceptions => new List<object[]>
        {
            new object[] { new List<string>{ "5", "/", "0" }},
            new object[] { new List<string>{ "2", "+", "("}},
            new object[] { new List<string>{ "3", "+", ")"}},
            new object[] { new List<string>{ "2", "+", "(", ")"}},
            new object[] { new List<string>{ "2", "+", "@", "3" }},
        };

        [Theory]
        [MemberData(nameof(TestCasesExceptions))]
        public void Calculator_ThrowsException_OnInvalidExpressions(List<string> tokens)
        {
            var calc = new Calculator(tokens);
            Assert.ThrowsAny<CalculatorException>(() => calc.Evaluate());
        }

        #endregion

        #region OverflowTests
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

        #endregion

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

        #region ShuntingYardTests
        public static IEnumerable<object[]> TestCasesShuntingYard => new List<object[]>
        {
            new object[] { new List<string>{ "3", "+", "(", "(", "-2", ")", ")" }, 1 },
            new object[] { new List<string>{ "-2", "*", "(", "-3", "+", "4" , ")", "+", "7"}, 5 },
            new object[] { new List<string>{ "2", "*", "(", "-3", ")" }, -6 },
        };
        [Theory]
        [MemberData(nameof(TestCasesShuntingYard))]
        public void ShuntingYard_CorrectResults(List<string> tokens, double expected)
        {
            var calc = new Calculator(tokens);
            double result = calc.Evaluate();
            Assert.Equal(expected, result);
        }

        #endregion
    }
}