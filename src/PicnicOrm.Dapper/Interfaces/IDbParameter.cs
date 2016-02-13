using System.Data;

namespace PicnicOrm.Dapper.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbParameter
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        DbType? DbType { get; }

        /// <summary>
        /// 
        /// </summary>
        ParameterDirection? Direction { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        byte? Precision { get; }

        /// <summary>
        /// 
        /// </summary>
        byte? Scale { get; }

        /// <summary>
        /// 
        /// </summary>
        int? Size { get; }

        /// <summary>
        /// 
        /// </summary>
        object Value { get; }

        #endregion
    }
}