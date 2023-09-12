using Microsoft.AspNetCore.Mvc;
using apistudy.Models.Detos;
using apistudy.Servesess;
using Ecommerce_Api.interfaces;
using PagedList;
using apistudy.interfaces;

namespace apistudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly Unitofwork _unitofwork;

        public ShoppingController(Unitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedCarts(int pageNumber = 1)
        {
            // Assuming you have an IQueryable source of products (e.g., _unitofwork.Product.GetAllAsQueryable())
            var products =   _unitofwork.shopingCart.GetAllAsync();

            // Use the PaginationHelper to get paginated data
            var pagedProducts = _unitofwork.shopingCart.GetPagedData(products, pageNumber);

            return Ok(pagedProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCart(int id)
        {
            var product =   _unitofwork.shopingCart.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        
        [HttpGet("getcount_priceto_user")]
        public async Task<IActionResult> DetshopingCartTotalcount(string id)
        {
            var product =   _unitofwork.shopingCart.GetAllByQantityandCount(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("Get_ALLCustomer_Carts")]
        public IActionResult GetForCustomer( string Userid, int pageNumber = 1)
        {
            // Validate the input
            if (string.IsNullOrEmpty(Userid) || pageNumber < 1)
            {
                return BadRequest("Invalid input parameters.");
            }

            var products = _unitofwork.shopingCart.GetAllByUserId(Userid);

            if (products == null)
            {
                return NotFound();
            }

            // Implement pagination using a library like X.PagedList
            var pagedProducts = _unitofwork.shopingCart.GetPagedData(products, pageNumber);

            // Return the paged products
            return Ok(pagedProducts);
        }

        [HttpPost]
        public ActionResult<CreatedShopingCartDto> CreateProduct([FromForm] CreatedShopingCartDto productCreateDto)
        {
            var existingProduct = _unitofwork.shopingCart.Save(productCreateDto);
            return CreatedAtAction(nameof(GetShoppingCart), new { id = existingProduct.Id }, existingProduct);
        }

        [HttpPut]
        public ActionResult<CreatedShopingCartDto> UpdateProduct( [FromForm] CreatedShopingCartDto updatedProductDto)
        {
            var existingProduct = _unitofwork.shopingCart.Save(updatedProductDto);
            return CreatedAtAction(nameof(GetShoppingCart), new { id = existingProduct.Id }, existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
              _unitofwork.shopingCart.DeleteAsync(id);
            return NoContent();
        }
    }
}
