using CSharpFunctionalExtensions;
using Media.Images.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Media.Images.Core.Interfaces
{
    public interface IImageProcess
    {
        /// <summary>
        /// Expects input of data uri example: 
        /// data:image/png;base64,XXXXXXXXXXXXXX
        /// </summary>
        /// <param name="dataUri"></param>
        /// <returns>
        /// Data uri of resized image
        /// </returns>
        Result<ImageFile> ResizeImage(string dataUri, int desiredHeight, int desiredWidth, string name);
    }
}
