using DevexpressDemoVue.Service;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace DevexpressDemoVue.Controllers
{
    public class GridController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IResponseGetaway _responseGetaway;
        public GridController(ILogger<HomeController> logger, IResponseGetaway responseGetaway)
        {
            _logger = logger;
            _responseGetaway = responseGetaway;
        }
        public IActionResult WithCheckBox()
        {
            return View();
        }
        [HttpGet]
        public async Task<object> GetData(DataSourceLoadOptions loadOptions)
        {
            var resp = await _responseGetaway.SendTAsync(loadOptions, "api/Movies/GetData", HttpMethod.Post);

            return resp;
        }
    }
}
