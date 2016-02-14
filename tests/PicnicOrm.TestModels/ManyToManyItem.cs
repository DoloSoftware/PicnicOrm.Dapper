using System.Collections.Generic;

namespace PicnicOrm.TestModels
{
    public class ManyToManyItem
    {
        #region Properties

        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public IList<ParentItem> Parents { get; set; }

        #endregion
    }
}