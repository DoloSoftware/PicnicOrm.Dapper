using System.Data;

namespace PicnicOrm.Dapper.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbParameter : IDbParameter
    {
        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public DbParameter(string name, object value, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            Name = name;
            Value = value;
            DbType = dbType;
            Direction = direction;
            Size = size;
            Precision = precision;
            Scale = scale;
        }

        #endregion

        #region Properties

        /// <summary>
        /// </summary>
        public DbType? DbType { get; private set; }

        /// <summary>
        /// </summary>
        public ParameterDirection? Direction { get; private set; }

        /// <summary>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// </summary>
        public byte? Precision { get; private set; }

        /// <summary>
        /// </summary>
        public byte? Scale { get; private set; }

        /// <summary>
        /// </summary>
        public int? Size { get; private set; }

        /// <summary>
        /// </summary>
        public object Value { get; private set; }

        #endregion
    }
}