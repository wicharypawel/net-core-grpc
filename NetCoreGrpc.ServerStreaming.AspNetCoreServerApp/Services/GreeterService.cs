using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;
using Microsoft.Extensions.Logging;

namespace NetCoreGrpc.ServerStreaming.AspNetCoreServerApp.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            var name = request.Name;
            var message = "Hello " + name;
            foreach (var item in message)
            {
                await responseStream.WriteAsync(new HelloReply() { 
                    Message = item.ToString()
                });
            }
        }
    }
}
