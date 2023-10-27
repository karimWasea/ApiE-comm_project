using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apistudy.Models.Entityies
{
    public class OrderDetail : BaseEntity
    {
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }


       

        public string applicstionuserid { get; set; }
        [ForeignKey("applicstionuserid")]

        public ApplicationUser  applicationUser  { get; set; }
        [ForeignKey("Productid")]
        public Product    Product  { get; set; }

        public int Productid { get; set; }  
        
        
        public int Count { get; set; }
        public double Price { get; set; }
     
    }
}
