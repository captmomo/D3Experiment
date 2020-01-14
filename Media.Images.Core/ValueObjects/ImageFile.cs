using SixLabors.ImageSharp.Formats;
using System;
using System.Collections.Generic;

namespace Media.Images.Core.ValueObjects
{
    public class ImageFile
    {
        private string _dataURI;
        private IImageFormat _format;
        public IEnumerable<string> GetFileExtensions()
        {
            return _format.FileExtensions;
        }
        public IEnumerable<string> GetMimeTypes()
        {
            return _format.MimeTypes;
        }
        public string GetBase64String()
        {
            return _dataURI.Split(',')[1];
        }
        public string GetDataUri()
        {
            return _dataURI;
        }
        private readonly string _name;

        public string Name => _name;

        public ImageFile(string dataURI, IImageFormat format, string name)
        {
            _dataURI = dataURI ?? throw new ArgumentNullException(nameof(dataURI));
            _format = format ?? throw new ArgumentNullException(nameof(format));
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
