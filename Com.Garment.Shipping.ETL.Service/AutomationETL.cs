using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service
{
    public class AutomationETL
    {
        private readonly ILogingETLService _logingETLService;
        private readonly IGShippingExportService _gShippingExportService;
        private readonly IGShippingLocalService _gShippingLocalService;

        public AutomationETL(
            ILogingETLService logingETLService,
            IGShippingExportService gShippingExportService,
            IGShippingLocalService gShippingLocalService
        )
        {
            _logingETLService = logingETLService;
            _gShippingExportService = gShippingExportService;
            _gShippingLocalService = gShippingLocalService;
        }

        [FunctionName("automation-etl-batch-1")]
        public async Task AutomationETLBatch1([TimerTrigger("0 12 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                await RunETL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [FunctionName("automation-etl-batch-2")]
        public async Task AutomationETLBatch2([TimerTrigger("0 20 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                await RunETL();
            }
            catch (Exception ex)
            {
                await GenerateLoging(false);
                throw ex;
            }
        }

        public async Task GenerateLoging(bool status)
        {
            var data = new LogingETLModel(
                0,
                "Garment Shipping",
                DateTime.Now,
                "SYSTEM",
                status
            );

            await _logingETLService.Save(data);
        }

        public async Task RunETL()
        {
            try
            {
                var result = await _gShippingExportService.Get();
                await _gShippingExportService.ClearData(result);
                await _gShippingExportService.Save(result);

                var resultLocal = await _gShippingLocalService.Get();
                await _gShippingLocalService.ClearData(resultLocal);
                await _gShippingLocalService.Save(resultLocal);

                await GenerateLoging(true);
            }
            catch (Exception ex)
            {
                await GenerateLoging(false);
                throw ex;
            }

        }
    }
}
