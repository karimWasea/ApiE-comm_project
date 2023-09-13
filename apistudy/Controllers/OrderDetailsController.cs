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
    public class OrderDetailsController : ControllerBase
    {
        private readonly Unitofwork _unitofwork;
        //0a8dc315-345d-4928-b5a9-05ace821688a
        public OrderDetailsController(Unitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateorderDetails(int pageNumber = 1)
        {
            // Assuming you have an IQueryable source of products (e.g., _unitofwork.Product.GetAllAsQueryable())
            var products =   _unitofwork.orderDetails.GetAllAsync();

            // Use the PaginationHelper to get paginated data
            var pagedProducts = _unitofwork.orderDetails.GetPagedData(products, pageNumber);

            return Ok(pagedProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetorderDetails(int id)
        {
            var product =   _unitofwork.orderDetails.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        
      
        [HttpGet("Get_Customer_Dtails")]
        public IActionResult GetForCustomer( string Userid, int pageNumber = 1)
        {
            // Validate the input
            if (string.IsNullOrEmpty(Userid) || pageNumber < 1)
            {
                return BadRequest("Invalid input parameters.");
            }

            var products = _unitofwork.orderDetails.GetorderhederByuserId(Userid);

            if (products == null)
            {
                return NotFound();
            }

            // Implement pagination using a library like X.PagedList

            // Return the paged products
            return Ok(products);
        }

        [HttpPost]
        public ActionResult<OrderdetailDto> CreateDettails([FromForm] OrderdetailDto productCreateDto)
        {
            var existingProduct = _unitofwork.orderDetails.Save(productCreateDto);
            return CreatedAtAction(nameof(GetorderDetails), new { id = existingProduct.Id }, existingProduct);
        }

        [HttpPut]
        public ActionResult<OrderdetailDto> UpdateDettails([FromForm] OrderdetailDto productCreateDto)
        {
            var existingProduct = _unitofwork.orderDetails.Save(productCreateDto);
            return CreatedAtAction(nameof(GetorderDetails), new { id = existingProduct.Id }, existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
              _unitofwork.orderDetails.DeleteAsync(id);
            return NoContent();
        }
    }
}
