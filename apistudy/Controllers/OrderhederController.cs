using Microsoft.AspNetCore.Mvc;
using apistudy.Models.Detos;
using apistudy.Servesess;
using Ecommerce_Api.interfaces;
using PagedList;
using apistudy.interfaces;
using Ecommerce_Api.Models.Detos;

namespace apistudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderhederController : ControllerBase
    {
        private readonly Unitofwork _unitofwork;
        //0a8dc315-345d-4928-b5a9-05ace821688a
        public OrderhederController(Unitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateorderHeders(int pageNumber = 1)
        {
            // Assuming you have an IQueryable source of products (e.g., _unitofwork.Product.GetAllAsQueryable())
            var products =   _unitofwork.orderHeder.GetAllAsync();

            // Use the PaginationHelper to get paginated data
            var pagedProducts = _unitofwork.orderHeder.GetPagedData(products, pageNumber);

            return Ok(pagedProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetorderHeder(int id)
        {
            var product =   _unitofwork.orderHeder.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        
      
        [HttpGet("Get_Customer_Orderheder")]
        public IActionResult GetForCustomer( string Userid, int pageNumber = 1)
        {
            // Validate the input
            if (string.IsNullOrEmpty(Userid) || pageNumber < 1)
            {
                return BadRequest("Invalid input parameters.");
            }

            var products = _unitofwork.orderHeder.GetorderhederByuserId(Userid);

            if (products == null)
            {
                return NotFound();
            }

            // Implement pagination using a library like X.PagedList

            // Return the paged products
            return Ok(products);
        }

        [HttpPost]
        public ActionResult<OrderHeaderDTO> CreateProduct([FromForm]  OrderHeaderDTO productCreateDto)
        {
            var existingProduct = _unitofwork.orderHeder.Save(productCreateDto);
            return CreatedAtAction(nameof(GetPaginateorderHeders), new { id = existingProduct.Id }, existingProduct);
        }

        [HttpPut]
        public ActionResult<OrderHeaderDTO> UpdateProduct([FromForm] OrderHeaderDTO productCreateDto)
        {
            var existingProduct = _unitofwork.orderHeder.Save(productCreateDto);
            return CreatedAtAction(nameof(GetPaginateorderHeders), new { id = existingProduct.Id }, existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
              _unitofwork.orderHeder.DeleteAsync(id);
            return NoContent();
        }
    }
}
