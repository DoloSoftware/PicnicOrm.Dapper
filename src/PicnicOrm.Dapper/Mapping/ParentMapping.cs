using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParentMapping<T> : IParentMapping<T>
        where T : class
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IList<IChildMapping<T>> _childMappings;

        /// <summary>
        /// 
        /// </summary>
        private readonly Func<T, int> _keySelector;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySelector"></param>
        public ParentMapping(Func<T, int> keySelector)
        {
            _keySelector = keySelector;
            _childMappings = new List<IChildMapping<T>>();
        }

        #endregion

        #region Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridReader"></param>
        /// <returns></returns>
        public IEnumerable<T> Read(SqlMapper.GridReader gridReader)
        {
            var items = gridReader.Read<T>();

            if (items != null && items.Any())
            {
                var itemDictionary = items.ToDictionary(_keySelector);
                foreach (var childMapping in _childMappings)
                {
                    childMapping.Map(gridReader, itemDictionary);
                }
            }

            return items;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="childMapping"></param>
        public void AddMapping(IChildMapping<T> childMapping)
        {
            _childMappings.Add(childMapping);
        }

        #endregion
    }
}