using Xunit;
using ConsoleCalculator.Exceptions;

namespace ConsoleCalculator.Tests
{
    public class ExpressionParserTests
    {
        #region SimpleTests
        public static IEnumerable<object[]> TestCasesSimple => new List<object[]>
        {
            new object[] { "4-3", new List<string>{ "4", "-", "3" } },
            new object[] { "4--3", new List<string>{ "4", "+", "3" } },
            new object[] { "4+-3*6", new List<string>{ "4", "-", "3", "*", "6" } },
            new object[] { "-4+3*5", new List<string>{ "-4", "+", "3", "*", "5" } },
            new object[] { "3*-5", new List<string>{ "3", "*", "-5" } },
            new object[] { "3 * 5 ", new List<string>{ "3", "*", "5" } },
        };

        [Theory]
        [MemberData(nameof(TestCasesSimple))]
        public void Evaluate_SimpleParse_ReturnsTokens(string expression, List<string> expected)
        {
            // Arrange
            // Act
            List<string> result = ExpressionParser.Parse(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        [Fact]
        public void Evaluate_EmptyStringParse_ReturnsTokens()
        {
            // Arrange
            string expression = "";

            // Act
            List<string> result = ExpressionParser.Parse(expression);

            // Assert
            Assert.Equal(new List<string> { }, result);
        }
        [Fact]
        public void Evaluate_SpaceStringParse_ReturnsTokens()
        {
            // Arrange
            string expression = " ";

            // Act
            List<string> result = ExpressionParser.Parse(expression);

            // Assert
            Assert.Equal(new List<string> { }, result);
        }

        [Fact]
        public void Evaluate_InvalidTokenParse_ReturnsTokens()
        {
            // Arrange
            string expression = "4+a";

            // Act
            // Assert
            Assert.Throws<InvalidTokenException>(() => ExpressionParser.Parse(expression));
        }

        #region NormalizeSignsTests
        public static IEnumerable<object[]> TestCasesNormalizeSigns => new List<object[]>
        {
            new object[] { "4-+3", new List<string>{ "4", "-", "3" } },
            new object[] { "4--3", new List<string>{ "4", "+", "3" } },
            new object[] { "4+-3", new List<string>{ "4", "-", "3" } },
            new object[] { "-4-----3*5", new List<string>{ "-4", "-", "3", "*", "5" } },
            new object[] { "3*-5", new List<string>{ "3", "*", "-5" } },
            new object[] { "3 *-- 5 ", new List<string>{ "3", "*", "5" } },
        };

        [Theory]
        [MemberData(nameof(TestCasesNormalizeSigns))]
        public void Evaluate_NormalizeSignsParse_ReturnsTokens(string expression, List<string> expected)
        {
            // Arrange
            // Act
            List<string> result = ExpressionParser.Parse(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion

        #region NormalizeSignsTests
        public static IEnumerable<object[]> TestCasesParentheses => new List<object[]>
        {
            new object[] { "4-(+3)", new List<string>{ "4", "-", "(", "3", ")" } },
            new object[] { "4-(3)", new List<string>{ "4", "-", "(", "3", ")" } },
            new object[] { "4+((-3))", new List<string>{ "4", "+", "(", "(", "-3", ")", ")" } },
            new object[] { "-4-(3+5)", new List<string>{ "-4", "-", "(", "3", "+", "5", ")" } },
            new object[] { "(3)*-5", new List<string>{ "(", "3", ")", "*", "-5" } },
            new object[] { "(3)", new List<string>{ "(", "3", ")"} },
        };

        [Theory]
        [MemberData(nameof(TestCasesNormalizeSigns))]
        public void Evaluate_ParenthesesParse_ReturnsTokens(string expression, List<string> expected)
        {
            // Arrange
            // Act
            List<string> result = ExpressionParser.Parse(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        #endregion
    }
}
