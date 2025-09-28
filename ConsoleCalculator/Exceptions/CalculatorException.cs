using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Exceptions
{
    public abstract class CalculatorException : Exception
    {
        protected CalculatorException(string message) : base(message) { 
        }
    }
}
