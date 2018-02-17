using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using PicnicOrm.Ado.Data;
using PicnicOrm.Data;
using PicnicOrm.Factories;

namespace PicnicOrm.Ado.Factories
{
    public class SqlGridReaderFactory : IGridReaderFactory
    {
        #region Fields

        private readonly IDictionary<Type, IEntityFactory> _entityFactories;

        #endregion

        #region Constructors

        public SqlGridReaderFactory()
        {
            _entityFactories = new Dictionary<Type, IEntityFactory>();
        }

        #endregion

        #region Interfaces

        /// <summary>
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
            var dataSet = new DataSet();
            var sqlConnection = (SqlConnection)dbConnection;
            var sqlCommand = GetSqlCommand(sqlConnection, storedProcName, parameters, dbTransaction, commandTimeout, commandType);

            var adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataSet);

            var gridReader = new SqlGridReader(dataSet, _entityFactories);

            return gridReader;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityFactory"></param>
        public void AddEntityFactory<T>(IEntityFactory<T> entityFactory) where T : class
        {
            _entityFactories.Add(typeof(T), entityFactory);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="dbTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private SqlCommand GetSqlCommand(SqlConnection sqlConnection, string storedProcName, IList<IDbParameter> parameters, IDbTransaction dbTransaction, int? commandTimeout, CommandType? commandType)
        {
            var sqlCommand = new SqlCommand(storedProcName, sqlConnection, dbTransaction as SqlTransaction);
            sqlCommand.CommandTimeout = commandTimeout ?? 120000;
            sqlCommand.CommandType = commandType ?? CommandType.StoredProcedure;

            return AddParameters(sqlCommand, parameters);
        }

        /// <summary>
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private SqlCommand AddParameters(SqlCommand sqlCommand, IList<IDbParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    var sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = parameter.Name;
                    sqlParameter.Value = parameter.Value;

                    if (parameter.DbType.HasValue)
                    {
                        sqlParameter.DbType = parameter.DbType.Value;
                    }

                    if (parameter.Direction.HasValue)
                    {
                        sqlParameter.Direction = parameter.Direction.Value;
                    }

                    if (parameter.Size.HasValue)
                    {
                        sqlParameter.Size = parameter.Size.Value;
                    }

                    if (parameter.Precision.HasValue)
                    {
                        sqlParameter.Precision = parameter.Precision.Value;
                    }

                    if (parameter.Scale.HasValue)
                    {
                        sqlParameter.Scale = parameter.Scale.Value;
                    }

                    sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            return sqlCommand;
        }

        #endregion
    }
}