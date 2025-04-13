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
    public class DemandController : ControllerBase
    {
        private readonly DemandService demandService;
        private readonly AppDbContext appDbContext;

        public DemandController(DemandService demandService, AppDbContext appDbContext)
        {
            this.demandService = demandService;
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DemandCreateModel model)
        {
            var result = await demandService.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<DemandDTO>> GetByUser([FromQuery] Guid userId)
        {
            var result = await demandService.GetByUserAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await demandService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetForSeller([FromQuery] Guid userId)
        {
            var result = await demandService.GetForSellerAsync(userId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBySeller([FromBody] DemandUpdateModel model)
        {
            var result = await demandService.UpdateBySellerAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Approve([FromBody] DemandActionModel model)
        {
            var result = await demandService.ApproveAsync(model.DemandId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Cancel([FromBody] DemandActionModel model)
        {
            var result = await demandService.CancelAsync(model.DemandId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddManually([FromBody] DemandCreateModel model)
        {
            var result = await demandService.AddManuallyAsync(model);
            return Ok(result);
        }
    }
}
