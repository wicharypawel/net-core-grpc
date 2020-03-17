using Grpc.Core;
using Helloworld;
using System;
using System.Threading.Tasks;

namespace NetCoreGrpc.ClientStreaming.ConsoleClientApp
{
    public class Program
    {
        public static async Task Main()
        {
            var channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);
            var user = "Pawel";
            var call = client.SayHello();
            foreach (var item in user)
            {
                Console.WriteLine("Sending: " + item.ToString());
                await call.RequestStream.WriteAsync(new HelloRequest { Name = item.ToString() });
            }
            await call.RequestStream.CompleteAsync();
            var result = await call;
            Console.WriteLine("Greeting: " + result.Message);
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
