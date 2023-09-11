using apistudy.Models;
using apistudy.Models.Entityies;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace apistudy.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
  
    using apistudy.Models.Detos;
    using static System.Net.Mime.MediaTypeNames;
    using apistudy.Servesess;

    namespace YourProjectName.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly Unitofwork _unitofwork;

            public ProductsController(Unitofwork unitofwork)
            {
                _unitofwork = unitofwork;
            }
            [HttpGet]
            public ActionResult<IEnumerable<ProductDto>> GetProducts()
            {


                var products = _unitofwork.Product.GetAllAsync();




                return Ok(products);
            }

            [HttpGet("{id}")]
            public ActionResult<ProductDto> GetProduct(int id)
            {

                var product = _unitofwork.Product.GetByIdAsync(id);


                if (product == null)
                {
                    return NotFound();
                }

              

                return Ok(product);
            }

            [HttpPost]
            public ActionResult<ProductCreateDto> CreateProduct([FromForm] ProductCreateDto productCreateDto)
            {
              







          
                var existingProduct = _unitofwork.Product.Save(productCreateDto);
                return CreatedAtAction(nameof(GetProduct), new { id = existingProduct.Id }, existingProduct);
            }

            [HttpPut("{id}")]
            public ActionResult<ProductCreateDto> UpdateProduct(int id, [FromForm] ProductCreateDto updatedProductDto)
            {


                var existingProduct = _unitofwork.Product.Save(updatedProductDto);
                return Ok(existingProduct);
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteProduct(int id)
            {
                
                _unitofwork.Product.DeleteAsync(id);
                return NoContent();
            }

      


        }
    }
    

}


