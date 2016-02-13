using System.Collections.Generic;
using System.Data;
using System.Linq;

using Dapper;

using PicnicOrm.Dapper.Data;

namespace PicnicOrm.Dapper.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class DapperGridReaderFactory : IGridReaderFactory
    {
        #region Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IGridReader Create(IDbConnection dbConnection, string storedProcName, IList<IDbParameter> parameters = null, IDbTransaction dbTransaction = null, int? commandTimeout = null,
                                  CommandType? commandType = null)
        {
            var dynamicParameters = ConvertToDynamicParameters(parameters);

            return new GridReaderWrapper(dbConnection.QueryMultiple(storedProcName, dynamicParameters, dbTransaction, commandTimeout, commandType));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DynamicParameters ConvertToDynamicParameters(IList<IDbParameter> parameters)
        {
            DynamicParameters dynamicParameters = null;

            if (parameters != null && parameters.Any())
            {
                dynamicParameters = new DynamicParameters();

                foreach (var parameter in parameters)
                {
                    dynamicParameters.Add(parameter.Name, parameter.Value, parameter.DbType, parameter.Direction, parameter.Size, parameter.Precision, parameter.Scale);
                }
            }

            return dynamicParameters;
        }

        #endregion
    }
}