using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidCases.Models
{
    public class ReportFile
    { 
        public string Name { get; set; }
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
    }
}