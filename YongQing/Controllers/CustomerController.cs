using Microsoft.AspNetCore.Mvc;
using YongQing.Entities;
using YongQing.Services;

namespace YongQing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseApiController<Customer, String>
    {
        protected CustomerController(IDbService<Customer, string> NorthwindDbService) : base(NorthwindDbService)
        {
        }
    }
}
