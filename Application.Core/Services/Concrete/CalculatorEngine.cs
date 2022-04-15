using Application.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Concrete
{
    public class CalculatorEngine : ICalculatorEngine
    {
        public int Add(int num1, int num2)
        {
            return num1 + num2;
        }

        public decimal ConvertFromPLNtoUSD(int value)
        {
            decimal usd = 4.27m;
            return (usd * value);
        }

        public int Division(int num1, int num2)
        {
            throw new NotImplementedException();
        }

        public int Subtract(int num1, int num2)
        {
            throw new NotImplementedException();
        }
    }
}
