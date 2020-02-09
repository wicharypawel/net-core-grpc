using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace NetCoreGrpc.HelloWorld.AspNetCoreServerApp.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0052:Remove unread private members")]
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}