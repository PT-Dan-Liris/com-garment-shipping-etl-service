using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Garment.Shipping.ETL.Service.Test.Services
{
    public class ETLLogTest
    {
        [Fact]
        public async Task ETLLogSuccess()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var logModel = new LogingETLModel(1, "Area", new DateTime(), "Test", true);

            var json = JsonConvert.SerializeObject(logModel);

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            var logger = new Mock<ILogger>();

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Query["name"]).Returns("Test");
            request.Setup(x => x.Body).Returns(ms);
            request.Setup(x => x.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"));
            request.Setup(x => x.Headers["Authorization"]).Returns(new Microsoft.Extensions.Primitives.StringValues("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImRldjIiLCJwcm9maWxlIjp7ImZpcnN0bmFtZSI6IlRlc3QiLCJsYXN0bmFtZSI6IlVuaXQiLCJnZW5kZXIiOiJNIiwiZG9iIjoiMjAxNy0wMi0xN1QxODozNToyMyswNzowMCIsImVtYWlsIjoiZGV2QHVuaXQudGVzdCJ9LCJwZXJtaXNzaW9uIjp7IlBJIjo3LCJQNyI6NywiUDQiOjcsIlA2Ijo3LCJQMyI6NywiUDEiOjcsIkI0Ijo2LCJVVC9VTklULzAxIjo3LCJDOSI6NywiRjEiOjYsIkI5Ijo2LCJBMiI6NywiRjIiOjcsIkIxIjo3fSwiaWF0IjoxNjI2MjI5NzczfQ.QmqENXn8w22scW1-lXDu0KbUI-CHSmc5WRgUmEaT_e4"));

            var mockLogingETL = new Mock<ILogingETLService>();

            ETLLog service = new ETLLog(mockLogingETL.Object);
            await service.Run(request.Object,logger.Object);
        }
    }
}
