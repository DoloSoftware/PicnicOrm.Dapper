using System.Collections.Generic;

using PicnicOrm.Data;

namespace PicnicOrm.Mapping
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
        /// <param name="shouldContinueThroughEmptyTables"></param>
        /// <returns></returns>
        IEnumerable<T> Read(IGridReader gridReader, bool shouldContinueThroughEmptyTables = true);

        #endregion
    }
}