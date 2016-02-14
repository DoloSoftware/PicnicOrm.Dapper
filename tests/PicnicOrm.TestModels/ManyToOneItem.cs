using System.Collections.Generic;

namespace PicnicOrm.TestModels
{
    public class ManyToOneItem
    {
        public int Id { get; set; }

        public IList<ParentItem> Parents { get; set; } 
    }
}
