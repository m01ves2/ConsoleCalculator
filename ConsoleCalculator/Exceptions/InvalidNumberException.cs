using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Exceptions
{
    internal class InvalidNumberException : CalculatorException
    {
        public InvalidNumberException(string message) : base(message) { }
    }
}
