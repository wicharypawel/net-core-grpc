using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace NetCoreGrpc.HelloWorld.ConsoleServerApp
{
    internal class GreeterGrpcService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
