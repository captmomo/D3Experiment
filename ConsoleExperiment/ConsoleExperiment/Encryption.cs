using Microsoft.Extensions.CommandLineUtils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using LoginCryptography;
using System;

namespace ConsoleExperiment
{
    internal class Encryption
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("encrypt", cmd =>
            {
                cmd.Description = "Enter string to encrypt";

                CommandArgument stringArg = cmd.Argument("<STRING>", "String to be encrypted", true);

                cmd.OnExecute(() => Execute(stringArg.Value));

            });

        }

        public static int Execute(string inString)
        {
            inString = string.IsNullOrWhiteSpace(inString) ? "Sample" : inString;
            Console.WriteLine($"Encrypting...");
            var outString = Cryptography.Encrypt(inString);
            inString = Cryptography.Decrypt(inString);
            if (string.IsNullOrWhiteSpace(outString))
            {
                Console.Write("FAILED");
            }
            else
            {
                Console.WriteLine($"{outString}");
            }
 

            return 0;
        }
    }
}
