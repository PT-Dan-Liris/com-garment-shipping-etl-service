using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Com.Garment.Shipping.ETL.Service.Services;
using Com.Garment.Shipping.ETL.Service.Models;
using System.Collections.Generic;

namespace Com.Garment.Shipping.ETL.Service
{
    public class ManualETL
    {
        private readonly IGShippingExportService _gShippingExportService;
        private readonly IGShippingLocalService _gShippingLocalService;
        private readonly ILogingETLService _loggingETLService;
        public ManualETL(
            IGShippingExportService gShippingExportService,
            IGShippingLocalService gshippingLocalService,
            ILogingETLService logingETLService)
        {
            _gShippingExportService = gShippingExportService;
            _gShippingLocalService = gshippingLocalService;
            _loggingETLService = logingETLService;
        }

        [FunctionName("manual-etl")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            
            string token = req.Headers["Authorization"];
            if (token == null) {
                return new BadRequestObjectResult(new {message ="Failed! token empty"});
            }
            var tokenPayload = new TokenPayloadExtractorService(token);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<LogingETLModel>(requestBody);

            try
            {
                //Get and Insert to Export
                var result = await _gShippingExportService.Get();

                await _gShippingExportService.ClearData(result);

                await _gShippingExportService.Save(result);

                //Get and Insert to Local
                var resultLocal = await _gShippingLocalService.Get();

                await _gShippingLocalService.ClearData(resultLocal);

                var listDataLocal = new List<GShippingLocalModel>();

                await _gShippingLocalService.Save(resultLocal);
                
                var loggingExportData = new LogingETLModel(
                    data.Id,
                    data.DataArea,
                    DateTime.UtcNow,
                    tokenPayload.GetUsername(),
                    true
                );
             
                await _loggingETLService.Update(loggingExportData);

                return new OkObjectResult(new { message = "success" });
            }
            catch (Exception Ex)
            {
                var loggingExportData = new LogingETLModel(
                    data.Id,
                    data.DataArea,
                    DateTime.Now,
                    tokenPayload.GetUsername(),
                    false
                );
             
                await _loggingETLService.Update(loggingExportData);

                return new BadRequestObjectResult(new {message ="Bad Request", info = Ex.Message});
            }
        }
    }
}
