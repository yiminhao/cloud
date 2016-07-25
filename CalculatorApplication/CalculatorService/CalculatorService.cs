using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;


namespace CalculatorService
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class CalculatorService : StatelessService, ICalculatorService.ICalculatorService
    {
        public CalculatorService(StatelessServiceContext context)
            : base(context)
        { }

        public Task<string> Add(int a, int b)
        {
            return Task.FromResult<string>(string.Format("Instance {0} returns:{1}", this.Partition.PartitionInfo.Id, a+b));
        }

        public Task<string> Subtract(int a, int b)
        {
            return Task.FromResult<string>(string.Format("Instance {0} returns:{1}", this.Partition.PartitionInfo.Id, a - b));
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        /// *********** Default Communication Stack, out-of-box communication stack based on RPC Proxy
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            //return new ServiceInstanceListener[0];

            return new[]
            {
                new ServiceInstanceListener(initParams => new FabricTransportServiceRemotingListener(initParams, this)),
            };

        }





        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            //       or remove this RunAsync override if it's not needed in your service.

            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                ServiceEventSource.Current.ServiceMessage(this, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }

 
    }
}
