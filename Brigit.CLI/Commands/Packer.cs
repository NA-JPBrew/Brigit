using Spectre.Console;
using System.IO;
using System.IO.Compression;

namespace Brigit.CLI.Commands;

public static class Packer
{
    public static void Run(string? projectDir)
    {
        var targetDir = string.IsNullOrEmpty(projectDir) ? Directory.GetCurrentDirectory() : projectDir;
        var projectName = new DirectoryInfo(targetDir).Name;
        
        AnsiConsole.MarkupLine($"Packing [bold]{projectName}[/]...");

      
        var publishDir = Path.Combine(targetDir, "bin", "Release");
        if (!Directory.Exists(publishDir))
        {
             var potentialPath = Directory.GetDirectories(Path.Combine(targetDir, "bin", "Release")).FirstOrDefault();
             if (potentialPath != null) publishDir = potentialPath;
             else 
             {
                 AnsiConsole.MarkupLine("[red]Error: Build output not found. Please build the project in Release mode first.[/]");
                 return;
             }
        }

        var distDir = Path.Combine(targetDir, "dist");
        Directory.CreateDirectory(distDir);
        
        var ymmePath = Path.Combine(distDir, $"{projectName}.ymme");
        if (File.Exists(ymmePath)) File.Delete(ymmePath);

        ZipFile.CreateFromDirectory(publishDir, ymmePath);
        
        AnsiConsole.MarkupLine($"[green]Successfully packed to {ymmePath}[/]");
    }
}
