using System.Collections.Generic;

using PicnicOrm.Data;

namespace PicnicOrm.Mapping
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
        /// <param name="parents"></param>
        /// <param name="shouldContinueThroughEmptyTables"></param>
        void Map(IGridReader gridReader, IDictionary<int, T> parents, bool shouldContinueThroughEmptyTables);

        #endregion
    }
}