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
            var sqlCommand = new SqlCommand(storedProcName, sqlConnection, dbTransaction as SqlTransaction);
            sqlCommand.CommandTimeout = commandTimeout ?? 120000;
            sqlCommand.CommandType = commandType ?? CommandType.StoredProcedure;

            var adapter = new SqlDataAdapter(sqlCommand);
            adapter.Fill(dataSet);

            var gridReader = new SqlGridReader(dataSet, _entityFactories);

            return gridReader;
        }

        #endregion

        #region Public Methods

        public void AddEntityFactory<T>(IEntityFactory<T> entityFactory) where T : class
        {
            _entityFactories.Add(typeof(T), entityFactory);
        }

        #endregion
    }
}