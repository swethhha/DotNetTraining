using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Interfaces;

namespace ShopTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        // ================= GET ALL PRODUCTS =================
        // Everyone with Admin, User, or Seller role can see products
        [HttpGet]
        [Authorize(Roles = "Admin,User,Seller")]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        // ================= GET PRODUCT BY ID =================
        // Everyone with Admin, User, or Seller role
        [HttpGet("{id}", Name = "GetProductById")]
        [Authorize(Roles = "Admin,User,Seller")]
        public async Task<ActionResult<ProductResponseDTO>> GetById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // ================= CREATE PRODUCT =================
        // Only Admin and Seller can create products
        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<ProductResponseDTO>> Create([FromBody] ProductRequestDTO dto)
        {
            var product = await _service.CreateProductAsync(dto);

            // Return 201 Created with Location header pointing to GET by Id route
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }

        // ================= UPDATE PRODUCT =================
        // Only Admin and Seller can update products
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<ActionResult<ProductResponseDTO>> Update(int id, [FromBody] ProductRequestDTO dto)
        {
            var product = await _service.UpdateProductAsync(id, dto);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // ================= DELETE PRODUCT =================
        // Only Admin can delete products
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return Ok(new { message = "Product deleted successfully." });
        }
    }
}
