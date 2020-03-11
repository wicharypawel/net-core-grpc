using System;
using Grpc.Core;
using Helloworld;

namespace NetCoreGrpc.HelloWorld.ConsoleClientApp
{
    public class Program
    {
        public static void Main()
        {
            var channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);
            var user = "Pawel";
            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
