using tevo_service.Entities;
using tevo_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace tevo_service.Controllers

{
    [ApiController, Route("api/v1/tevo-service/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService clientService;
        private readonly AppDbContext appDbContext;
        public ClientController(ClientService clientService, AppDbContext appDbContext)
        {
            this.clientService = clientService;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await clientService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await clientService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Client client)
        {
            var result = await clientService.AddAsync(client);
            return CreatedAtAction(nameof(Get), new { id = result.ClientId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Client client)
        {
            var result = await clientService.UpdateAsync(client);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await clientService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet()]
        public async Task<IActionResult> Filter(
            [FromQuery] string? name,
            [FromQuery] string? surname,
            [FromQuery] string? tel,
            [FromQuery] string? adres)
        {
            var result = await clientService.FilterAsync(name, surname, tel, adres);
            return Ok(result);
        }
    }
}
