using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleExperiment
{
    internal class ClipboardPaste
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("paste", cmd =>
            {
                cmd.Description = "Paste what you type to cursor location after 10 seconds";

                var pasteTextArgument = cmd.Argument("<PASTETEXT>", "The text to paste");

                cmd.OnExecute(() => Execute(pasteTextArgument.Value));
            });

        }

        public static int Execute(string pasteText)
        {
            pasteText = string.IsNullOrWhiteSpace(pasteText) ? "Sample text" : pasteText;

            Console.WriteLine($"Pasting {pasteText}!");
            Utils.WindowsClipboard.SetText(pasteText);
            for (int a = 10; a >= 0; a--)
            {
                Console.Write($"\rPasting text in {a} seconds");
                System.Threading.Thread.Sleep(1000);
            }
            Utils.WindowsClipboard.SendKeys();

            return 0;
        }
    }
}
