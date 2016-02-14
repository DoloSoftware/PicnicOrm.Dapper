using System;
using System.Data;
using System.Data.SqlClient;

namespace PicnicOrm.Factories
{
    /// <summary>
    /// </summary>
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        #region Interfaces

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IDbConnection Create(string connectionString)
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