using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using YongQing.Entities;
using YongQing.Services;

namespace YongQing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDbService _customerDbService;
        public CustomerController(ICustomerDbService customerDbService)
        {
            _customerDbService = customerDbService;
        }

        // GET: api/<BaseApiController>
        [HttpGet]
        public async Task<IActionResult> Get() =>
            RunSafeAsync(await _customerDbService.GetAllAsync());

        // GET api/<BaseApiController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id) =>
            RunSafeAsync(await _customerDbService.GetByIdAsync(id));

        // POST api/<BaseApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer) =>
            RunSafeAsync(await _customerDbService.CreateAsync(customer));

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, [FromBody] Customer customer) =>
            RunSafeAsync(await _customerDbService.UpdateAsync(id, customer));

        // DELETE api/<BaseApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id) =>
            RunSafeAsync(await _customerDbService.DeleteAsync(id));

        private IActionResult RunSafeAsync<T>(T? result)
        {
            try
            {
                if (result == null)
                    return NotFound();

                if (result is List<Customer> entities && entities.Count == 0)
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
