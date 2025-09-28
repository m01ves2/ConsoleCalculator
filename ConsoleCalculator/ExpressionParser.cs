using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using ConsoleCalculator.Exceptions;

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
            expression = expression.Replace(" ", String.Empty);
            expression = expression.Replace(",", ".");
            List<string> tokens = new List<string>();

            bool expectOperand = true;
            string token = "";
            for (int i = 0; i < expression.Length; i++) {


                if (expression[i] == '-' && expectOperand) { //found '-' as sign
                    token = "-";
                    expectOperand = false;
                    continue;
                }

                if (char.IsDigit(expression[i]) || (expression[i] == '.')) {
                    token += expression[i];
                    expectOperand = false;
                }
                else
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/') {
                    if (!string.IsNullOrEmpty(token))
                        tokens.Add(token);
                    token = "";

                    tokens.Add(expression[i].ToString());
                    expectOperand = true;
                }
                else
                if (expression[i] == '(' || expression[i] == ')') {
                    throw new InvalidTokenException("While not supported");
                }
                else {
                    throw new InvalidTokenException($"Unexpected character: {expression[i]}");
                }

            }
            
            if(!string.IsNullOrEmpty(token))
                tokens.Add(token);
            return tokens;
        }
    }
}
