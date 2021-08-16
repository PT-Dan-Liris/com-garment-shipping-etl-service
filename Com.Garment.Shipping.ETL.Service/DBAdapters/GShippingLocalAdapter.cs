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
    public class GShippingLocalAdapter : IGShippingLocalAdapter
    {
        private readonly ISqlDataContext<GShippingLocalModel> context;
        public GShippingLocalAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<GShippingLocalModel>>();
        }


        public async Task<IEnumerable<GShippingLocalModel>> Get()
        {
            var query = $"SELECT a.Id,a.NoteNo,a.Date,a.BuyerCode,a.BuyerName,a.TransactionTypeCode,a.TransactionTypeName,b.LocalSalesNoteId,b.Quantity,b.UomUnit,b.Price,b.Quantity*b.Price as Amount from GarmentShippingLocalSalesNotes as a join GarmentShippingLocalSalesNoteItems as b on a.Id=b.LocalSalesNoteId where a.IsDeleted=0 and b.IsDeleted=0 order by a.Date,a.NoteNo";
            var result = await context.QueryAsync(query);
            return result.ToList();
        }

        public async Task Save(IEnumerable<GShippingLocalModel> models)
        {
            var query = $"INSERT INTO  [dbo].[GShippingLocal] ([Id],[NoteNo],[Date],[BuyerCode],[BuyerName],[LocalSalesNoteId],[Quantity],[UomUnit],[Price],[Amount],[TransactionTypeCode],[TransactionTypeName]) Values (@Id ,@NoteNo ,@Date ,@BuyerCode ,@BuyerName ,@LocalSalesNoteId ,@Quantity ,@UomUnit ,@Price ,@Amount, @TransactionTypeCode, @TransactionTypeName )";
            await context.ExecuteAsync(query, models);
        }      

        public async Task Truncate(IEnumerable<GShippingLocalModel> models)
        {
            var query = $"TRUNCATE TABLE [dbo].[GShippingLocal]";
            await context.ExecuteAsyncTruncate(query);
        }
    }

    public interface IGShippingLocalAdapter : IBaseAdapter<GShippingLocalModel>
    {
        
    }
}