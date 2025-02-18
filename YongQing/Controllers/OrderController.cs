using Microsoft.AspNetCore.Mvc;
using YongQing.Entities;
using YongQing.Services;

namespace YongQing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController<Order, int>
    {
        protected OrderController(IDbService<Order, int> northwindDbService)
        : base(northwindDbService)
        {
        }
    }
}
