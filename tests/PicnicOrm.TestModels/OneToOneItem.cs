namespace PicnicOrm.TestModels
{
    public class OneToOneItem
    {
        public int Id { get; set; }
        
        public ParentItem Parent { get; set; }
    }
}
