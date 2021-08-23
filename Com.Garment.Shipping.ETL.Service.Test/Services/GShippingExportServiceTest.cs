using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Garment.Shipping.ETL.Service.Test.Services
{
    public class GShippingExportServiceTest
    {
        public List<GShippingExportModel> GenerateModel()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();
            var listData = new List<GShippingExportModel>();
            listData.Add(new GShippingExportModel(
                rnd.Next(1, 10),
                guid,
                DateTime.Now,
                guid,
                guid,
                guid,
                guid,
                rnd.Next(1, 10),
                rnd.Next(1, 10),
                rnd.Next(1, 10),
                guid,
                guid,
                guid,
                guid,
                rnd.Next(1, 10),
                guid,
                rnd.Next(1, 10),
                rnd.Next(1, 10)
            ));

            return listData;
        }

        [Fact]
        public async Task SaveSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<GShippingExportModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<GShippingExportModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<GShippingExportModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<GShippingExportModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IGShippingExportAdapter))).Returns(new GShippingExportAdapter(serviceProvider.Object));

            var data = GenerateModel();

            GShippingExportService service = new GShippingExportService(serviceProvider.Object);
            await service.Save(data);
            Assert.True(true);
        }

        [Fact]
        public async Task GetSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<GShippingExportModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var data = GenerateModel();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<GShippingExportModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(data);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<GShippingExportModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IGShippingExportAdapter))).Returns(new GShippingExportAdapter(serviceProvider.Object));

            GShippingExportService service = new GShippingExportService(serviceProvider.Object);
            var result = await service.Get();
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task TruncateSuccess()
        {
            var sqlDataContext = new Mock<ISqlDataContext<GShippingExportModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var data = GenerateModel();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<GShippingExportModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(data);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<GShippingExportModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(IGShippingExportAdapter))).Returns(new GShippingExportAdapter(serviceProvider.Object));

            GShippingExportService service = new GShippingExportService(serviceProvider.Object);
            
            await service.ClearData(data);
            Assert.True(true);
        }
    }
}
