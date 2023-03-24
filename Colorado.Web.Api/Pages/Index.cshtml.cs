using Colorado.Core.Entities;
using Colorado.Core.Interfaces;
using Colorado.Web.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colorado.Web.Api.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerRepositoryAsync _cliente;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRazorRenderService _renderService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ICustomerRepositoryAsync cliente, IUnitOfWork unitOfWork, IRazorRenderService renderService)
        {
            _logger = logger;
            _cliente = cliente;
            _unitOfWork = unitOfWork;
            _renderService = renderService;
        }
        public IEnumerable<Cliente> Clientes { get; set; }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            Clientes = await _cliente.GetAllAsync();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<Cliente>>(ViewData, Clientes)
            };
        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new Cliente()) });
            else
            {
                var thisCustomer = await _cliente.GetByIdAsync(id);
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", thisCustomer) });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _cliente.AddAsync(cliente);
                    await _unitOfWork.Commit();
                }
                else
                {
                    await _cliente.UpdateAsync(cliente);
                    await _unitOfWork.Commit();
                }
                Clientes = await _cliente.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", Clientes);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_CreateOrEdit", cliente);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var cliente = await _cliente.GetByIdAsync(id);
            await _cliente.DeleteAsync(cliente);
            await _unitOfWork.Commit();
            Clientes = await _cliente.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", Clientes);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
