using Cocona;
using System.Text.Json;
using System.IO;
using System.Text.Unicode;
namespace Script;

public class Commands
{
    [Command("init", Description = "Generates Config file")]
    public async Task Init()
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var configPath = Path.Combine(currentDirectory, Constants.CONFIG_FILE_NAME);

        var configFileExists = File.Exists(Path.Combine(currentDirectory, Constants.CONFIG_FILE_NAME));

        if (configFileExists)
        {
            Console.WriteLine("Already Initialized because config file already exist!");
            return;
        }


        var existingConfigContent = await File.ReadAllTextAsync(Path.Combine(currentDirectory, Constants.SAMPLE_CONFIG_FILE_NAME));

        await File.WriteAllTextAsync(configPath, existingConfigContent);

        Console.BackgroundColor = ConsoleColor.Green;
        Console.WriteLine("Configuration Successfull :)");

        Console.BackgroundColor = ConsoleColor.Black;
    }

    [Command("merge", Description = "Merges Content of File According to configuration specified")]
    public async Task Merge()
    {
        // var config = JsonSerializer.Deserialize<Config>();

        var currentDirectory = Directory.GetCurrentDirectory();

        var configPath = Path.Combine(currentDirectory, Constants.CONFIG_FILE_NAME);

        var isConfigFileExists = File.Exists(configPath);

        if (!isConfigFileExists)
        {
            Console.WriteLine("No config File found, Please use command \"init;\"");
            return;
        }

        var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(configPath));

        if (config == null)
        {
            Console.WriteLine("Config Not Well Defined");
            return;
        }


        // VALIDATION
        if (config.Files == null || config.Files.Count == 0)
        {
            Console.WriteLine("Please define files in config section");
            return;
        }

        if (string.IsNullOrEmpty(config.GeneratedFileName))
        {
            Console.WriteLine("Please provide a valid File Name in config. Use \"generatedFileName\"");
            return;
        }


        using var stream = File.CreateText(Path.Combine(currentDirectory, config.GeneratedFileName));
        foreach (var file in config.Files.OrderBy(x => x.Index))
        {
            var fileContent = await File.ReadAllTextAsync(Path.Combine(currentDirectory, file.FileName));

            await stream.WriteLineAsync(fileContent);
            await stream.WriteLineAsync(" ");
        }

    }
}
