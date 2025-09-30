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
    public static class ExpressionParser
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
            expression = NormalizeSigns(expression);

            List<string> tokens = new List<string>();

            bool expectOperand = true;
            string token = "";
            for (int i = 0; i < expression.Length; i++) {

                if (expression[i] == '-' && expectOperand ) { //found '-' as sign
                    token = "-";
                    expectOperand = false;
                    continue;
                }
                if (expression[i] == '+' && expectOperand) { //found '+' as sign
                    token = "";
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
                if (expression[i] == '(') {
                    if (token == "-" || token == "+") {
                        tokens.Add(token);
                        token = "";
                    }
                    tokens.Add(expression[i].ToString());
                    expectOperand = true;
                }
                else
                if (expression[i] == ')') {
                    if (!string.IsNullOrEmpty(token)) {
                        tokens.Add(token);
                        token = "";
                    }
                    tokens.Add(expression[i].ToString());
                    expectOperand = false;
                }
                else {
                    throw new InvalidTokenException($"Unexpected character #{i}: {expression[i]}");
                }

            }
            
            if(!string.IsNullOrEmpty(token))
                tokens.Add(token);
            return tokens;
        }

        private static string NormalizeSigns(string expression)
        {
            bool isChanged;
            do {
                string oldExpression = new string(expression);
                expression = expression.Replace("++", "+");
                expression = expression.Replace("-+", "-");
                expression = expression.Replace("+-", "-");
                expression = expression.Replace("--", "+");

                isChanged = (oldExpression != expression);
            } while (isChanged);
            return expression;
        }
    }
}
