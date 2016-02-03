using System;
using System.Collections.Generic;

using Dapper;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TChild"></typeparam>
    public abstract class BaseChildMapping<TParent, TChild> : IChildMapping<TParent>
        where TParent : class
        where TChild : class
    {
        #region Fields

        /// <summary>
        /// </summary>
        protected readonly Func<TChild, int> _childKeySelector;

        /// <summary>
        /// </summary>
        protected readonly IList<IChildMapping<TChild>> _childMappings;

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="childKeySelector"></param>
        public BaseChildMapping(Func<TChild, int> childKeySelector)
        {
            _childKeySelector = childKeySelector;

            _childMappings = new List<IChildMapping<TChild>>();
        }

        #endregion

        #region Interfaces

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="items"></param>
        public virtual void Map(SqlMapper.GridReader gridReader, IDictionary<int, TParent> parents)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="childMapping"></param>
        public void AddMapping(IChildMapping<TChild> childMapping)
        {
            _childMappings.Add(childMapping);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="children"></param>
        protected void MapChildren(SqlMapper.GridReader gridReader, IDictionary<int, TChild> childDictionary)
        {
            foreach (var childMapping in _childMappings)
            {
                childMapping.Map(gridReader, childDictionary);
            }
        }

        #endregion
    }
}