using DevExtreme.AspNet.Data;
using EntityLayer.Dapper;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevController : ControllerBase
    {
        private readonly MovieDbContext _context;
        private readonly DapperContext _dapperContext;
        public DevController(MovieDbContext context, DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        [HttpPost]
        [Route("GetData")]
        public object GetData(DataSourceLoadOptionsBase loadOptions)
        {
            return DataSourceLoader.Load(_context.Movies.FromSqlRaw("SELECT * FROM Movies").AsQueryable(), loadOptions);
        }
    }
}
