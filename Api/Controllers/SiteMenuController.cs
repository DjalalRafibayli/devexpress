using DevExtreme.AspNet.Data;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiteMenuController : Controller
    {
        private readonly MovieDbContext _context;

        public SiteMenuController(MovieDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("GetData")]
        public object GetData(DataSourceLoadOptionsBase loadOptions)
        {
            return DataSourceLoader.Load(_context.SiteMenus, loadOptions);
        }
    }
}
