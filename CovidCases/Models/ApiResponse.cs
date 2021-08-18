using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidCases.Models
{
    public class ApiResponse
    {
        public List<ApiData> data { get; set; }


    }
    public class ApiData
    {
        public DateTime date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public int recovered_diff { get; set; }
        public DateTime last_update { get; set; }
        public int active { get; set; }
        public int active_diff { get; set; }
        public decimal fatality_rate { get; set; }
        public ApiRegion region { get; set; }


    }

    public class ApiRegion
    {
        public string iso { get; set; }
        public string name { get; set; }

        public string province { get; set; }
        public string lat { get; set; }
        public List<ApiCity> cities { get; set; }

    }

    public class ApiCity
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public int? fips { get; set; }
        public string lat { get; set; }
        public int? confirmed { get; set; }
        public int? deaths { get; set; }
        public int? confirmed_diff { get; set; }
        public int? deaths_diff { get; set; }
        public DateTime last_update { get; set; }
    }
}