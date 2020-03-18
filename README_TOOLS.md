## Tools

### Generate code from proto files (command line)

source: https://stackoverflow.com/questions/50687335/how-to-use-the-grpc-tools-to-generate-code (Jon Skeet)

Steps for installation of protoc compiler with gRPC plugin
1. Install the protoc compiler,
  - open https://github.com/protocolbuffers/protobuf/releases
  - download a zip pack suitable for your operating system
  - unzip the zip and open the `bin` folder
  - move the `protoc` file so that it is available in the system path (PATH)
2. (Optional) Install additional Google types,
  - Open the `include` folder located in the package
  - Move the `google` folder to a folder where *.proto files are defined
3. Download `grpc_csharp_plugin.exe`. The easiest way to download an add-on is to install the NuGet `Grpc.Tools` package for any project and then search for the file in the NuGet system cache `.nuget\packages\grpc.tools\2.27.0\tools`.
4. Create `tools` folder 
5. Move `grpc_csharp_plugin.exe` into `tools`

Using protoc compiler with gRPC plugin:

1. Define the `helloworld.proto` file that __contains protocol buffer messages and grpc service__,
2. Create a folder named `out`,
3. Call the command,
```
protoc --proto_path=. --csharp_out=.\out --grpc_out=.\out --plugin=protoc-gen-grpc=.\tools\grpc_csharp_plugin.exe helloworld.proto
```

- (optional) change parameter `--proto_path=.\path\to\imported\proto\files` (default's to current directory)

Calling the command will create the `Helloworld.cs` and `HelloworldGrpc.cs` files, the first file contains an implementation of the message types, and the second one a service implementation.

__NOTE: in order to generate code for different language replace `--csharp_out` argument and plugin__
 
__NOTE: protoc ignores system PATH when looking for plugins__

### Generate code from proto files (Configuration for VS Code)

Install `vscode-proto3` extension in order to get first class experience when working with *.proto files. 

Features:
- code snippets,
- code completion,
- vscode commands for compiling files,
- on-save compilation for *.proto files,

Steps:
1. Install the `protoc` compiler if not installed already
2. Install `vscode-proto3` via VS Code Extensions Marketplace
3. Restart VS Code
4. Download `grpc_csharp_plugin.exe` and copy it to `tools` folder in main directory,
5. Create a folder named `.vscode` in the main repository folder
6. Create a configuration file named `settings.json` inside the newly created folder

```
{
    "protoc": {
        "compile_on_save": true,
        "options": [
            "--proto_path=.",
            "--csharp_out=out",
            "--grpc_out=out ",
            "--plugin=protoc-gen-grpc=tools/grpc_csharp_plugin.exe"
        ]
    }
}
```

__NOTE: adjust directories for your setup__

7. Open and save any file with the *.proto extension. The result should appear in the folder named `out`. In case of compilation errors a popup notification will be displayed.

### Generate code from proto files (Configuration for .NET, .NET Core and Visual Studio)

The `Grpc.Tools` NuGet package contains the `protoc` and `grpc_csharp_plugin.exe` C# plugin binaries needed to generate the code. The package also integrates with MSBuild to provide automatic C# code generation from .proto files. In other words, protocol buffers files (*.proto) can be compiled together with *.cs as part of a consistent application building mechanism. All available cross-platform with the minimum configuration using a single NuGet package.

The build regenerates the files under the obj/Debug/TARGET_FRAMEWORK directory.

1. Create a new .NET Standard project
2. Add the `helloworld.proto` file
3. Install a NuGet package called `Google.Protobuf` - a package containing definitions used by protocol buffers
4. Install NuGet package named `Grpc` - gRPC meta package,
5. Install NuGet package named `Grpc.Tools` - compilation tools for gRPC,
6. Include *.proto file to the compilation by modifying the *.csproj file (see example below)

```
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>netstandard2.1</TargetFramework>
    </PropertyGroup>
    
    <ItemGroup>
        <PackageReference Include="Google.Protobuf" Version="3.11.3" />
        <PackageReference Include="Grpc" Version="2.27.0" />
        <PackageReference Include="Grpc.Tools" Version="2.27.0" PrivateAssets="all" />
    </ItemGroup>
    
    <ItemGroup>
        <Protobuf Include="helloworld.proto" />
    </ItemGroup>
</Project>
```

Building the project should generate language native protocol buffers files in `.\objobug\netstandard2.1`, so they will be available at any time during the programmer's work. The advantages of this are support for intellisense, static compilation and strong typing. The code will be updated transparently every time the `helloworld.proto` file is changed. 

__NOTE: when working with Grpc.AspNetCore package it is not required to reference Grpc.Tools since it is going to be referenced as dependency__

Read for more → https://github.com/grpc/grpc/blob/master/src/csharp/BUILD-INTEGRATION.md