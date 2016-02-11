using System;
using System.Collections.Generic;
using System.Data;

using Dapper;

using PicnicOrm.Dapper.Mapping;

namespace PicnicOrm.Dapper
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataBroker
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mapping"></param>
        void AddMapping(int key, IParentMapping mapping);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mapping"></param>
        void AddMapping<T>(IParentMapping mapping);

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parentMappingKey"></param>
        /// <returns></returns>
        IEnumerable<T> QueryGraph<T>(string sql, int parentMappingKey) where T : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> QueryGraph<T>(string sql) where T : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql) where T : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T QuerySingle<T>(string sql) where T : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T QuerySingleGraph<T>(string sql) where T : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parentMappingKey"></param>
        /// <returns></returns>
        T QuerySingleGraph<T>(string sql, int parentMappingKey) where T : class;

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T QueryScalar<T>(string sql) where T : IConvertible;

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        void ExecuteStoreQuery(string sql);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName, DynamicParameters parameters = null) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName, int configKey, DynamicParameters parameters = null) where T : class;

        #endregion
    }
}