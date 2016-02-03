using System;
using System.Collections.Generic;
using System.Linq;

using Dapper;

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
        public OneToOneMapping(Func<TChild, int> childKeySelector, Func<TParent, int> parentChildKeySelector, Action<TParent, TChild> childSetter) : base(childKeySelector)
        {
            _parentChildKeySelector = parentChildKeySelector;
            _childSetter = childSetter;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="gridReader"></param>
        /// <param name="items"></param>
        public override void Map(SqlMapper.GridReader gridReader, IDictionary<int, TParent> parents)
        {
            base.Map(gridReader, parents);

            var children = gridReader.Read<TChild>();

            //TODO: Figure out how we continue reading children tables so that we don't read from the
            //wrong tables when moving to our siblings
            if (children != null && children.Any())
            {
                var childDictionary = children.ToDictionary(_childKeySelector);

                //map our children to ourselves before mapping ourself to our parent
                MapChildren(gridReader, childDictionary);

                //iterate through each parent and map their child based on the foreign key in the parent
                foreach (var parent in parents.Values)
                {
                    var parentChildKey = _parentChildKeySelector(parent);
                    if (childDictionary.ContainsKey(parentChildKey))
                    {
                        _childSetter(parent, childDictionary[parentChildKey]);
                    }
                }
            }
        }

        #endregion
    }
}