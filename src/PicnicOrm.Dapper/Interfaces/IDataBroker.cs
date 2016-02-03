using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PicnicOrm.Dapper.Mapping;

namespace PicnicOrm.Dapper
{
    public interface IDataBroker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="mapping"></param>
        void AddMapping(int key, IParentMapping mapping);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parentMappingKey"></param>
        /// <returns></returns>
        IEnumerable<T> ExecuteStoredProcedure<T>(string sql, int parentMappingKey)
            where T : class;
    }
}