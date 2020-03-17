using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;
using Microsoft.Extensions.Logging;

namespace NetCoreGrpc.ClientStreaming.AspNetCoreServerApp.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<HelloReply> SayHello(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            string buffor = string.Empty;
            while(await requestStream.MoveNext())
            {
                buffor += requestStream.Current.Name;
            }
            return new HelloReply
            {
                Message = "Hello " + buffor
            };
        }
    }
}
