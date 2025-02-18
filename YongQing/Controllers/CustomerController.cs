using Microsoft.AspNetCore.Mvc;
using YongQing.Entities;
using YongQing.Models;
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

        // GET: api/<CustomerApiController>
        [HttpGet]
        public async Task<IActionResult> Get() =>
            RunSafeAsync(await _customerDbService.GetAllAsync());

        // GET api/<CustomerApiController>/Name
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id) =>
            RunSafeAsync(await _customerDbService.GetByIdAsync(id));

        // POST api/<CustomerApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer) =>
            RunSafeAsync(await _customerDbService.CreateAsync(customer));

        // PUT api/<ValuesController>/Name
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, [FromBody] Customer customer) =>
            RunSafeAsync(await _customerDbService.UpdateAsync(id, customer));

        // DELETE api/<CustomerApiController>/Name
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id) =>
            RunSafeAsync(await _customerDbService.DeleteAsync(id));

        private IActionResult RunSafeAsync(ApiResult result)
        {
            try
            {
                if (result.ErrorMessage != string.Empty)
                    throw new Exception(result.ErrorMessage);

                if (result.Data == null)
                    return NotFound();

                if (result.Data is List<Customer> customers && customers.Count == 0)
                    return NotFound();

                if (result.Data is int statusCode)
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
