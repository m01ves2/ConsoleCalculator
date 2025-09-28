using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCalculator.Exceptions;


namespace ConsoleCalculator
{
    internal class Program
    {
        private static HistoryManager historyManager = new HistoryManager();
        static void Main(string[] args)
        {
            

            while (true) {
                ShowMenu();
                string expression = Console.ReadLine();

                if (string.Equals(expression, "Q", StringComparison.OrdinalIgnoreCase)) {
                    Quit();
                    break;
                }

                if (string.Equals(expression, "H", StringComparison.OrdinalIgnoreCase)) {
                    historyManager.ShowHistory();
                    Pause();
                    continue;
                }

                if (string.IsNullOrWhiteSpace(expression)) {
                    Console.WriteLine("Empty expression.");
                    Pause();
                    continue;
                }

                HandleExpression(expression);
            }
        }

        /// <summary>
        /// Shows main menu
        /// </summary>
        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Input math expression");
            Console.WriteLine("(Q - quit, H - Show history):");
            Console.WriteLine("Input: ");
        }

        /// <summary>
        /// Evaluates math expression and writes history. Outputs result
        /// </summary>
        /// <param name="expression">string with math expression</param>
        private static void HandleExpression(string expression)
        {
            try {
                List<string> tokens = ExpressionParser.Parse(expression);
                Calculator calculator = new Calculator(tokens);
                string result = $"{expression} = {calculator.Evaluate()}";

                historyManager.Add(result);

                Console.WriteLine("Result:");
                Console.WriteLine(result);
            }
            catch (CalculatorException e) {
                Console.WriteLine($"Error: {e.Message}");
            }
            catch (Exception e) {
                Console.WriteLine($"Unknown error: {e.Message}");
            }

            Pause();
        }

        /// <summary>
        /// Exprects user's key to continue
        /// </summary>
        private static void Pause()
        {
            Console.WriteLine("\nPress key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Program quits
        /// </summary>
        private static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Quit.");
        }
    }
}
