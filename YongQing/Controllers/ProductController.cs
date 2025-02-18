using Microsoft.AspNetCore.Mvc;
using YongQing.Entities;
using YongQing.Services;

namespace YongQing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseApiController<Product, int>
    {
        protected ProductController(IDbService<Product, int> northwindDbService)
        : base(northwindDbService)
        {
        }
    }
}
