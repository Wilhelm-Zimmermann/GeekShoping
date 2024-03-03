using GeekShoping.ProductAPI.Data.ValueObjects;
using GeekShoping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository 
                ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO product)
        {
            if(product == null) return BadRequest();

            var productReturn = await _productRepository.Create(product);

            return CreatedAtAction(nameof(Create), productReturn);
        }

        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO product)
        {
            if (product == null) return BadRequest();

            var productReturn = await _productRepository.Update(product);

            return Ok(productReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            var productDeleted = await _productRepository.DeleteById(id);

            if(!productDeleted) return BadRequest(false);

            return Ok(true);
        }
    }
}
