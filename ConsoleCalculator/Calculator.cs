using ConsoleCalculator.Exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    /// <summary>
    /// Provides basic math operations for the calculator.
    /// </summary>
    /// 
    public class Calculator
    {
        private readonly List<string> tokens;
        private readonly Dictionary<string, int> priorities = new Dictionary<string, int>
        {
            ["+"] = 1,
            ["-"] = 1,
            ["*"] = 2,
            ["/"] = 2,
        };

        public Calculator(List<string> tokens)
        {
            this.tokens = new List<string>(tokens); ; // Example: ["2", "+", "3", "*", "5"]
        }

        /// <summary>
        /// evaluates math expression
        /// </summary>
        /// <returns>The result of expression</returns>
        public double Evaluate()
        {
            double result = 0;
            //ProcessMulDiv();
            //result = ProcessAddSub();
            List<string> tokensRPN = ShuntingYard();
            result = EvaluateRPN(tokensRPN);

            return result;
        }


        private List<string> ShuntingYard()
        {
            List<string> tokensRPN = new List<string>();
            Stack<string> stack = new Stack<string>();
            Stack<bool> operandsInsideParentheses = new Stack<bool>();

            foreach (string token in tokens) {

                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double value)) {
                    tokensRPN.Add(value.ToString(CultureInfo.InvariantCulture));
                    
                    var tempStack = new Stack<bool>();
                    while(operandsInsideParentheses.Count > 0) {
                        tempStack.Push(true);
                        operandsInsideParentheses.Pop();
                    }
                    while (tempStack.Count > 0)
                        operandsInsideParentheses.Push(tempStack.Pop());
                }
                else
                if (token == "(") {
                    stack.Push(token);
                    operandsInsideParentheses.Push(false); // a new block of "(", no numbers inside
                }
                else
                if (token == ")") {
                    if (operandsInsideParentheses.Count == 0 || operandsInsideParentheses.Pop() == false)
                        throw new InvalidTokenException("Empty parentheses");

                    while (stack.Count > 0 && stack.Peek() != "(")
                        tokensRPN.Add(stack.Pop());

                    if (stack.Count == 0 || stack.Pop() != "(")
                        throw new InvalidTokenException("Mismatched parentheses");
                }
                else
                if (token == "+" || token == "-" || token == "*" || token == "/") {
                    while (stack.Count > 0 && stack.Peek() != "(" && priorities[stack.Peek()] >= priorities[token]) {
                        tokensRPN.Add(stack.Pop());
                    }
                    stack.Push(token);
                }
                else {
                    throw new InvalidTokenException($"Unknown token: {token}");
                }
            }

            while (stack.Count > 0) { //Let's check that every open parenthes has closed parenthes
                var op = stack.Pop();
                if (op == "(")
                    throw new InvalidTokenException("Mismatched parentheses. Too many '('");
                tokensRPN.Add(op);
            }

            return tokensRPN;
        }

        private double EvaluateRPN(List<string> tokensRPN)
        {
            Stack<double> stack = new Stack<double>();

            for (int i = 0; i < tokensRPN.Count; i++) {
                if (double.TryParse(tokensRPN[i], NumberStyles.Any, CultureInfo.InvariantCulture, out double value)) {
                    stack.Push(value);
                }
                else {
                    if (stack.Count > 1) {
                        double right = stack.Pop();
                        double left = stack.Pop();
                        stack.Push( Calc(left, tokensRPN[i], right) );
                    }
                    else {
                        throw new InvalidTokenException("Wrong expression. Not Enough operands");
                    }
                }
            }

            if (stack.Count != 1)
                throw new InvalidTokenException("Wrong expression. Extra operands or operators");

            return stack.Pop();
        }

        private double Calc(double left, string op, double right)
        {
            double result = 0;
            switch (op) {
                case "+": result = left + right; break;
                case "-": result = left - right; break;
                case "*": result = left * right; break;
                case "/":
                    if (right == 0)
                        throw new DivideByZeroCalculatorException("Division by zero");
                    result = left / right; 
                    break;
                default: throw new InvalidTokenException("Invalid operation");
            }
            return result;
        }
    }
}
