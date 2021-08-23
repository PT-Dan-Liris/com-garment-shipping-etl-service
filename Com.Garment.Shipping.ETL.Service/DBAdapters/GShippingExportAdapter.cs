using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class GShippingExportAdapter : IGShippingExportAdapter
    {
        private readonly ISqlDataContext<GShippingExportModel> context;
        public GShippingExportAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<GShippingExportModel>>();
        }
        public async Task<IEnumerable<GShippingExportModel>> Get()
        {
            try
            {
                var query = $"SELECT a.Id as IdPackingLists, a.InvoiceNo, a.TruckingDate, a.BuyerAgentCode, a.BuyerAgentName, a.Destination, a.SectionCode, b.PackingListId,b.Id as IdShippingInvoices,c.GarmentShippingInvoiceId,c.BuyerBrandName,c.ComodityCode,c.ComodityName,c.UnitCode,c.Quantity,c.UomUnit,c.CMTPrice,c.Amount from GarmentPackingLists as a join GarmentShippingInvoices as b on a.Id = b.PackingListId join GarmentShippingInvoiceItems as c on b.Id = c.GarmentShippingInvoiceId where a.IsDeleted = 0 and a.Omzet = 1 and a.IsUsed = 1 and b.IsDeleted = 0 and c.IsDeleted = 0 order by a.TruckingDate,a.InvoiceNo";


                var result = await context.QueryAsync(query);
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Save(IEnumerable<GShippingExportModel> models)
        {
            var query = $"Insert Into [dbo].[GShippingExport]  ([IdPackingLists],[InvoiceNo],[TruckingDate],[BuyerAgentCode],[BuyerAgentName],[Destination],[SectionCode],[PackingListId],[IdShippingInvoices],[GarmentShippingInvoiceId],[BuyerBrandName],[ComodityCode],[ComodityName],[UnitCode],[Quantity],[UomUnit],[CMTPrice],[Amount]) VALUES ( @IdPackingLists ,@InvoiceNo ,@TruckingDate ,@BuyerAgentCode ,@BuyerAgentName ,@Destination ,@SectionCode ,@PackingListId ,@IdShippingInvoice ,@GarmentShippingInvoiceId ,@BuyerBrandName ,@ComodityCode ,@ComodityName ,@UnitCode ,@Quantity ,@UomUnit ,@CMTPrice ,@Amount)";
            await context.ExecuteAsync(query, models);
        }

        public async Task Truncate(IEnumerable<GShippingExportModel> models)
        {
            var query = "TRUNCATE TABLE [dbo].[GShippingExport]";
            await context.ExecuteAsyncTruncate(query);
        }
    }

    public interface IGShippingExportAdapter : IBaseAdapter<GShippingExportModel>
    {

    }
}