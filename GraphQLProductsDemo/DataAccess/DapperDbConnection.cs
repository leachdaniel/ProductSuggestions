using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLProductsDemo.DataAccess
{
    /// <summary>
    /// Exposes methods we want to use from Dapper with the same interface, but not as extension methods
    /// Allows overriding the methods from Dapper
    /// </summary>
    public class DapperDbConnection<TConn> : IDisposable, IDapperDbConnection
        where TConn : IDbConnection
    {
        public DapperDbConnection(string connectionString)
        {
            _connection = new Lazy<IDbConnection>(() => CreateAndGetOpenConnection(connectionString));
        }

        public void Dispose()
        {
            if (_connection != null && _connection.IsValueCreated && _connection.Value != null)
            {
                _connection.Value.Close();
                _connection.Value.Dispose();
            }
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryAsync<T>(Conn, sql, param, transaction, commandTimeout, commandType);
        }


        public Task<T?> GetAsync<T>(dynamic key, IDbTransaction? transaction = null, int? commandTimeout = null)
            where T : class
        {
            return SqlMapperExtensions.GetAsync<T>(Conn, key, transaction, commandTimeout);
        }

        private IDbConnection Conn => _connection.Value;

        private IDbConnection CreateAndGetOpenConnection(string connectionString)
        {
            IDbConnection c;

            if (typeof(TConn) == typeof(SqlConnection))
            {
                c = new SqlConnection(connectionString);
            }
            else
            {
                throw new NotSupportedException();
            }

            c.Open();
            return c;
        }
        private readonly Lazy<IDbConnection> _connection;
    }
}
