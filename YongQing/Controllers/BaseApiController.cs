using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using YongQing.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YongQing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController<TEntity, TId> : ControllerBase where TEntity : class
    {
        private readonly IDbService<TEntity, TId> _dbService;
        protected BaseApiController(IDbService<TEntity, TId> dbService)
        {
            _dbService = dbService;
        }

        // GET: api/<BaseApiController>
        [HttpGet]
        public async Task<IActionResult> Get() =>
            RunSafeAsync(await _dbService.GetAllAsync());

        // GET api/<BaseApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(TId id) =>
            RunSafeAsync(await _dbService.GetByIdAsync(id));

        // POST api/<BaseApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TEntity entity) =>
            RunSafeAsync(await _dbService.CreateAsync(entity));

        // Patch api/<BaseApiController>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(TId id, [FromBody] JsonPatchDocument<TEntity> patchDoc) =>
            RunSafeAsync(await _dbService.PatchAsync(id, patchDoc));

        // DELETE api/<BaseApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(TId id) =>
            RunSafeAsync(await _dbService.DeleteAsync(id));

        private IActionResult RunSafeAsync<T>(T? result)
        {
            try
            {
                if (result == null)
                    return NotFound();

                if (result is List<TEntity> entities && entities.Count == 0)
                    return NotFound();

                if (result is int statusCode)
                {
                    return statusCode == 0 ? NotFound() : Ok();
                }

                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
