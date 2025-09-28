using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Exceptions
{
    internal class InvalidTokenException : CalculatorException
    {
        public InvalidTokenException(string message) : base(message)
        {
        }
    }
}
