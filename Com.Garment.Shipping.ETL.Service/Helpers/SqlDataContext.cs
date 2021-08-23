using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Com.Garment.Shipping.ETL.Service.Helpers

{
    [ExcludeFromCodeCoverage]
    public class SqlDataContext<TModel> : ISqlDataContext<TModel>
    {

        private readonly SqlConnection _connectionOrigin;
        private readonly SqlConnection _connectionDestination;

        public SqlDataContext(string connectionStringOrigin, string connectionStringDestination)
        {
            _connectionOrigin = CreateConnection(connectionStringOrigin);
            _connectionDestination = CreateConnection(connectionStringDestination);
        }

        public async Task<int> ExecuteAsync(string query, IEnumerable<TModel> models)
        {
            if(_connectionDestination.State != ConnectionState.Open)
                _connectionDestination.Open(); 
            
            var transaction = _connectionDestination.BeginTransaction();
            var result = await _connectionDestination.ExecuteAsync(query, models, transaction : transaction);
            transaction.Commit();

            if(_connectionDestination.State == ConnectionState.Open)
                _connectionDestination.Close();
            return result;
        }

        public async Task<int> ExecuteAsync(string query, TModel model)
        {
            if(_connectionOrigin.State != ConnectionState.Open)
                _connectionOrigin.Open(); 
            
            var transaction = _connectionOrigin.BeginTransaction();
            var result = await _connectionOrigin.ExecuteAsync(query, model,  transaction : transaction);
            transaction.Commit();

            if(_connectionOrigin.State == ConnectionState.Open)
                _connectionOrigin.Close();
            return result;
        }

        public async Task<IEnumerable<TModel>> QueryAsync(string query)
        {
            if(_connectionOrigin.State == ConnectionState.Closed)
                _connectionOrigin.Open(); 

            
            var transaction = _connectionOrigin.BeginTransaction();
            var result = await _connectionOrigin.QueryAsync<TModel>(query, transaction : transaction);
            _connectionOrigin.Close();

            if(_connectionOrigin.State == ConnectionState.Open)
                _connectionOrigin.Close();
            return result;
            }

        public async Task<int> ExecuteAsyncTruncate(string query)
        {
            if (_connectionDestination.State != ConnectionState.Open)
                _connectionDestination.Open();

            //var transaction = _connectionDestination.BeginTransaction();
            var result = await _connectionDestination.ExecuteAsync(query);
            //transaction.Commit();

            if (_connectionDestination.State == ConnectionState.Open)
                _connectionDestination.Close();
            return result;
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            return new SqlConnection(connectionString);
        }
    }

    public interface ISqlDataContext<TModel>
    {
        Task<int> ExecuteAsync(string query, IEnumerable<TModel> model);
        Task<int> ExecuteAsync(string query, TModel model);
        Task<IEnumerable<TModel>> QueryAsync(string query);

        Task<int> ExecuteAsyncTruncate(string query);
    }
}
