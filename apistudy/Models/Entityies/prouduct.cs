namespace apistudy.Models.Entityies
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public string Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public int Quantity { get; set; }
        public decimal Offer { get; set; }


    }
}
