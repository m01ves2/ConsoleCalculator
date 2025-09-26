using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    /// <summary>
    /// Provides string parsing for the calculator.
    /// </summary>
    internal static class ExpressionParser
    {
        /// <summary>
        /// Splits string to tokens
        /// </summary>
        /// <param name="expression">The expression to be splitted.</param>
        /// <returns>List of tokens</returns>
        public static List<string> Parse(string expression)
        {
            string[] splittedTokens = expression.Split(new char[] { ' ' });
            return new List<string>(splittedTokens);
        }
    }
}
