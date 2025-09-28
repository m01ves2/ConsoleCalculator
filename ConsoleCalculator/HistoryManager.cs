using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
    public class HistoryManager
    {
        private List<string> history;

        public HistoryManager()
        {
            history = new List<string>();
        }

        /// <summary>
        ///     Adds item to history
        /// </summary>
        /// <param name="item">History string item</param>
        public void Add(string item)
        {
            history.Add(item);
        }

        /// <summary>
        /// Outputs history of results
        /// </summary>
        public void ShowHistory()
        {
            Console.WriteLine("History:");

            if (history.Count == 0) {
                Console.WriteLine("Empty history.");
            }
            else {
                for (int i = 0; i < history.Count; i++) {
                    Console.WriteLine($"{i + 1}. {history[i]}");
                }
            }
        }
    }
}
