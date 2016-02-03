using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;

namespace PicnicOrm.Dapper.Mapping
{
    /// <summary>
    /// </summary>
    /// <typeparam name="TParent"></typeparam>
    /// <typeparam name="TChild"></typeparam>
    public class ManyToManyMapping<TParent, TChild, TLink> : BaseChildMapping<TParent, TChild>
        where TParent : class
        where TChild : class
        where TLink : class
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

        #region Override Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="parents"></param>
        public override void Map(SqlMapper.GridReader gridReader, IDictionary<int, TParent> parents)
        {
            base.Map(gridReader, parents);

            var links = gridReader.Read<TLink>();
            if (links != null && links.Any())
            {
                var groupedLinks = links.GroupBy(_parentLinkKeySelector, _childLinkKeySelector).ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());
                var children = gridReader.Read<TChild>();
                if (children != null && children.Any())
                {
                    var childrenDictionary = children.ToDictionary(_childKeySelector);
                    MapChildren(gridReader, childrenDictionary);

                    var manyToManyGroupedChildren = childrenDictionary.ToGrouping(groupedLinks);
                    foreach (var parentKey in parents.Keys)
                    {
                        if (manyToManyGroupedChildren.ContainsKey(parentKey))
                        {
                            _parentSetter(parents[parentKey], manyToManyGroupedChildren[parentKey]);
                        }
                    }
                }
            }
        }

        #endregion
    }
}