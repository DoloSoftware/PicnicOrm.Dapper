using System.Data.SqlClient;

namespace PicnicOrm.Dapper.Factories
{
    /// <summary>
    /// </summary>
    public interface ISqlConnectionFactory
    {
        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        SqlConnection Create(string connectionString);

        #endregion
    }
}