using System.Collections.Generic;

namespace PicnicOrm.TestModels
{
    public class ParentItem
    {
        public int Id { get; set; }

        public OneToOneItem Child { get; set; }

        public int ChildId { get; set; }

        public IList<OneToManyItem> Children { get; set; }

        public ManyToOneItem ManyToOneChild { get; set; }

        public int ManyToOneChildId { get; set; }
        
        public IList<ManyToManyItem> ManyToManyChildren { get; set; }  
    }
}
