using Microsoft.Extensions.CommandLineUtils;
using System;
using ConsoleExperiment.Utils;
namespace ConsoleExperiment
{
    internal class ConfigEncrypt
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("config", cmd =>
            {
                cmd.Description = "Enter path to save config file";

                CommandArgument fileArgument = cmd.Argument("<CONFIG>", "Path to save config file", true);

                CommandOption keyOption = cmd.Option("-k|--key <KEY>",
                    "Name of key",
                    CommandOptionType.SingleValue);

                CommandOption valueOption = cmd.Option("-v|--value <value>",
                    "Value for key",
                    CommandOptionType.SingleValue);
                cmd.OnExecute(() => Execute(fileArgument.Value, keyOption.Value(), valueOption.Value()));

            });

        }

        public static int Execute(string filePath, string key, string value)
        {
            filePath = string.IsNullOrWhiteSpace(filePath) ? @"data\temp" : filePath;

            bool fileInfo = System.IO.Directory.Exists(filePath);
            if (!fileInfo)
            {
                Console.WriteLine($"Folder does not exist");
            }
            string configKey = string.IsNullOrWhiteSpace(key) ? "key" : key;
            string configValue = string.IsNullOrWhiteSpace(value) ? "value" : value;

            var configManager = new ConfigManager().WithCurrentUserScope().Set(configKey, configValue).AtFolder(filePath).Save();
            return 0;
        }
    }
}
