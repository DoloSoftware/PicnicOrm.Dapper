using System;
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
            : this(connectionString, new SqlConnectionFactory())
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlConnectionFactory"></param>
        public SqlDataBroker(string connectionString, ISqlConnectionFactory sqlConnectionFactory)
        {
            _connectionString = connectionString;
            _sqlConnectionFactory = sqlConnectionFactory;
            _parentMappings = new Dictionary<int, IParentMapping>();
        }

        #endregion

        #region Interfaces

        public void AddMapping(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mapping"></param>
        public void AddMapping(int key, IParentMapping mapping)
        {
            _parentMappings.Add(key, mapping);
        }

        public void ExecuteStoreQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string sql) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryGraph<T>(string sql) where T : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryGraph<T>(string sql, int parentMappingKey) where T : class
        {
            throw new NotImplementedException();
        }

        public T QueryScalar<T>(string sql) where T : IConvertible
        {
            throw new NotImplementedException();
        }

        public T QuerySingle<T>(string sql) where T : class
        {
            throw new NotImplementedException();
        }

        public T QuerySingleGraph<T>(string sql) where T : class
        {
            throw new NotImplementedException();
        }

        public T QuerySingleGraph<T>(string sql, int parentMappingKey) where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods

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
                    using (var multi = new GridReaderWrapper(connection.QueryMultiple(sql)))
                    {
                        var mapping = (IParentMapping<T>)_parentMappings[parentMappingKey];
                        list = mapping.Read(multi);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}