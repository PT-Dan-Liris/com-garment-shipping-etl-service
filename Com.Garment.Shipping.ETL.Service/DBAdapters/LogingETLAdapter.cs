using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Helpers;
using Com.Garment.Shipping.ETL.Service.Models;
using Com.Garment.Shipping.ETL.Service.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Com.Garment.Shipping.ETL.Service.DBAdapters
{
    public class LogingETLAdapter : ILogingETLAdapter
    {
        private readonly ISqlDataContext<LogingETLModel> context;
        public LogingETLAdapter(IServiceProvider service)
        {
            context = service.GetService<ISqlDataContext<LogingETLModel>>();
        }
        public async Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword, string order)
        {
            try{    
                var orderObject = JsonConvert.DeserializeObject<LoggingETLOrderViewModel>(order);
                var query = $"SELECT * From [GDashboardLogETL] ";

                if (!keyword.Equals("")) {
                    query += " WHERE DataArea LIKE '%" + keyword +"%' ";
                }

                if (orderObject.DataArea != null) {
                    query += "  ORDER BY DataArea " + orderObject.DataArea;
                }

                if (orderObject.UpdatedAt != null) {
                    query += "  ORDER BY UpdatedAt " + orderObject.UpdatedAt;
                }

                if (orderObject.UpdatedBy != null) {
                    query += "  ORDER BY UpdatedBy " + orderObject.UpdatedBy;
                }

                if (orderObject.Status != null) {
                    query += "  ORDER BY Status " + orderObject.Status;
                }

                if (orderObject.DataArea == null && orderObject.UpdatedAt == null && orderObject.UpdatedBy == null && orderObject.Status == null) {
                    query += "  ORDER BY UpdatedAt desc ";
                }

                query += $" OFFSET({(page - 1) * size}) ROWS FETCH NEXT({size}) ROWS ONLY";

                var result = await context.QueryAsync(query);
                return result.ToList();
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Save(LogingETLModel model)
        {
            try{
                var query = $"INSERT INTO [GDashboardLogETL] ([DataArea] ,[UpdatedAt] ,[UpdatedBy] ,[Status]) VALUES (@DataArea, @UpdatedAt, @UpdatedBy, @Status)";
                var result = await context.ExecuteAsync(query, model);
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task<int> CountAll()
        {
            try{    
                var query = $"SELECT * From [GDashboardLogETL]";
                var result = await context.QueryAsync(query);
                return result.Count();
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Update(LogingETLModel model)
        {
            try{
                var query = $"Update [GDashboardLogETL] set [DataArea]=@DataArea ,[UpdatedAt]=@UpdatedAt ,[UpdatedBy]=@UpdatedBy ,[Status]=@Status WHERE [Id]=@Id";
                var result = await context.ExecuteAsync(query, model);
            }catch(Exception ex){
                throw ex;
            }
        }
    }


    public interface ILogingETLAdapter
    {        
        Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword, string order);
        Task Save(LogingETLModel model);
        Task Update(LogingETLModel model);
        Task<int> CountAll();
    }
}