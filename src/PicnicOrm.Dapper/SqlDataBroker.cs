using System;
using System.Collections.Generic;
using System.Text;

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

        private readonly IDictionary<Type, IParentMapping> _typeMapping; 

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
            _typeMapping = new Dictionary<Type, IParentMapping>();
        }

        #endregion

        #region Interfaces

        public void AddMapping<T>(IParentMapping mapping)
        {
            _typeMapping.Add(typeof(T), mapping);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mapping"></param>
        public void AddMapping(int key, IParentMapping mapping)
        {
            if (_parentMappings.ContainsKey(key))
            {
                throw new ArgumentException($"{nameof(key)}: ({key}) has already been added.");
            }

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


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="parentMappingKey"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName, int parentMappingKey) where T : class
        {
            IEnumerable<T> list = null;

            if (_parentMappings.ContainsKey(parentMappingKey))
            {
                var sqlBuilder = new StringBuilder("EXEC ");
                sqlBuilder.Append(storedProcName);

                using (var connection = _sqlConnectionFactory.Create(_connectionString))
                {
                    using (var multi = new GridReaderWrapper(connection.QueryMultiple(sqlBuilder.ToString())))
                    {
                        var mapping = (IParentMapping<T>)_parentMappings[parentMappingKey];
                        list = mapping.Read(multi);
                    }
                }
            }

            return list;
        }

        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName) where T : class
        {
            IEnumerable<T> list = null;

            var key = typeof(T);
            if (_typeMapping.ContainsKey(key))
            {
                var sqlBuilder = new StringBuilder("EXEC ");
                sqlBuilder.Append(storedProcName);
                using (var connection = _sqlConnectionFactory.Create(_connectionString))
                {

                    using (var multi = new GridReaderWrapper(connection.QueryMultiple(sqlBuilder.ToString())))
                    {
                        var mapping = (IParentMapping<T>)_typeMapping[key];
                        list = mapping.Read(multi);
                    }
                }
            }

            return list;
        } 
    }
}