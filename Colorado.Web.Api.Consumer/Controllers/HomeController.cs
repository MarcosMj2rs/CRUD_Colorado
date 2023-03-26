using Colorado.Core.Entities;
using Colorado.Web.Api.Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Colorado.Web.Api.Consumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string apiUrl = " https://localhost:5001/api/Cliente/GetAllAsync";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET api/values
        public async Task<IActionResult> Index()
        {
            IEnumerable<ClienteModel> listaClientes = new List<ClienteModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaClientes = JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(apiResponse);
                }
            }
            return View(listaClientes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
