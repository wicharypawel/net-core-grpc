using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;
using Microsoft.Extensions.Logging;

namespace NetCoreGrpc.BidirectionalStreaming.AspNetCoreServerApp.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task SayHello(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = "Hello " + requestStream.Current
                });
            }
        }
    }
}
