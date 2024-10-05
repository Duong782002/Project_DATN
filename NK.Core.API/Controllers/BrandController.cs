using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NK.Core.Business.Model;
using NK.Core.Business.Service;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;
        private readonly AppDbContext _dbContext;
        public BrandController(IBrandService brandService, IProductService productService, AppDbContext dbContext)
        {
            _brandService = brandService;
            _productService = productService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("getcount")]
        public async Task<ActionResult<IEnumerable<BrandViewDto>>> GetBrandCount()
        {
            var brands = await _brandService.GetBrandCount(5);
            var brandDtos = new List<BrandViewDto>();

            foreach(var brand in brands)
            {
                var productImage = await _dbContext.Products
                                            .Include(p => p.Brand)
                                            .Where(p => p.BrandId == brand.Id && p.ProductImage != null)
                                            .FirstOrDefaultAsync();

                if(productImage != null)
                {
                    var brandDto = new BrandViewDto()
                    {
                        Id = brand.Id,
                        Name = brand.Name,
                        Image = Url.Action(nameof(GetProductImage), new { id = productImage.Id }) ?? string.Empty
                    };

                    brandDtos.Add(brandDto);
                }
            }

            return brandDtos;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(string id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if(brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(BrandDto brandDto)
        {
            if(brandDto == null)
            {
                return  BadRequest("Brand object is null");
            }

            var existingBrand = await _brandService.GetByNameBrandAsync(brandDto.name);
            if(existingBrand != null)
            {
                return Conflict("Brand with the same name already exist");
            }

            var brand = new Brand()
            {

                Name = brandDto.name
            };

            await _brandService.AddBrandAsync(brand);
            return CreatedAtAction("GetBrand", new {id = brand.Id}, brand);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(string id, BrandDto brandDto)
        {
            if(brandDto == null)
            {
                return BadRequest("Brand object is null");
            }

            var existingBrand = await _brandService.GetBrandByIdAsync(id);
            if(existingBrand == null)
            {
                return NotFound("Brand not found");
            }

            var checkBrand = await _brandService.GetByNameBrandAsync(brandDto.name);
            if(checkBrand != null && checkBrand != existingBrand)
            {
                return Conflict("Brand with the same Name already exist");
            }

            existingBrand.Name = brandDto.name;
            await _brandService.UpdateBrandAsync(existingBrand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if(brand == null)
            {
                return NotFound();
            }

            await _brandService.RemoveBrandAsync(brand);
            return NoContent();
        }

        // GET: api/products/1/image
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetProductImage(string id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null || product.ProductImage == null)
            {
                return NotFound();
            }

            return File(product.ProductImage, "image/jpeg");
        }
    }
}
