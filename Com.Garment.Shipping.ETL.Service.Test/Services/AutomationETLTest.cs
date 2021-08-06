using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Garment.Shipping.ETL.Service.Test.Services
{
    public class AutomationETLTest
    {
        [Fact]
        public async Task AutomationRunETL()
        {
            var mockShippingExport = new Mock<IGShippingExportService>();
            var mockShippingLocal = new Mock<IGShippingLocalService>();
            var mockLogging = new Mock<ILogingETLService>();

            AutomationETL service = new AutomationETL(mockLogging.Object, mockShippingExport.Object, mockShippingLocal.Object);
            await service.RunETL();

        }

        [Fact]
        public async Task AutomationRunETLBatch1()
        {
            var mockShippingExport = new Mock<IGShippingExportService>();
            var mockShippingLocal = new Mock<IGShippingLocalService>();
            var mockLogging = new Mock<ILogingETLService>();
            var mockIlogger = new Mock<ILogger>();

            var param1 = default(TimerInfo);

            AutomationETL service = new AutomationETL(mockLogging.Object, mockShippingExport.Object, mockShippingLocal.Object);

            await service.AutomationETLBatch1(param1,mockIlogger.Object);            
        }

        [Fact]
        public async Task AutomationRunETLBatch2()
        {
            var mockShippingExport = new Mock<IGShippingExportService>();
            var mockShippingLocal = new Mock<IGShippingLocalService>();
            var mockLogging = new Mock<ILogingETLService>();
            var mockIlogger = new Mock<ILogger>();

            var param1 = default(TimerInfo);

            AutomationETL service = new AutomationETL(mockLogging.Object, mockShippingExport.Object, mockShippingLocal.Object);
            
            await service.AutomationETLBatch2(param1, mockIlogger.Object);
        }
    }
}
