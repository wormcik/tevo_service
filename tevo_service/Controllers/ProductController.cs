using Microsoft.AspNetCore.Mvc;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Services;
using System.Threading.Tasks;
using tevo_service.Models.DTOs;

namespace tevo_service.Controllers
{
    [ApiController]
    [Route("api/v1/tevo-service/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;
        private readonly AppDbContext appDbContext;

        public ProductController(ProductService productService, AppDbContext appDbContext)
        {
            this.productService = productService;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAll()
        {
            var result = await productService.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Add([FromBody] ProductModel model)
        {
            var result = await productService.AddAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Delete([FromBody] ProductModel model)
        {
            var result = await productService.DeleteAsync(model);
            return Ok(result);
        }
    }
}
