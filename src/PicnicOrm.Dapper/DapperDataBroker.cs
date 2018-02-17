using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PicnicOrm.Data;
using PicnicOrm.Factories;
using PicnicOrm.Mapping;

namespace PicnicOrm.Dapper
{
    public class DapperDataBroker
    {
        /// <summary>
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// </summary>
        private readonly IGridReaderFactory _gridReaderFactory;
        
        /// <summary>
        /// </summary>
        private readonly IDbConnectionFactory _sqlConnectionFactory;

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="gridReaderFactory"></param>
        public DapperDataBroker(string connectionString, IGridReaderFactory gridReaderFactory)
            : this(connectionString, new SqlConnectionFactory(), gridReaderFactory)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlConnectionFactory"></param>
        /// <param name="gridReaderFactory"></param>
        public DapperDataBroker(string connectionString, IDbConnectionFactory sqlConnectionFactory, IGridReaderFactory gridReaderFactory)
        {
            _connectionString = connectionString;
            _sqlConnectionFactory = sqlConnectionFactory;
            _gridReaderFactory = gridReaderFactory;
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcName, Func<IGridReader, IEnumerable<T>> loader, IList<IDbParameter> parameters = null) where T : class
        {
            IEnumerable<T> list = null;
            using (var connection = _sqlConnectionFactory.Create(_connectionString))
            {
                using (var gridReader = _gridReaderFactory.Create(connection, storedProcName, parameters, commandType: CommandType.StoredProcedure))
                {
                    list = loader(gridReader);
                }
            }
            return list;
        }
    }
}
