using System.Data;

namespace PicnicOrm.Ado.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityFactory
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityFactory<T> : IEntityFactory
        where T : class
    {
        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        T Create(DataRow dataRow);

        #endregion
    }
}