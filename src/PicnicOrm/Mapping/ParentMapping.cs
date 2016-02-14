using System;
using System.Collections.Generic;
using System.Linq;

using PicnicOrm.Data;

namespace PicnicOrm.Mapping
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    public class ParentMapping<TParent> : IParentMapping<TParent>
        where TParent : class
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly IList<IChildMapping<TParent>> _childMappings;

        /// <summary>
        /// </summary>
        private readonly Func<TParent, int> _keySelector;

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="keySelector"></param>
        public ParentMapping(Func<TParent, int> keySelector)
        {
            _keySelector = keySelector;
            _childMappings = new List<IChildMapping<TParent>>();
        }

        #endregion

        #region Interfaces

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="shouldContinueThroughEmptyTables"></param>
        /// <returns></returns>
        public IEnumerable<TParent> Read(IGridReader gridReader, bool shouldContinueThroughEmptyTables = true)
        {
            IList<TParent> parentList = null;
            var parents = gridReader.Read<TParent>();

            if (parents != null)
            {
                parentList = parents.ToList();
                var parentDictionary = parentList.ToDictionary(_keySelector);

                MapChildren(gridReader, parentDictionary, shouldContinueThroughEmptyTables);
            }

            return parentList ?? new List<TParent>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="childMapping"></param>
        public void AddMapping(IChildMapping<TParent> childMapping)
        {
            _childMappings.Add(childMapping);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="parents"></param>
        /// <param name="shouldContinueThroughEmptyTables"></param>
        protected void MapChildren(IGridReader gridReader, IDictionary<int, TParent> parents, bool shouldContinueThroughEmptyTables)
        {
            foreach (var childMapping in _childMappings)
            {
                childMapping.Map(gridReader, parents, shouldContinueThroughEmptyTables);
            }
        }

        #endregion
    }
}