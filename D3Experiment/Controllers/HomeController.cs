using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D3Experiment.Models;
using Newtonsoft.Json;

namespace D3Experiment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet, ActionName("GetJson")]
        public IActionResult Testing()
        {
            var nodelist = new List<NodeDTO>(){
                new NodeDTO { Id = 1, Label = "Supplier", Name = "Supplier ABC", Colour = "red" },
            new NodeDTO { Id = 2, Label = "Resource", Name = "Resource XYZ", Colour = "pink" },
            new NodeDTO { Id = 3, Label = "Resource", Name = "Resource ABC", Colour = "pink" },
            new NodeDTO { Id = 4, Label = "BOM", Name = "BOM #123", Colour = "blue" }
            };
            var linkList = new List<LinkDTO>()
            {
                new LinkDTO{ Source = "Supplier ABC", Target ="Resource XYZ" , Purpose = "SUPPLIES"},
                new LinkDTO{ Source = "Supplier ABC", Target = "Resource ABC", Purpose = "SUPPLIES"},
                new LinkDTO{ Source = "Resource XYZ", Target = "BOM #123", Purpose = "USED IN"},
                new LinkDTO{ Source = "Resource ABC", Target = "BOM #123", Purpose = "USED IN"}
            };

            var dict = new Dictionary<String, dynamic>();
            dict.Add("nodes", nodelist);
            dict.Add("links", linkList);
            return new JsonResult(dict);
        }
    }
}
