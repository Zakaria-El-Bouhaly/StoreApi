using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Dto;
using Service.Services;
using Service.Exceptions;
using Microsoft.AspNetCore.Authorization;



namespace StoreAPI.Controllers
{
    //[Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                List<Product> products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("an error occurred");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                Product? product = await _productService.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("an error occurred");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product)
        {
            try
            {

                Product newProduct = await _productService.CreateProduct(product);
                return Ok(newProduct);
            }
            catch (CategoryNotFoundException ex)
            {
                return BadRequest("category not found");
            }

            catch (Exception ex)
            {
                return BadRequest("cannot create product");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest("id does not match");
            }

            try
            {
                Product? newProduct = await _productService.UpdateProduct(id, product);
                if (newProduct == null)
                {
                    return NotFound();
                }
                return Ok(newProduct);
            }
            catch (CategoryNotFoundException ex)
            {
                return BadRequest("category not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                Product? product = await _productService.DeleteProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest("cannot delete product");
            }
        }
    }
}

