using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Com.Garment.Shipping.ETL.Service.Services;
using Newtonsoft.Json;
using System.Linq;
using Com.Garment.Shipping.ETL.Service.ViewModels;

namespace Com.Garment.Shipping.ETL.Service
{
    public class ETLLog
    {
        private readonly ILogingETLService _iLogingETLService;
        public ETLLog(ILogingETLService ilogingETLService)
        {
            _iLogingETLService = ilogingETLService;
        }

        [FunctionName("ETLLog")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            int page;
            int size;
            var pageExtract = int.TryParse(req.Query["page"], out page);
            var sizeExtract = int.TryParse(req.Query["size"], out size);
            string order = req.Query["order"];
            if (order == null) {
                order = "{}";
            }
            string keyword = req.Query["keyword"];
            if (keyword == null) {
                keyword = "";
            }

            var result = await _iLogingETLService.Get(size, page, keyword, order);
            var count = await _iLogingETLService.CountAll();

            if (result != null)
            {
                return new OkObjectResult(
                    new ResponseSuccessViewModel(
                        "Success",
                        result,
                        new { Count = result.Count(), Page = page, Size = size, Total = count}
                    )
                );
            }

            return new BadRequestObjectResult(new {message = "Failed"});
        }
    }
}
