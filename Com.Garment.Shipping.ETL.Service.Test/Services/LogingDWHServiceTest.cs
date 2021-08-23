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
    public class LogingDWHServiceTest
    {
        private LogingDWHModel GenerateModel()
        {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();
            LogingDWHModel model = new LogingDWHModel(
                rnd.Next(1, 10),
                DateTime.Now,
                guid,
                true
            );

            return model;
        }

        [Fact]
        public async Task SaveSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<LogingDWHModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingDWHModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<LogingDWHModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingDWHModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingDWHAdapter))).Returns(new LogingDWHAdapter(serviceProvider.Object));

            var model = GenerateModel();

            LogingDWHService service = new LogingDWHService(serviceProvider.Object);
            await service.Save(model);
            Assert.True(true);
        }
        [Fact]
        public async Task GetSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<LogingDWHModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var listData = new List<LogingDWHModel>();
            listData.Add(new LogingDWHModel(
                rnd.Next(1, 10),
                DateTime.Now,
                guid,
                true
            ));

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingDWHModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(listData);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingDWHModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingDWHAdapter))).Returns(new LogingDWHAdapter(serviceProvider.Object));

            var model = GenerateModel();

            LogingDWHService service = new LogingDWHService(serviceProvider.Object);
            var result = await service.Get(1, 1, string.Empty);
            Assert.True(result.Count() > 0);
        }
    }
}
