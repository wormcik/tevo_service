using tevo_service.Entities;
using tevo_service.Services;
using Microsoft.AspNetCore.Mvc;

namespace tevo_service.Controllers

{
    [ApiController, Route("api/v1/tevo-service/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly TestService testService;
        private readonly AppDbContext appDbContext;
        public TestController(TestService testService, AppDbContext appDbContext)
        {
            this.testService = testService;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await testService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await testService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Test test)
        {
            var result = await testService.AddAsync(test);
            return CreatedAtAction(nameof(Get), new { id = result.TestId }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Test test)
        {
            var result = await testService.UpdateAsync(test);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await testService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

    }
}
