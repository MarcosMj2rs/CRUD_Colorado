using Colorado.WebApiConsumer.MVC.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Colorado.WebApiConsumer.MVC.Controllers
{
    public class ClientesController : ControllerBase
    {
        private readonly string apiUrl = " https://localhost:5001/api/Cliente/GetAllAsync";

        public async Task<IActionResult> Index()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaClientes = JsonConvert.DeserializeObject<List<Cliente>>(apiResponse);
                }
            }
            return Ok(listaClientes);
        }
    }
}
