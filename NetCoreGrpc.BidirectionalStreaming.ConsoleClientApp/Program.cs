using Grpc.Core;
using Helloworld;
using System;
using System.Threading.Tasks;

namespace NetCoreGrpc.BidirectionalStreaming.ConsoleClientApp
{
    public class Program
    {
        public static async Task Main()
        {
            var channel = new Channel("127.0.0.1:5000", ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);
            var users = new string[] { "Pawel", "Joh", "Emma" , "David", "Alex" };
            var call = client.SayHello();
            var listener = Task.Run(async () =>
            {
                while(await call.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Greeting: " + call.ResponseStream.Current);
                }
            });
            foreach (var item in users)
            {
                Console.WriteLine("Sending: " + item);
                await call.RequestStream.WriteAsync(new HelloRequest() { Name = item });
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }
            await call.RequestStream.CompleteAsync();
            await listener;
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
