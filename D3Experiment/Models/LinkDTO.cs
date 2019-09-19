using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D3Experiment.Models
{
    public class LinkDTO
    {
       public string Source { get; set; }
        public string Target { get; set; }

        public string Purpose { get; internal set; }
    }
}
