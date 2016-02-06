using System.Collections.Generic;

namespace PicnicOrm.Dapper
{
    public interface IGridReader
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> Read<T>();

        #endregion
    }
}