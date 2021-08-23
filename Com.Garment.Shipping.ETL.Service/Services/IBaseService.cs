using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    public interface IBaseService<TModel>
    {
        Task<IEnumerable<TModel>> Get();
        Task Save(IEnumerable<TModel> data);
        Task ClearData(IEnumerable<TModel> data);
    }
}