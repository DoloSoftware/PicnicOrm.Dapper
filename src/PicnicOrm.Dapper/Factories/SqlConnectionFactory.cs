using System;
using System.Data.SqlClient;

namespace PicnicOrm.Dapper.Factories
{
    /// <summary>
    /// </summary>
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        #region Interfaces

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public SqlConnection Create(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"Parameter: ({nameof(connectionString)}) cannot be null or whitespace.");
            }

            var connection = new SqlConnection(connectionString);

            return connection;
        }

        #endregion
    }
}