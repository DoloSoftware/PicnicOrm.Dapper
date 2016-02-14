using System;
using System.Collections.Generic;
using System.Linq;

using PicnicOrm.Data;

namespace PicnicOrm.Mapping
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TParent">ex: User</typeparam>
    /// <typeparam name="TChild">ex: Car</typeparam>
    /// <typeparam name="TLink">ex: UserCar</typeparam>
    public class ManyToManyMapping<TParent, TChild, TLink> : BaseChildMapping<TParent, TChild>
        where TParent : class where TChild : class where TLink : class
    {
        #region Fields

        /// <summary>
        /// </summary>
        protected readonly Func<TLink, int> _childLinkKeySelector;

        /// <summary>
        /// </summary>
        protected readonly Func<TLink, int> _parentLinkKeySelector;

        /// <summary>
        /// </summary>
        protected readonly Action<TParent, IEnumerable<TChild>> _parentSetter;

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        /// <param name="childKeySelector"></param>
        /// <param name="childLinkKeySelector"></param>
        /// <param name="parentKeyLinkSelector"></param>
        /// <param name="parentSetter"></param>
        public ManyToManyMapping(Func<TChild, int> childKeySelector, Func<TLink, int> childLinkKeySelector, Func<TLink, int> parentKeyLinkSelector, Action<TParent, IEnumerable<TChild>> parentSetter)
            : base(childKeySelector)
        {
            _childLinkKeySelector = childLinkKeySelector;
            _parentLinkKeySelector = parentKeyLinkSelector;
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

            //Organize the link entities by parent key
            var groupedLinks = GetGroupedLinks(gridReader);

            if (shouldContinueThroughEmptyTables || (groupedLinks != null && groupedLinks.Any()))
            {
                var children = gridReader.Read<TChild>();

                //map children and parents using the grouped links and return the children in a dictionary if there were any
                childDictionary = Map(children, groupedLinks, parents);
            }

            MapChildren(gridReader, childDictionary, shouldContinueThroughEmptyTables);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <returns></returns>
        private IDictionary<int, List<int>> GetGroupedLinks(IGridReader gridReader)
        {
            IDictionary<int, List<int>> groupedLinks = null;

            var links = gridReader.Read<TLink>();

            if (links != null)
            {
                groupedLinks = links.GroupBy(_parentLinkKeySelector, _childLinkKeySelector).ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
            }

            return groupedLinks != null && groupedLinks.Any() ? groupedLinks : null;
        }

        /// <summary>
        /// </summary>
        /// <param name="childDictionary"></param>
        /// <param name="parents"></param>
        /// <param name="groupedLinks"></param>
        private void MapParents(IDictionary<int, TChild> childDictionary, IDictionary<int, TParent> parents, IDictionary<int, List<int>> groupedLinks)
        {
            if (parents == null)
            {
                return;
            }

            //Organize the children entities by parent key (children can belong to more than one parent)
            var manyToManyGroupedChildren = childDictionary.ToGrouping(groupedLinks);

            //Map the children collections to their parents
            foreach (var parent in parents)
            {
                if (manyToManyGroupedChildren.ContainsKey(parent.Key))
                {
                    _parentSetter(parent.Value, manyToManyGroupedChildren[parent.Key]);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="children"></param>
        /// <param name="groupedLinks"></param>
        /// <param name="parents"></param>
        /// <returns></returns>
        private IDictionary<int, TChild> Map(IEnumerable<TChild> children, IDictionary<int, List<int>> groupedLinks, IDictionary<int, TParent> parents)
        {
            IDictionary<int, TChild> childDictionary = null;

            if (children != null && groupedLinks != null)
            {
                childDictionary = children.ToDictionary(_childKeySelector);

                MapParents(childDictionary, parents, groupedLinks);
            }

            return childDictionary != null && childDictionary.Any() ? childDictionary : null;
        }

        #endregion
    }
}