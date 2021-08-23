using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class GShippingExportService : IGShippingExportService
    {
        IGShippingExportAdapter _gShippingExportAdapter;
        public GShippingExportService(IServiceProvider service)
        {
            _gShippingExportAdapter = service.GetService<IGShippingExportAdapter>();
        }
        
        public async Task ClearData(IEnumerable<GShippingExportModel> data)
        {
            await _gShippingExportAdapter.Truncate(data);
        }

        public async Task<IEnumerable<GShippingExportModel>> Get()
        {
            var result = await _gShippingExportAdapter.Get();
            return result;
        }

        public async Task Save(IEnumerable<GShippingExportModel> data)
        {
            try
            {
                await _gShippingExportAdapter.Save(data);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
    }

    public interface IGShippingExportService : IBaseService<GShippingExportModel>
    {

    }
}