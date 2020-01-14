using CSharpFunctionalExtensions;
using Media.Images.Core.Interfaces;
using Media.Images.Core.ValueObjects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Media.Images.Core.Services
{
    public class ImageProcessor : IImageProcess
    {
        private static int[] SupportedSizes = { 480, 960, 1280 };

        private int SanitizeSize(int value)
        {
            if (value >= 1280) { return 1280; }
            return SupportedSizes.First(size => size >= value);
        }

        public Result<ImageFile> ResizeImage(string dataUri, int desiredHeight, int desiredWidth, string name)
        {
            if (desiredWidth < 0 || desiredHeight < 0)
                return Result.Failure<ImageFile>("Width and height must be > 0");
            if (desiredWidth == 0 && desiredHeight == 0)
                return Result.Failure<ImageFile>("Width and height cannot both be 0");

            if (desiredHeight == 0)
            {
                desiredWidth = SanitizeSize(desiredWidth);
            }
            else
            {
                desiredWidth = 0;
                desiredHeight = SanitizeSize(desiredHeight);
            }
            return Resize(dataUri, desiredHeight, desiredWidth, name);
        }

        private Result<ImageFile> Resize(string dataUri, int height, int width, string name)
        {
            try
            {
                IImageFormat type;
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] fileBytes = Convert.FromBase64String(dataUri.Split(',')[1]);
                    using (Image<Rgba32> img = Image.Load<Rgba32>(fileBytes, out type))
                    {

                        img.Mutate(op => op
                        .Resize(width, height));
                        img.ToBase64String(type);
                        return Result.Success(new ImageFile(img.ToBase64String(type), type, name));
                    }


                }
            }
            catch (Exception ex)
            {

                return Result.Failure<ImageFile>(ex.GetBaseException().Message);
            }


        }
    }
}
