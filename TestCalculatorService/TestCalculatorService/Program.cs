using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ICalculatorService;
using Microsoft.ServiceFabric.Services.Remoting.Client;


namespace TestCalculatorService
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var calculatorClient = ServiceProxy.Create<ICalculatorService.ICalculatorService>(new Uri("fabric:/CalculatorApplication/CalculatorService"));
                var result = calculatorClient.Add(1, 2).Result;
                Console.WriteLine(result);
                Thread.Sleep(3000);
            }
           
        }
    }
}
