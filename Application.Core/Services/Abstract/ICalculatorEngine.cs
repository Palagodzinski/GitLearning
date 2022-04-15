using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Services.Abstract
{
    public interface ICalculatorEngine
    {
        int Add(int num1, int num2);

        int Subtract(int num1, int num2);

        int Division(int num1, int num2);

        decimal ConvertFromPLNtoUSD(int value);
    }
}
