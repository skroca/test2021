using System.Threading.Tasks;
using System.Web.Mvc;
using CovidCases.Enums;
using CovidCases.Models;

namespace CovidCases.Controllers
{
    public class ReportController : Controller
    {
        public async Task<ActionResult> Index(string region= null)
        {
            ReportViewModel report = new ReportViewModel();
            await report.GetRegionsReport();
            TempData["report"] = report;
            return View(report);
        }
        [HttpPost]
        public async Task<ActionResult> Index(ReportViewModel reportResponse)
        {
            var report = new ReportViewModel();

            if (TempData.ContainsKey("report"))
                report = (ReportViewModel)TempData["report"];

            await reportResponse.GetProvincesReport(reportResponse.SelectedRegion);
            reportResponse.Regions = report.Regions;
            TempData["report"] = reportResponse;

            return View(reportResponse);
        }

        [HttpGet]
        [Route("Report/GenerateReport/{fileType}")]
        public FileContentResult GenerateReport(FileType fileType)
        {
            var report = new ReportViewModel();

            if (TempData.ContainsKey("report"))
                report = (ReportViewModel)TempData["report"];

            TempData["report"] = report;

            switch (fileType)
            {
                case FileType.Xml:
                    return File(new System.Text.UTF8Encoding().GetBytes(report.GenerateXml()), "text/xml", "report2021.xml");
                case FileType.Json:
                    return File(new System.Text.UTF8Encoding().GetBytes(report.GenerateJson()), "text/json", "report2021.json");
                case FileType.CSV:
                    return File(new System.Text.UTF8Encoding().GetBytes(report.GenerateCsv()), "text/csv", "report2021.csv");
            }

            return null;
        }

    }
}