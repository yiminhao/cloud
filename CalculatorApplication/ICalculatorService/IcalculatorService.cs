using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace ICalculatorService
{
    public interface ICalculatorService: IService
    {
        Task<string> Add(int a, int b);
        Task<string> Subtract(int a, int b);
    }
}
