using Microsoft.AspNetCore.Http;

namespace apistudy.Models.Detos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal Offer { get; set; }

    }

    public class ProductCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public IFormFile Image { get; set; }
        public decimal Offer { get; set; }
    }

    //public class ProductUpdateDto
    //{
    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public decimal Price { get; set; }
    //    public int CategoryId { get; set; }
    //    public int Quantity { get; set; }
    //    public IFormFile Image { get; set; }
    //    public decimal Offer { get; set; }
    //}


}
