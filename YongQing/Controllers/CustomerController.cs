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
            RunSafe(await _customerDbService.GetAllAsync());

        // GET api/<CustomerApiController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(String id) =>
            RunSafe(await _customerDbService.GetByIdAsync(id));

        // POST api/<CustomerApiController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer) =>
            RunSafe(await _customerDbService.CreateAsync(customer));

        // PUT api/<ValuesController>/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, [FromBody] Customer customer) =>
            RunSafe(await _customerDbService.UpdateAsync(id, customer));

        // DELETE api/<CustomerApiController>/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id) =>
            RunSafe(await _customerDbService.DeleteAsync(id));

        private IActionResult RunSafe(ApiResult result)
        {
            try
            {
                if (result.ErrorMessage != string.Empty)
                    throw new Exception(result.ErrorMessage);  // 拋出資料層錯誤

                else if (result.Data == null)  // 檢查單筆搜尋
                    return NotFound();

                else if (result.Data is List<Customer> customers && customers.Count == 0)  // 檢查多筆搜尋
                    return NotFound();

                else if (result.Data is int statusCode)    // 檢查增加、更新、刪除
                    return statusCode > 0 ? Ok() : NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
