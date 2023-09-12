using apistudy.interfaces;
using apistudy.Models.Entityies;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static ServiceStack.LicenseUtils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace apistudy.Models.Detos
{
    public class ShopingCartDto
    {
        public int ProductId { get; set; }
        public int Id { get; set; }
       
        public int Count { get; set; }
        public string ApplicationUserId { get; set; }
    
        public double Price { get; set; }
        public string UserName { get; set; }
        public string ProductTitles { get; set; }
        public double TotalPriceoFcart  { get; set; }
        public double TotalQantity  { get; set; }
        //public static ShoppingCart  ConvertdetoTceatedobject(ShopingCartDto categoryDto)
        //{

        //    return new ShoppingCart { Id = categoryDto.Id, Count = categoryDto.Count, ProductId = categoryDto.ProductId, };


        //}
    }



    public class CreatedShopingCartDto
    {
        [JsonIgnore]
        public double Price { get; set; }

        public int ProductId { get; set; }
        public int Id { get; set; }

        public int Count { get; set; }
        public string ApplicationUserId { get; set; }


        public static ShoppingCart ConvertdetoTceatedobject(CreatedShopingCartDto categoryDto)
        {

            return new ShoppingCart { Id = categoryDto.Id, Count = categoryDto.Count, ProductId = categoryDto.ProductId,  ApplicationUserId= categoryDto.ApplicationUserId };


        }


    }








    }
