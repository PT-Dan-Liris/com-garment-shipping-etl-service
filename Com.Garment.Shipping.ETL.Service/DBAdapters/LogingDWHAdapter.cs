using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class LogingDWHAdapter : ILogingDWHAdapter
    {
        
        private readonly ISqlDataContext<LogingDWHModel> context;
        public LogingDWHAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<LogingDWHModel>>();
        }
        public async Task<IEnumerable<LogingDWHModel>> Get(int size, int page, string keyword)
        {
            try{    
                var query = $"SELECT * From [GDashboardLogDWH] ORDER BY UpdatedAt OFFSET({(page - 1) * size}) ROWS FETCH NEXT({size}) ROWS ONLY";
                var result = await context.QueryAsync(query);
                return result.ToList();
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Save(LogingDWHModel model)
        {
            try{
         
                var query = $"INSERT INTO [GDashboardLogDWH] ([UpdatedAt] ,[UpdatedBy] ,[Status]) VALUES (@UpdatedAt, @UpdatedBy, @Status)";
                var result = await context.ExecuteAsync(query, model);
            }catch(Exception ex){
                throw ex;
            }
            
        }
    }

    public interface ILogingDWHAdapter
    {
        Task<IEnumerable<LogingDWHModel>> Get(int size, int page, string keyword);
        Task Save(LogingDWHModel model);
    }
}