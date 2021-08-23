using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Garment.Shipping.ETL.Service.DBAdapters;
using Com.Garment.Shipping.ETL.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public class LogingETLService : ILogingETLService
    {
        ILogingETLAdapter _logingETLAdapter;
        public LogingETLService(IServiceProvider service)
        {            
            _logingETLAdapter = service.GetService<ILogingETLAdapter>();
        }
        public async Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword, string order)
        {
            try{
                var result = await _logingETLAdapter.Get(size, page, keyword, order);
                return result;
            }catch(Exception ex){
                throw ex;
            }
        }
        public async Task<int> CountAll()
        {
            try{
                var result = await _logingETLAdapter.CountAll();
                return result;
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Save(LogingETLModel data)
        {
            try{
                await _logingETLAdapter.Save(data);
            }catch(Exception ex){
                throw ex;
            }
        }

        public async Task Update(LogingETLModel data)
        {
            try{
                await _logingETLAdapter.Update(data);
            }catch(Exception ex){
                throw ex;
            }
        }
    }

    public interface ILogingETLService
    {
        Task<IEnumerable<LogingETLModel>> Get(int size, int page, string keyword, string order);
        Task Save(LogingETLModel data); 
        Task Update(LogingETLModel data);      
        Task<int> CountAll();  
    }
}