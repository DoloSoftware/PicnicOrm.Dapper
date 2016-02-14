using System;
using System.Collections.Generic;
using System.Data;

using PicnicOrm.Data;
using PicnicOrm.Factories;
using PicnicOrm.Mapping;

namespace PicnicOrm
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
        /// 
        /// </summary>
        private readonly IGridReaderFactory _gridReaderFactory;

        /// <summary>
        /// </summary>
        private readonly IDictionary<int, IParentMapping> _parentMappings;

        /// <summary>
        /// </summary>
        private readonly IDbConnectionFactory _sqlConnectionFactory;

        /// <summary>
        /// </summary>
        private readonly IDictionary<Type, IParentMapping> _typeMapping;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="gridReaderFactory"></param>
        public SqlDataBroker(string connectionString, IGridReaderFactory gridReaderFactory)
            : this(connectionString, new SqlConnectionFactory(), gridReaderFactory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlConnectionFactory"></param>
        /// <param name="gridReaderFactory"></param>
        public SqlDataBroker(string connectionString, IDbConnectionFactory sqlConnectionFactory, IGridReaderFactory gridReaderFactory)
        {
            _connectionString = connectionString;
            _sqlConnectionFactory = sqlConnectionFactory;
            _gridReaderFactory = gridReaderFactory;
            _parentMappings = new Dictionary<int, IParentMapping>();
            _typeMapping = new Dictionary<Type, IParentMapping>();
        }

        #endregion

        #region Interfaces

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

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapping"></param>
        public void AddMapping<T>(IParentMapping<T> mapping) where T : class => _typeMapping.Add(typeof(T), mapping);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="mappingKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName, int mappingKey, IList<IDbParameter> parameters = null) where T : class
        {
            IEnumerable<T> list = null;

            if (!_parentMappings.ContainsKey(mappingKey))
            {
                throw new ArgumentException($"Parameter: ({nameof(mappingKey)}: {mappingKey}) is not a valid key.");
            }
            using (var connection = _sqlConnectionFactory.Create(_connectionString))
            {
                using (var multi = _gridReaderFactory.Create(connection, storedProcName, parameters, commandType: CommandType.StoredProcedure))
                {
                    var mapping = (IParentMapping<T>)_parentMappings[mappingKey];
                    list = mapping.Read(multi);
                }
            }

            return list;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName, IList<IDbParameter> parameters = null) where T : class
        {
            IEnumerable<T> list = null;

            var key = typeof(T);
            if (!_typeMapping.ContainsKey(key))
            {
                throw new ArgumentException($"Type: ({typeof(T)}) is not a registered Type.");
            }

            using (var connection = _sqlConnectionFactory.Create(_connectionString))
            {
                using (var multi = _gridReaderFactory.Create(connection, storedProcName, parameters, commandType: CommandType.StoredProcedure))
                {
                    var mapping = (IParentMapping<T>)_typeMapping[key];
                    list = mapping.Read(multi);
                }
            }

            return list;
        }

        #endregion

        //}
        //    throw new NotImplementedException();
        //{

        //public IEnumerable<T> QueryGraph<T>(string sql) where T : class
        //}
        //    throw new NotImplementedException();
        //{

        //public IEnumerable<T> Query<T>(string sql) where T : class
        //}
        //    throw new NotImplementedException();
        //{

        //public void ExecuteStoreQuery(string sql)

        //public IEnumerable<T> QueryGraph<T>(string sql, int parentMappingKey) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public T QueryScalar<T>(string sql) where T : IConvertible
        //{
        //    throw new NotImplementedException();
        //}

        //public T QuerySingle<T>(string sql) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public T QuerySingleGraph<T>(string sql) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public T QuerySingleGraph<T>(string sql, int parentMappingKey) where T : class
        //{
        //    throw new NotImplementedException();
        //}
    }
}