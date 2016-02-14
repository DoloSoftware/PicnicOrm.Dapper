using System;
using System.Collections.Generic;

namespace PicnicOrm.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGridReader : IDisposable
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