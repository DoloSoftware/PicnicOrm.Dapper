using System;
using System.Collections.Generic;
using System.Linq;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// </summary>
    public class OneToOneMapping<TParent, TChild> : BaseChildMapping<TParent, TChild>
        where TParent : class
        where TChild : class
    {
        #region Fields

        /// <summary>
        /// </summary>
        protected readonly Action<TParent, TChild> _childSetter;

        /// <summary>
        /// </summary>
        protected readonly Func<TParent, int> _parentChildKeySelector;

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="childKeySelector"></param>
        /// <param name="parentChildKeySelector"></param>
        /// <param name="childSetter"></param>
        public OneToOneMapping(Func<TChild, int> childKeySelector, Func<TParent, int> parentChildKeySelector, Action<TParent, TChild> childSetter)
            : base(childKeySelector)
        {
            _parentChildKeySelector = parentChildKeySelector;
            _childSetter = childSetter;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="parents"></param>
        /// <param name="shouldContinueThroughEmptyTables"></param>
        public override void Map(IGridReader gridReader, IDictionary<int, TParent> parents, bool shouldContinueThroughEmptyTables)
        {
            base.Map(gridReader, parents, shouldContinueThroughEmptyTables);
            IDictionary<int, TChild> childDictionary = null;

            var children = gridReader.Read<TChild>();

            if (children != null)
            {
                //if we have children then put them in a dictionary
                childDictionary = children.ToDictionary(_childKeySelector);
                MapParents(childDictionary, parents);
            }

            //map children with their children
            MapChildren(gridReader, childDictionary, shouldContinueThroughEmptyTables);
        }

        #endregion

        #region Private Methods

        private void MapParents(IDictionary<int, TChild> childDictionary, IDictionary<int, TParent> parents)
        {
            if (parents == null)
            {
                return;
            }

            //iterate through each parent and map parent/child relationship
            foreach (var parent in parents.Values)
            {
                var parentChildKey = _parentChildKeySelector(parent);
                if (childDictionary.ContainsKey(parentChildKey))
                {
                    _childSetter(parent, childDictionary[parentChildKey]);
                }
            }
        }

        #endregion
    }
}