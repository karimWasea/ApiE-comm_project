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
                //var categories = await _context.Categories.ToListAsync(); // Fetch categories

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

                //// Attach category name to the product
                //var category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
                //if (category != null)
                //{
                //    product.CategoryName = category.Title;
                //}

                return Ok(product);
            }

            [HttpPost]
            public ActionResult<ProductCreateDto> CreateProduct([FromForm] ProductCreateDto productCreateDto)
            {
                //var product = new Product
                //{
                //    Title = productCreateDto.Title,
                //    Description = productCreateDto.Description,
                //    Price = productCreateDto.Price,
                //    CategoryId = productCreateDto.CategoryId,
                //    Quantity = productCreateDto.Quantity,
                //    Offer = productCreateDto.Offer
                //    ,
                //    Image = await SaveImage(productCreateDto.Image),

                //};










                //if (productCreateDto.Image != null)
                //{
                //    product.Image = await SaveImage(productCreateDto.Image);
                //}

                //_context.Products.Add(product);
                //await _context.SaveChangesAsync();

                //var productDto = new ProductDto
                //{
                //    Id = product.Id,
                //    Title = product.Title,
                //    Description = product.Description,
                //    Price = product.Price,
                //    Image = product.Image,
                //    CategoryId = product.CategoryId,
                //    Quantity = product.Quantity,
                //    Offer = product.Offer
                //};
                var existingProduct = _unitofwork.Product.Save(productCreateDto);
                return CreatedAtAction(nameof(GetProduct), new { id = existingProduct.Id }, existingProduct);
            }

            [HttpPut("{id}")]
            public ActionResult<ProductCreateDto> UpdateProduct(int id, [FromForm] ProductCreateDto updatedProductDto)
            {


                //UpdateProductProperties(existingProduct, updatedProductDto);

                //if (updatedProductDto.Image != null)
                //{
                //    existingProduct.Image = await SaveImage(updatedProductDto.Image);
                //}

                //await _context.SaveChangesAsync();
                var existingProduct = _unitofwork.Product.Save(updatedProductDto);
                return Ok(existingProduct);
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteProduct(int id)
            {
                //var product = await _context.Products.FindAsync(id);

                //if (product == null)
                //{
                //    return NotFound();
                //}

                //if (!string.IsNullOrEmpty(product.Image))
                //{
                //    DeleteImage(product.Image);
                //}

                //_context.Products.Remove(product);
                //await _context.SaveChangesAsync();
                _unitofwork.Product.DeleteAsync(id);
                return NoContent();
            }

            //private void UpdateProductProperties(Product existingProduct, ProductCreateDto updatedProductDto)
            //{
            //    existingProduct.Title = updatedProductDto.Title;
            //    existingProduct.Description = updatedProductDto.Description;
            //    existingProduct.Price = updatedProductDto.Price;
            //    existingProduct.CategoryId = updatedProductDto.CategoryId;
            //    existingProduct.Quantity = updatedProductDto.Quantity;
            //    existingProduct.Offer = updatedProductDto.Offer;
            //}


        }
    }
    

}


