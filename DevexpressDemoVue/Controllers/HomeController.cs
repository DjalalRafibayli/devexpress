using DevexpressDemoVue.DxModel;
using DevexpressDemoVue.Models;
using DevexpressDemoVue.Models.Entity;
using DevexpressDemoVue.Service;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

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
        public IActionResult Dev()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<object> GetData([FromBody] GridDxModel loadOptions)
        {
            var resp = await _responseGetaway.SendTAsync(loadOptions, "api/Movies/GetData", HttpMethod.Post);
            //var resp = await _responseGetaway.SendTAsync(loadOptions, "api/Home/GetData", HttpMethod.Post);

            return Json(JsonConvert.DeserializeObject<DxGridResponseModel<Movie>>(resp));
        }
        [HttpGet]
        public async Task<object> GetDataDev(DataSourceLoadOptions loadOptions)
        {
            var resp = await _responseGetaway.SendTAsync(loadOptions, "api/Dev/GetData", HttpMethod.Post);

            return resp;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}