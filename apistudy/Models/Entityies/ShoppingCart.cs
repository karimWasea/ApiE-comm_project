using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static ServiceStack.LicenseUtils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apistudy.Models.Entityies
{
    public class ShoppingCart : BaseEntity
    {
 
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product product { get; set; }
        [Range(1, 1000, ErrorMessage = "Please Enter a value between 1 and 1000")]
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
        //[NotMapped]
        public double Price { get; set; } // Ensure Product is not null
    }
}
