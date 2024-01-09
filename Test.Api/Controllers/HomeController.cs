using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using EntityLayer.Models;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Dynamic.Core;

using System.ComponentModel;

namespace Test.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly MovieDbContext _context;

        public HomeController(MovieDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("GetData")]
        public object GetData(DataSourceLoadOptionsBase loadOptions)
        {
            var model = new { test = "", aa = "" };
            return DataSourceLoader.Load(_context.Movies, loadOptions);
        }
        public class Test
        {
            public int test { get; set; }
        }
    }
}
