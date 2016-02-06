using System;
using System.Collections.Generic;

using PicnicOrm.Dapper.Mapping;

namespace PicnicOrm.Dapper
{
    public interface IDataBroker
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mapping"></param>
        void AddMapping(int key, IParentMapping mapping);

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        void AddMapping(Type type);

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

        #endregion
    }
}