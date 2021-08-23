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
    public class ManualETLTest
    {
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

            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Query["name"]).Returns("Test");
            request.Setup(x => x.Body).Returns(ms);            
            request.Setup(x => x.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*"));
            request.Setup(x => x.Headers["Authorization"]).Returns(new Microsoft.Extensions.Primitives.StringValues("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImRldjIiLCJwcm9maWxlIjp7ImZpcnN0bmFtZSI6IlRlc3QiLCJsYXN0bmFtZSI6IlVuaXQiLCJnZW5kZXIiOiJNIiwiZG9iIjoiMjAxNy0wMi0xN1QxODozNToyMyswNzowMCIsImVtYWlsIjoiZGV2QHVuaXQudGVzdCJ9LCJwZXJtaXNzaW9uIjp7IlBJIjo3LCJQNyI6NywiUDQiOjcsIlA2Ijo3LCJQMyI6NywiUDEiOjcsIkI0Ijo2LCJVVC9VTklULzAxIjo3LCJDOSI6NywiRjEiOjYsIkI5Ijo2LCJBMiI6NywiRjIiOjcsIkIxIjo3fSwiaWF0IjoxNjI2MjI5NzczfQ.QmqENXn8w22scW1-lXDu0KbUI-CHSmc5WRgUmEaT_e4"));

            var tokenPayload = new Mock<TokenPayloadExtractorService>();            
            var mockJwt = new Mock<JwtSecurityTokenHandler>();

            var mockShippingExport = new Mock<IGShippingExportService>();
            var mockShippingLocal = new Mock<IGShippingLocalService>(); 

            var mockLogging = new Mock<ILogingETLService>();                       
            mockLogging.Setup(x => x.Update(logModel)).Returns(It.IsAny<Task>());

            ManualETL service = new ManualETL(mockShippingExport.Object,mockShippingLocal.Object, mockLogging.Object);

            var result = await service.Run(request.Object, logger.Object);

            Assert.NotNull(result);
        }
    }
}
