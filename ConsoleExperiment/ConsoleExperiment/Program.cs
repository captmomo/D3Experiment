using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;



namespace ConsoleExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("--debug"))
            {
                Console.WriteLine($"Ready for debugger to attach. Process ID: {Process.GetCurrentProcess().Id}");
                Console.Write("Press ENTER to Continue");
                Console.ReadLine();
                args = args.Except(new[] { "--debug" }).ToArray();
            }

            var app = new CommandLineApplication();
            app.FullName = "Console Experiments";
            app.Description = "Console application for testing stuff";

            ClipboardPaste.Register(app);
            Redirects.Register(app);
            ImageResize.Register(app);
            Encryption.Register(app);
            ConfigEncrypt.Register(app);

            app.Command("help", cmd =>
            {
                cmd.Description = "Get help for the application, or a specific command";
                var commandArgument = cmd.Argument("<COMMAND>", "The command to get help for");
                cmd.OnExecute(() =>
                {
                    app.ShowHelp(commandArgument.Value);
                    return 0;
                });
            });

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });

           
            app.Execute(args);

        }
    }
}
