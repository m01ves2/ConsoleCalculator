using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Exceptions
{
    public class DivideByZeroCalculatorException : CalculatorException
    {
        public DivideByZeroCalculatorException(string message) : base(message)
        {
        }
    }
}
