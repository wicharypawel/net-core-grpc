using Grpc.Core;
using Helloworld;
using System;
using System.Threading.Tasks;

namespace NetCoreGrpc.ServerStreaming.ConsoleClientApp
{
    public class Program
    {
        public static async Task Main()
        {
            var channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);
            var user = "Pawel";
            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: ");
            while (await reply.ResponseStream.MoveNext())
            {
                Console.WriteLine(reply.ResponseStream.Current);
            }
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
