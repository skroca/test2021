using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CovidCases.Controllers;
using System.Web.Mvc;
using CovidCases.Enums;
using CovidCases.Models;

namespace CovidCasesTests
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public async Task IndexRegionTest()
        {
            ReportController report = new ReportController();
            var result = await report.Index() as ViewResult;
            Assert.IsNotNull(result.Model);
        }
        [TestMethod]
        public async Task IndexProvincesTest()
        {
            ReportViewModel data = new ReportViewModel();
            data.SelectedRegion = "US";
            ReportController report = new ReportController();
            var result = await report.Index(data) as ViewResult;
            Assert.IsNotNull(result.Model);
        }
        [TestMethod]
        public async Task GenerateCsvFile()
        {
            ReportViewModel data = new ReportViewModel();
            data.SelectedRegion = "US";
            ReportController report = new ReportController();
            await report.Index(data);
            var result = report.GenerateReport(FileType.CSV) as FileContentResult;

            Assert.IsNotNull(result.ContentType);
            Assert.AreEqual("text/csv", result.ContentType);
        }
        [TestMethod]
        public async Task GenerateXmlFile()
        {
            ReportViewModel data = new ReportViewModel();
            data.SelectedRegion = "US";
            ReportController report = new ReportController();
            await report.Index(data);
            var result = report.GenerateReport(FileType.Xml) as FileContentResult;

            Assert.IsNotNull(result.ContentType);
            Assert.AreEqual("text/xml", result.ContentType);
        }
        [TestMethod]
        public async Task GenerateJsonFile()
        {
            ReportViewModel data = new ReportViewModel();
            data.SelectedRegion = "US";
            ReportController report = new ReportController();
            await report.Index(data);
            var result = report.GenerateReport(FileType.Json) as FileContentResult;
            Assert.IsNotNull(result.ContentType);
            Assert.AreEqual("text/json", result.ContentType);
        }
    }
}
