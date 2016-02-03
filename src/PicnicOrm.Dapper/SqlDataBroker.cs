using System.Collections.Generic;

using Dapper;

using PicnicOrm.Dapper.Factories;
using PicnicOrm.Dapper.Mapping;

namespace PicnicOrm.Dapper
{
    /// <summary>
    /// </summary>
    public class SqlDataBroker : IDataBroker
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// </summary>
        private readonly IDictionary<int, IParentMapping> _parentMappings;

        /// <summary>
        /// </summary>
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlDataBroker(string connectionString)
        {
            _connectionString = connectionString;

            _sqlConnectionFactory = new SqlConnectionFactory();
        }

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlConnectionFactory"></param>
        public SqlDataBroker(string connectionString, ISqlConnectionFactory sqlConnectionFactory)
        {
            _connectionString = connectionString;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        #endregion

        #region Interfaces

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mapping"></param>
        public void AddMapping(int key, IParentMapping mapping)
        {
            _parentMappings.Add(key, mapping);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parentMappingKey"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedure<T>(string sql, int parentMappingKey) where T : class
        {
            IEnumerable<T> list = null;

            if (_parentMappings.ContainsKey(parentMappingKey))
            {
                using (var connection = _sqlConnectionFactory.Create(_connectionString))
                {
                    using (var multi = connection.QueryMultiple(sql))
                    {
                        var mapping = (IParentMapping<T>) _parentMappings[parentMappingKey];
                        list = mapping.Read(multi);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}