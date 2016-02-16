using System;
using System.Collections.Generic;
using System.Data;

using PicnicOrm.Ado.Factories;
using PicnicOrm.Data;

namespace PicnicOrm.Ado.Data
{
    public class SqlGridReader : IGridReader
    {
        #region Fields

        private DataSet _dataSet;
        private IDictionary<Type, IEntityFactory> _entityFactories;
        private IDictionary<string, int> _tablesRead;
        private IDictionary<string, DataTable> _dataTables; 

        #endregion

        #region Constructors

        public SqlGridReader(DataSet dataSet, IDictionary<Type, IEntityFactory> entityFactories)
        {
            _dataSet = dataSet;
            _entityFactories = entityFactories;
            _tablesRead = new Dictionary<string, int>();
            _dataTables = new Dictionary<string, DataTable>();

            if (_dataSet.Tables[0].TableName.StartsWith("Table"))
            {
                ToNamedTableSet(_dataSet);
            }
        }

        #endregion

        #region Interfaces

        public void Dispose()
        {
            _dataSet = null;
            _entityFactories = null;
            _tablesRead = null;
        }

        public IEnumerable<T> Read<T>() where T : class
        {
            var list = new List<T>();
            var key = typeof(T);
            var factory = (IEntityFactory<T>)_entityFactories[key];

            var table = GetTable(key.Name);

            if (table.Rows != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    var entity = factory.Create(row);
                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }

            return list;
        }

        #endregion

        #region Private Methods

        private DataTable GetTable(string tableName)
        {
            if (_tablesRead.ContainsKey(tableName))
            {
                var timesUsed = _tablesRead[tableName];
                _tablesRead[tableName] += 1;
                tableName = $"{tableName}{timesUsed}";
            }
            else
            {
                _tablesRead.Add(tableName, 1);
            }

            if (!_dataTables.ContainsKey(tableName))
            {
                throw new ArgumentException($"Table: ({tableName}) was not found in the DataSet.");
            }

            return _dataTables[tableName];

            //foreach (DataTable table in _dataSet.Tables)
            //{
            //    if (table.TableName.Equals(tableName))
            //    {
            //        returnTable = table;
            //        break;
            //    }
            //}
        }

        private void ToNamedTableSet(DataSet dataSet)
        {
            var usedNames = new Dictionary<string, int>();

            foreach (DataTable table in dataSet.Tables)
            {
                var lastColumnName = table.Columns[table.Columns.Count - 1].ColumnName;
                if (usedNames.ContainsKey(lastColumnName))
                {
                    var timesUsed = usedNames[lastColumnName];
                    usedNames[lastColumnName] += 1;
                    lastColumnName = $"{lastColumnName}{timesUsed}";
                }
                else
                {
                    usedNames.Add(lastColumnName, 1);
                }

                _dataTables.Add(lastColumnName, table);
            }
        }

        #endregion
    }
}