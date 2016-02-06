using System;
using System.Collections.Generic;
using System.Linq;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TChild"></typeparam>
    public class OneToManyMapping<TParent, TChild> : BaseChildMapping<TParent, TChild>
        where TParent : class
        where TChild : class
    {
        #region Fields

        /// <summary>
        /// </summary>
        protected readonly Func<TChild, int> _childParentKeySelector;

        /// <summary>
        /// </summary>
        protected readonly Action<TParent, IEnumerable<TChild>> _parentSetter;

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="childKeySelector"></param>
        /// <param name="childParentKeySelector"></param>
        /// <param name="parentSetter"></param>
        public OneToManyMapping(Func<TChild, int> childKeySelector, Func<TChild, int> childParentKeySelector, Action<TParent, IEnumerable<TChild>> parentSetter)
            : base(childKeySelector)
        {
            _childParentKeySelector = childParentKeySelector;
            _parentSetter = parentSetter;
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
                //if we have childrent then put them in a dictionary
                childDictionary = children.ToDictionary(_childKeySelector);
                MapParents(childDictionary, parents);
            }

            //map children with their children
            MapChildren(gridReader, childDictionary, shouldContinueThroughEmptyTables);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="childDictionary"></param>
        /// <param name="parents"></param>
        private void MapParents(IDictionary<int, TChild> childDictionary, IDictionary<int, TParent> parents)
        {
            if (parents != null)
            {
                //group the children by their parents
                var childGrouping = childDictionary.Values.GroupBy(_childParentKeySelector).ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());

                //iterate through each parent and map parent/child relationship
                foreach (var parentKey in parents.Keys)
                {
                    if (childGrouping.ContainsKey(parentKey))
                    {
                        _parentSetter(parents[parentKey], childGrouping[parentKey]);
                    }
                }
            }
        }

        #endregion
    }
}