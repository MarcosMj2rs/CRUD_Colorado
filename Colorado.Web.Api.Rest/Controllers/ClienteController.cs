using Colorado.Core.Entities;
using Colorado.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Colorado.Web.Api.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ClienteController : ControllerBase
	{
		private readonly ICustomerRepositoryAsync _cliente;
		private readonly IUnitOfWork _unitOfWork;

		public ClienteController(ICustomerRepositoryAsync cliente, IUnitOfWork unitOfWork)
		{
			_cliente = cliente;
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		[Route("GetByIdAsync")]
		public async Task<IActionResult> GetByIdAsync(int id)
		{
			var data = await _cliente.GetByIdAsync(id);
			return data is null ? null : Ok(data);
		}

		[HttpGet]
		[Route("GetAllAsync")]
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await _cliente.GetAllAsync();
			return data is null ? null : Ok(data);
		}

		[HttpPost]
		[Route("AddAsync")]
		public async Task<IActionResult> AddAsync([FromBody] Cliente cliente)
		{
			var data = await _cliente.AddAsync(cliente);
			await _unitOfWork.Commit();
			return data is null ? null : Ok(data.Id);
		}

		[HttpPost]
		[Route("UpdateAsync")]
		public async Task<IActionResult> UpdateAsync([FromBody] Cliente cliente)
		{
			try
			{
				await _cliente.UpdateAsync(cliente);
				await _unitOfWork.Commit();
			}
			catch { return null; }
			return Ok(true);
		}

		[HttpPost]
		[Route("DeleteAsync")]
		public async Task<IActionResult> DeleteAsync([FromBody] Cliente cliente)
		{
			try
			{
				await _cliente.DeleteAsync(cliente);
				await _unitOfWork.Commit();
			}
			catch { return null; }
			return Ok(true);
		}
	}
}
