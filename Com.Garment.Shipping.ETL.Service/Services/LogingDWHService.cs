using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class LogingDWHService : ILogingDWHService
    {
        ILogingDWHAdapter _logingDWHAdapter;
        public LogingDWHService(IServiceProvider service)
        {
            _logingDWHAdapter = service.GetService<ILogingDWHAdapter>();
        }

        public async Task<IEnumerable<LogingDWHModel>> Get(int size, int page, string keyword)
        {
            try{
                var result = await _logingDWHAdapter.Get(size, page, keyword);
                return result;
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Save(LogingDWHModel data)
        {
            try{
                await _logingDWHAdapter.Save(data);
            }catch(Exception ex){
                throw ex;
            }
            
        }
    }

    public interface ILogingDWHService
    {
        Task<IEnumerable<LogingDWHModel>> Get(int size, int page, string keyword);
        Task Save(LogingDWHModel data);
    }
}