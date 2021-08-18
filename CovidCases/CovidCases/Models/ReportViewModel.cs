using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using CovidCases.ApiService;
using Newtonsoft.Json;

namespace CovidCases.Models
{
    public class ReportViewModel
    {
        public List<ApiData> Data { get; set; }
        public List<string> Regions { get; set; }
        public string SelectedRegion { get; set; }
        public bool FilterByRegion { get; set; }
        public async Task GetRegionsReport()
        {
            var api = new ThirdPartyApi();
            string url = WebConfigurationManager.AppSettings["ApiCovidUrl"] + "reports";
            
            var apiCovidResponse =
                JsonConvert.DeserializeObject<ApiResponse>(await Task.Run(async () => await api.SendToThirdPartyApi(url)));
            Data = apiCovidResponse.data.OrderByDescending(x => x.confirmed).Take(10).ToList();
            Regions = Data.Select(x => x.region.name).ToList();
        }
        public async Task GetProvincesReport(string region)
        {
            var api = new ThirdPartyApi();
            string url = WebConfigurationManager.AppSettings["ApiCovidUrl"] + "reports?region_name=" + region;

            var apiCovidResponse =
                JsonConvert.DeserializeObject<ApiResponse>(await Task.Run(async () => await api.SendToThirdPartyApi(url)));
            Data = apiCovidResponse.data.OrderByDescending(x => x.confirmed).Take(10).ToList();
            Regions = Data.Select(x => x.region.name).ToList();
            FilterByRegion = true;
        }
        public string GenerateXml()
        { 
            List<ReportFile> reportFile = new List<ReportFile>();
            foreach (var record in Data)
            {
                reportFile.Add(new ReportFile(){Confirmed = record.confirmed,Deaths = record.deaths, Name = FilterByRegion ? record.region.province : record.region.name});
            }
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(reportFile.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, reportFile);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        public string GenerateJson()
        {
            List<ReportFile> reportFile = new List<ReportFile>();
            foreach (var record in Data)
            {
                reportFile.Add(new ReportFile() { Confirmed = record.confirmed, Deaths = record.deaths, Name = FilterByRegion ? record.region.province : record.region.name });
            }
            return JsonConvert.SerializeObject(reportFile.ToArray());
        }
        public string GenerateCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Name");
            sb.Append(",");
            sb.Append("Cases");
            sb.Append(",");
            sb.Append("Deaths");
            sb.AppendLine();
            foreach (var record in Data)
            {
                sb.Append(FilterByRegion ? record.region.province : record.region.name);
                sb.Append(",");
                sb.Append(record.confirmed);
                sb.Append(",");
                sb.Append(record.deaths);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}