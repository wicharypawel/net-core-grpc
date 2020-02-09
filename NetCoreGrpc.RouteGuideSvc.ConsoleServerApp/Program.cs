using Grpc.Core;
using NetCoreGrpc.RouteGuideSvc.Proto;
using NetCoreGrpc.RouteGuideSvc.Utils;
using System;
using System.Collections.Generic;

namespace NetCoreGrpc.RouteGuideSvc.ConsoleServerApp
{
    public class Program
    {
        const int Port = 50051;
        private static readonly List<Feature> _featuresSeed = RouteGuideUtil.ParseFeatures(RouteGuideUtil.DefaultFeaturesFile);

        public static void Main()
        {
            var server = new Server
            {
                Services = { RouteGuide.BindService(new RouteGuideImpl(_featuresSeed)) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();
            Console.WriteLine("RouteGuide server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }
}
