using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductSuggestions.DataAccess
{
    /// <summary>
    /// Exposes methods we want to use from Dapper/Dapper.Contrib with the same interface, but not as extension methods
    /// so they can be easily mocked and overridden
    /// </summary>
    public interface IDapperDbConnection
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);


        Task<T?> GetAsync<T>(dynamic key, IDbTransaction? transaction = null, int? commandTimeout = null)
            where T : class;
    }
}
