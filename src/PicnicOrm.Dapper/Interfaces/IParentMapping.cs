using System.Collections.Generic;

using Dapper;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// </summary>
    public interface IParentMapping
    {
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParentMapping<T> : IParentMapping
        where T : class
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <returns></returns>
        IEnumerable<T> Read(SqlMapper.GridReader gridReader);

        #endregion
    }
}