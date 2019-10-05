using Microsoft.Extensions.CommandLineUtils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace ConsoleExperiment
{
    internal class ImageResize
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("resize", cmd =>
            {
                cmd.Description = "Enter path to image";

                CommandArgument resizeArgument = cmd.Argument("<RESIZE>", "Image to be resized", true);

                CommandOption nameOption = cmd.Option("-n|--name <NAME>",
                    "Name of Output file without extension",
                    CommandOptionType.SingleValue);

                CommandOption heightOption = cmd.Option("-h|--height <HEIGHT>",
                    "Height of Output file",
                    CommandOptionType.SingleValue);
                cmd.OnExecute(() => Execute(resizeArgument.Value, nameOption.Value(), heightOption.Value()));

            });

        }

        public static int Execute(string filePath, string name, string height)
        {
            filePath = string.IsNullOrWhiteSpace(filePath) ? "C:\\Users\\ads\\source\\repos\\ConsoleExperiment\\ConsoleExperiment\\weiyang.lee.jpeg" : filePath;

            bool fileInfo = System.IO.File.Exists(filePath);
            if (!fileInfo)
            {
                Console.WriteLine($"File does not exist");
            }
            string ext = string.IsNullOrWhiteSpace(Path.GetExtension(filePath)) ? ".png" : Path.GetExtension(filePath);
            string outFile = string.IsNullOrWhiteSpace(name) ? "test" : name;
            int outHeight = 50;
            if (!string.IsNullOrWhiteSpace(height))
            {
                int.TryParse(height, out outHeight);
            }

            ImageFormatManager format = new SixLabors.ImageSharp.Formats.ImageFormatManager();
            IImageFormat type;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Image<Rgba32> img = Image.Load(filePath, out type))
                {

                    img.Mutate(op => op
                    .Resize(0, outHeight));
                    img.Save(ms, type);

                }
                using (Image<Rgba32> img = Image.Load(ms.ToArray(), out type))
                {
                    string hexString = img.ToBase64String(type);
                    byte[] fileBytes = Convert.FromBase64String(hexString.Split(",")[1]);
                    Image<Rgba32> outImg = Image.Load(fileBytes, out type);
                    outImg.Save($"{outFile}{ext}");
                }
                Console.WriteLine(type.ToString());
            }


            return 0;
        }
    }
}
