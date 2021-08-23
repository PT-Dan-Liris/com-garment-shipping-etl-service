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
    public class LogingETLServiceTest
    {
        private LogingETLModel GenerateModel() {
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();
            LogingETLModel model = new LogingETLModel(
                rnd.Next(1,10),
                guid,
                DateTime.Now,
                guid,
                true
            );

            return model;
        }

        [Fact]
        public async Task SaveSuccess()
        {
            
            var sqlDataContext = new Mock<ISqlDataContext<LogingETLModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingETLModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<LogingETLModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingETLModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingETLAdapter))).Returns(new LogingETLAdapter(serviceProvider.Object));

            var model = GenerateModel();

            LogingETLService service = new LogingETLService(serviceProvider.Object);
            await service.Save(model);
            Assert.True(true);
        }
        [Fact]
        public async Task UpdateSuccess()
        {
            
            var sqlDataContext = new Mock<ISqlDataContext<LogingETLModel>>();
            var serviceProvider = new Mock<IServiceProvider>();

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingETLModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(new List<LogingETLModel>());
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingETLModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingETLAdapter))).Returns(new LogingETLAdapter(serviceProvider.Object));

            var model = GenerateModel();

            LogingETLService service = new LogingETLService(serviceProvider.Object);
            await service.Update(model);
            Assert.True(true);
        }
        
        [Fact]
        public async Task GetSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<LogingETLModel>>();
            var serviceProvider = new Mock<IServiceProvider>();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var model = GenerateModel();
            var listData = new List<LogingETLModel>();
            listData.Add(model);

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingETLModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(listData);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingETLModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingETLAdapter))).Returns(new LogingETLAdapter(serviceProvider.Object));

            LogingETLService service = new LogingETLService(serviceProvider.Object);
            var result = await service.Get(1, 1, string.Empty, "{}");
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task GetDataAreaSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<LogingETLModel>>();
            var serviceProvider = new Mock<IServiceProvider>();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var model = GenerateModel();
            var listData = new List<LogingETLModel>();
            listData.Add(model);

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingETLModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(listData);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingETLModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingETLAdapter))).Returns(new LogingETLAdapter(serviceProvider.Object));

            LogingETLService service = new LogingETLService(serviceProvider.Object);
            var result = await service.Get(1, 1, "Test", "{DataArea:10}");
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task GetUpdateAtSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<LogingETLModel>>();
            var serviceProvider = new Mock<IServiceProvider>();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var model = GenerateModel();
            var listData = new List<LogingETLModel>();
            listData.Add(model);

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingETLModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(listData);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingETLModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingETLAdapter))).Returns(new LogingETLAdapter(serviceProvider.Object));

            LogingETLService service = new LogingETLService(serviceProvider.Object);
            var result = await service.Get(1, 1, "Test", "{UpdatedAt:'Today',UpdatedBy:'Test',Status:'Success'}");
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task CountAllSuccess()
        {

            var sqlDataContext = new Mock<ISqlDataContext<LogingETLModel>>();
            var serviceProvider = new Mock<IServiceProvider>();
            var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
            Random rnd = new Random();

            var model = GenerateModel();
            var listData = new List<LogingETLModel>();
            listData.Add(model);

            sqlDataContext.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<LogingETLModel>())).ReturnsAsync(1);
            sqlDataContext.Setup(x => x.QueryAsync(It.IsAny<string>())).ReturnsAsync(listData);
            serviceProvider.Setup(x => x.GetService(typeof(ISqlDataContext<LogingETLModel>))).Returns(sqlDataContext.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogingETLAdapter))).Returns(new LogingETLAdapter(serviceProvider.Object));

            LogingETLService service = new LogingETLService(serviceProvider.Object);
            var result = await service.CountAll();
            Assert.True(result > 0);
        }
    }
}
