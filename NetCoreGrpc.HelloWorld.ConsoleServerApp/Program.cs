﻿using System;
using Grpc.Core;
using Helloworld;

namespace NetCoreGrpc.HelloWorld.ConsoleServerApp
{
    public class Program
    {
        const int Port = 5000;

        public static void Main()
        {
            var server = new Server
            {
                Services = { Greeter.BindService(new GreeterService()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();
            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }
}
