using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    /// <summary>
    /// Provides basic math operations for the calculator.
    /// </summary>
    internal class Calculator
    {
        private readonly List<string> tokens;

        public Calculator(List<string> tokens)
        {
            this.tokens = tokens; // ["2", "+", "3", "*", "5"]
        }

        /// <summary>
        /// evaluates math expression
        /// </summary>
        /// <returns>The result of expression</returns>
        public double Evaluate()
        {
            double result = 0;
            ProcessMulDiv();
            result = ProcessAddSubstr();
            return result;
        }

        private void ProcessMulDiv() //Handle * and /
        {
            int index = 0;

            while (index < tokens.Count) {
                string token = tokens[index];
                double result = 0;

                if (token == "*" || token == "/") {
                    if (index <= 0 || index >= tokens.Count - 1) // no operands for operation
                        throw new InvalidExpressionException();

                    double left = Double.Parse(tokens[index - 1]);
                    double right = Double.Parse(tokens[index + 1]);

                    if (token == "*")
                        result = left * right;

                    if (token == "/") {
                        if (right == 0.0)
                            throw new DivideByZeroException();
                        result = left / right;
                    }

                    // Replace [left, operator, right] with [result]
                    tokens[index - 1] = result.ToString();
                    tokens.RemoveAt(index);
                    tokens.RemoveAt(index);

                    index--;
                }
                else
                    index++;
            }
        }

        private double ProcessAddSubstr() //Handle + and -
        {
            double result = Double.Parse(tokens[0]);
            int index = 1;
            while(index < tokens.Count) {
                string oper = tokens[index];
                double right = Double.Parse(tokens[index + 1]);

                if (oper == "+")
                    result += right;
                if (oper == "-")
                    result -= right;
                index += 2; //go to next operator
            }

            return result;
        }
    }
}
