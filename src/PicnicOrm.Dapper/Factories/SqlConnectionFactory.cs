using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicnicOrm.Dapper.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public SqlConnection Create(string connectionString)
        {
            var connection = new SqlConnection(connectionString);

            return connection;
        }
    }
}
