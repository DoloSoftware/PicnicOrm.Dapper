using System;
using System.Collections.Generic;

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
        /// <param name="parents"></param>
        /// <param name="shouldContinueThroughEmptyTables"></param>
        public virtual void Map(IGridReader gridReader, IDictionary<int, TParent> parents, bool shouldContinueThroughEmptyTables)
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
        /// <param name="childDictionary"></param>
        /// <param name="shouldContinueThroughEmptyTables"></param>
        protected void MapChildren(IGridReader gridReader, IDictionary<int, TChild> childDictionary, bool shouldContinueThroughEmptyTables)
        {
            //if we have nothing to map and we shouldn't continue reading because the query isn't reading the next table if the previous one had no results
            //then return
            if (childDictionary == null && !shouldContinueThroughEmptyTables)
            {
                return;
            }

            foreach (var childMapping in _childMappings)
            {
                childMapping.Map(gridReader, childDictionary, shouldContinueThroughEmptyTables);
            }
        }

        #endregion
    }
}