using DevexpressDemoVue.Models;
using DevexpressDemoVue.Service;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevexpressDemoVue.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IResponseGetaway _responseGetaway;
        public HomeController(ILogger<HomeController> logger, IResponseGetaway responseGetaway)
        {
            _logger = logger;
            _responseGetaway = responseGetaway;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public async Task<object> GetData(DataSourceLoadOptions loadOptions)
        {
            var resp = await _responseGetaway.SendTAsync(loadOptions, "api/Movies/GetData", HttpMethod.Post);
            //var resp = await _responseGetaway.SendTAsync(loadOptions, "api/Home/GetData", HttpMethod.Post);

            return resp;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}