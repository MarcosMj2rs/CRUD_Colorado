using Colorado.Core.Entities;
using Colorado.Web.Api.Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Colorado.Web.Api.Consumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string apiUrlHome = "https://localhost:5001/api/Cliente/GetAllAsync";
        private readonly string apiUrlCreate = "https://localhost:5001/api/Cliente/AddAsync";
        private readonly string apiUrlGetById = "https://localhost:5001/api/Cliente/GetByIdAsync";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ClienteModel> listaClientes = new List<ClienteModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrlHome))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaClientes = JsonConvert.DeserializeObject<IEnumerable<ClienteModel>>(apiResponse);
                }
            }
            return View(listaClientes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClienteModel cliente)
        {
            if (cliente == null)
                return View();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrlCreate);
                var postTask = await httpClient.PostAsJsonAsync<ClienteModel>(httpClient.BaseAddress.AbsoluteUri, cliente);

                if (postTask.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Erro no Processar página.");
            return View(cliente);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(ClienteModel cliente)
        {
            if (cliente.Id == 0)
                return BadRequest();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrlGetById);
                var postTask = await httpClient.PostAsJsonAsync<ClienteModel>(httpClient.BaseAddress.AbsoluteUri, cliente);

                if (postTask.IsSuccessStatusCode)
                    return View(cliente);
            }

            ModelState.AddModelError(string.Empty, "Erro no Processar página.");
            return RedirectToAction("edit");
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
