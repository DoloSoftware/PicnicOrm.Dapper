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
        public OneToManyMapping(Func<TChild, int> childKeySelector, Func<TChild, int> childParentKeySelector, Action<TParent, IEnumerable<TChild>> parentSetter) : base(childKeySelector)
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
        public override void Map(SqlMapper.GridReader gridReader, IDictionary<int, TParent> parents)
        {
            base.Map(gridReader, parents);

            var children = gridReader.Read<TChild>();

            if (children != null && children.Any())
            {
                //map our children to ourself before mapping ourself to our parent
                if (_childMappings.Any())
                {
                    var childDictionary = children.ToDictionary(_childKeySelector);
                    MapChildren(gridReader, childDictionary);
                }

                //group the children by their parents
                var childGrouping = children.GroupBy(_childParentKeySelector).ToDictionary(grouping => grouping.Key, grouping => grouping.ToList());

                //iterate through each parent and map their child collection to them
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