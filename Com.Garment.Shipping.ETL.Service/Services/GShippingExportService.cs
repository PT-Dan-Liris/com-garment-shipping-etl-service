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
        public async Task<IEnumerable<GShippingExportModel>> Get()
        {
            var result = await _gShippingExportAdapter.GetData();
            return result;
        }

        public async Task Save(IEnumerable<GShippingExportModel> data)
        {
            try
            {
                await _gShippingExportAdapter.LoadData(data);
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