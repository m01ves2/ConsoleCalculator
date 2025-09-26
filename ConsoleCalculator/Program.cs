using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Expression:");
            string expression = Console.ReadLine();
            //string expression = "2 + 3 * 5";
            List<string> tokens = ExpressionParser.Parse(expression);

            Calculator calculator = new Calculator(tokens);
            Console.WriteLine(expression + " = " + calculator.Evaluate());
        }
    }
}
