using apistudy.Atrubuts;
using apistudy.Models.Entityies;
using apistudy.Seting;
using apistudy.Utillites;

using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apistudy.Models.Detos
{
    public class ProductDto
    {
        //      public static FileUploadService _fileUploadService;
        //       public ProductDto(FileUploadService fileUploadService)
        //{
        //          _fileUploadService = fileUploadService;
        //      }

        public int Id { get; set; }
        public   List< int> productimgidId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public double Offer { get; set; }




    }

    public class ProductCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public double Price { get; set; }
        [Required]

        public int CategoryId { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        //public IFormFile Image { get; set; }
        public double Offer { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]

        public int productimgidId { get; set; }
        [AllowedExtensions(FileSettings.AllowedExtensions),
         MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        [Required]
        public IFormFileCollection IFormFileCollectionImageUrls { get; set; }
        public static Product ConvertdetoTceatedobject(ProductCreateDto entity)
        {
            //var imgesuloded = FileUploadService.UploadFiles(entity.IFormFileCollectionImageUrls);


            return new Product
            {
                Id = entity.Id,

                Title = entity.Title,
                Description = entity.Description,
                Price = entity.Price,
                //ProductImages = imgesuloded.Select(imgUrls=>imgUrls).ToList(),
                CategoryId = entity.CategoryId,
                Offer = entity.Offer,
                Quantity = entity.Quantity,

            };
        }

    }

}
