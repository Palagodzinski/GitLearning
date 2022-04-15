using Application.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Concrete
{
    public class Calculator
    {
        public ICalculatorEngine _calculatorEngine { get; }
        public Calculator(ICalculatorEngine calculatorEngine)
        {
            _calculatorEngine = calculatorEngine;
        }

        public int Add(int numA, int numB)
        {
            var result = this._calculatorEngine.Add(numA, numB);
            return result;
        }

        public decimal ConvertPLNtoUSD(int num)
        {
            var result = this._calculatorEngine.ConvertFromPLNtoUSD(num);
            return result;
        }
    }
}
