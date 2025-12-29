using System.CommandLine;
using System.CommandLine.Parsing;
using Spectre.Console;
using Brigit.CLI.Commands;

namespace Brigit.CLI;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Brigit CLI Tool for AviUtl2/AviUtl/YMM4 Plugin Development");

        var createCommand = new Command("create", "Create a new Brigit project");
        var nameOption = new Option<string>("--name", "Project name (Non-interactive mode)");
        var ymm4PathOption = new Option<string>("--ymm4", "YMM4 Installation Path");
        createCommand.AddOption(nameOption);
        createCommand.AddOption(ymm4PathOption);
        
        createCommand.SetHandler(async (string? name, string? ymm4) =>
        {
            if (!string.IsNullOrEmpty(name))
            {
                 // Defaulting or requiring YMM4 path
                 var path = ymm4 ?? @"C:\YMM4"; // Fallback for dev/test
                 Scaffolder.GenerateProject(name, "Visual Studio", new List<string> { "AviUtl", "AviUtl2", "YMM4" }, path);
            }
            else 
            {
                await Scaffolder.RunAsync();
            }
        }, nameOption, ymm4PathOption);
        rootCommand.AddCommand(createCommand);

        var templateCommand = new Command("template", "Add sample templates to the project");
        templateCommand.SetHandler(async () =>
        {
             await Scaffolder.AddTemplateAsync();
        });
        rootCommand.AddCommand(templateCommand);

        var packCommand = new Command("pack", "Pack the project into a .ymme file");
        packCommand.SetHandler((string? path) =>
        {
             Packer.Run(path);
        }, new Option<string>("--path", "Path to the project directory"));
        rootCommand.AddCommand(packCommand);
        
        var buildCommand = new Command("build", "Build the project");
        buildCommand.SetHandler(() =>
        {
            Builder.Run();
        });
        rootCommand.AddCommand(buildCommand);

        return await rootCommand.InvokeAsync(args);
    }
}
