namespace apistudy.Models.Entityies
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}