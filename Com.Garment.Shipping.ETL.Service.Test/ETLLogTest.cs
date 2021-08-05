using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Garment.Shipping.ETL.Service.Test
{
    public class ETLLogTest
    {
        private LogingETLModel GenerateModel()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();
            LogingETLModel model = new LogingETLModel(
                rnd.Next(1, 10),
                guid,
                DateTime.Now,
                guid,
                true
            );

            return model;
        }

        [Fact]
        public async Task Function_Manual_ETL()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var logModel = new LogingETLModel(1, "Area", new DateTime(), "Test", true);

            var json = JsonConvert.SerializeObject(logModel);

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            var logger = new Mock<ILogger>();
            var listData = new List<LogingETLModel>();
            listData.Add(GenerateModel());

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Query["page"]).Returns("1");
            request.Setup(x => x.Query["size"]).Returns("10");
            request.Setup(x => x.Body).Returns(ms);
            request.Setup(x => x.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"));

            var mockLogging = new Mock<ILogingETLService>();
            mockLogging.Setup(x => x.Get(1, 10, string.Empty, string.Empty)).ReturnsAsync(listData);
            mockLogging.Setup(x => x.CountAll()).ReturnsAsync(1);

            ETLLog service = new ETLLog(mockLogging.Object);

            var result = await service.Run(request.Object, logger.Object);
            Assert.NotNull(result);
        }
    }
}
