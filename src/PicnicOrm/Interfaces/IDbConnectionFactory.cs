using System.Data;

namespace PicnicOrm.Factories
{
    /// <summary>
    /// </summary>
    public interface IDbConnectionFactory
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IDbConnection Create(string connectionString);

        #endregion
    }
}