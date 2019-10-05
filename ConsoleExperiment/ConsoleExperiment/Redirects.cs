using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleExperiment
{
    internal class Redirects
    {
        internal static void Register(CommandLineApplication app)
        {
            app.Command("redirect", cmd =>
            {
                cmd.Description = "attempts to download file after a series of redirects";

                var redirectArgument = cmd.Argument("<URL>", "The initial URL");

                cmd.OnExecute(() => Execute(redirectArgument.Value));
            });

        }

        private static int Execute(string value)
        {
            value = string.IsNullOrWhiteSpace(value) ? "http://www.google.com" : value;
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(value);
            myHttpWebRequest.CookieContainer = new CookieContainer();
            myHttpWebRequest.MaximumAutomaticRedirections = 7;
            myHttpWebRequest.AllowAutoRedirect = true;
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            var guid = Guid.NewGuid();
            var guidString = guid.ToString();
            var docFilePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), guidString);
            using (var responseStream = myHttpWebResponse.GetResponseStream())
            {
                using (var fileStream =
                          new FileStream(Path.Combine(docFilePath), FileMode.Create))
                {
                    responseStream.CopyTo(fileStream);
                }
            }
            return 0;
        }
    }
}
