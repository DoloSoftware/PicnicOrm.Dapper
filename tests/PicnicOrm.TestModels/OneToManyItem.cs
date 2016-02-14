namespace PicnicOrm.TestModels
{
    public class OneToManyItem
    {
        public int Id { get; set; }

        public ParentItem Parent { get; set; }

        public int ParentId { get; set; }
    }
}