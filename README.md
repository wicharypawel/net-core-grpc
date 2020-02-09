# Net Core gRPC

# This repository

This repository shows how to work using Protocol Buffers files and gRPC services generated from proto files. Repository has 4 examples

- Hello world console server + console client
- Hello world `asp.net core` server + console client
- Route guide console server + console client
- Route guide `asp.net core` server + console client

Examples were originally copied from https://github.com/grpc/grpc/tree/master/examples/csharp

## Getting started

1. Download repository 
2. Download .Net SDK (in the moment of writing 3.1.101)
3. Open in VS 2019
4. Setup startup project's using multiple startup & start debugging

## How to generate C# code from proto files

Simply rebuild solution. Proto files will be build by `Grpc.Tools` or `Grpc.AspNetCore` package (depending on example you pick).

## Sources

- https://grpc.io/
- https://grpc.io/about/
- https://grpc.io/docs/
- https://grpc.io/docs/guides/
- https://grpc.io/docs/guides/concepts/
- https://grpc.io/docs/quickstart/csharp/
- https://grpc.io/docs/quickstart/csharp-dotnet/
- https://grpc.io/docs/tutorials/basic/csharp/