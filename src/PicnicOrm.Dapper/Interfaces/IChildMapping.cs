using System.Collections.Generic;

using Dapper;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChildMapping<T>
        where T : class
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="items"></param>
        void Map(SqlMapper.GridReader gridReader, IDictionary<int, T> parents);

        #endregion
    }
}