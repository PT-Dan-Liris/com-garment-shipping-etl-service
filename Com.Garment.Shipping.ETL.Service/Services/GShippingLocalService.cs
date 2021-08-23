using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class GShippingLocalService : IGShippingLocalService
    {
        IGShippingLocalAdapter _gShippingLocalAdapter;
        public GShippingLocalService(IServiceProvider service)
        {
            _gShippingLocalAdapter = service.GetService<IGShippingLocalAdapter>();
        }


        public async Task ClearData(IEnumerable<GShippingLocalModel> data)
        {
            await _gShippingLocalAdapter.Truncate(data);
        }

        public async Task<IEnumerable<GShippingLocalModel>> Get()
        {
            var result = await _gShippingLocalAdapter.Get();
            return result;
        }

        public async Task Save(IEnumerable<GShippingLocalModel> data)
        {
            try
            {
                await _gShippingLocalAdapter.Save(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    public interface IGShippingLocalService  : IBaseService<GShippingLocalModel>
    {
        
    }
}