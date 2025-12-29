using System;
using System.Diagnostics;
using System.IO;
using Spectre.Console;

namespace Brigit.CLI.Commands
{
    public static class Builder
    {
        public static void Run()
        {
            try 
            {
                var projectFile = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj").FirstOrDefault();
                if (projectFile == null)
                {
                    AnsiConsole.MarkupLine("[red]Error: No .csproj file found in the current directory.[/]");
                    return;
                }

                var projectName = Path.GetFileNameWithoutExtension(projectFile);

                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots)
                    .Start("Building for YMM4...", ctx =>
                    {
                        try 
                        {
                            RunProcess("dotnet", "build -c Release");
                            AnsiConsole.MarkupLine("[green]YMM4 Build Complete![/]");
                            
                            ctx.Status("Building for AviUtl (NativeAOT)...");
                            RunProcess("dotnet", "publish -c Release -r win-x64 /p:NativeLib=Shared /p:SelfContained=true");
                            
                            var publishDir = Path.Combine(Directory.GetCurrentDirectory(), "bin", "Release", "net10.0", "win-x64", "publish");
                            var aufPath = Path.Combine(publishDir, $"{projectName}.auf");
                            var dllPath = Path.Combine(publishDir, $"{projectName}.dll");

                            if (File.Exists(dllPath))
                            {
                                if (File.Exists(aufPath)) File.Delete(aufPath);
                                File.Move(dllPath, aufPath);
                            }
                            
                            AnsiConsole.MarkupLine("[green]AviUtl Build Complete![/]");
                        }
                        catch (Exception ex)
                        {
                             throw new Exception($"Build process failed: {ex.Message}");
                        }
                    });

                AnsiConsole.MarkupLine($"[bold]Build Artifacts:[/]");
                AnsiConsole.MarkupLine($"YMM4:   [blue]bin/Release/net10.0/{projectName}.dll[/]");
                AnsiConsole.MarkupLine($"AviUtl: [blue]bin/Release/net10.0/win-x64/publish/{projectName}.auf[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Fatal Error during build:[/]");
                AnsiConsole.WriteException(ex);
            }
        }

        private static void RunProcess(string fileName, string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null) throw new Exception($"Failed to start process: {fileName}");

            var errorTask = process.StandardError.ReadToEndAsync();
            var outputTask = process.StandardOutput.ReadToEndAsync();
            
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var error = errorTask.Result;
                var outText = outputTask.Result;
                throw new Exception($"Command '{fileName} {arguments}' failed.\nError: {error}\nOutput: {outText}");
            }
        }
    }
}
