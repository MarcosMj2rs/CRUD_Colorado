using Colorado.Core.Entities;
using Colorado.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

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
		public async Task<IActionResult> GetByIdAsync([FromBody] Cliente cliente)
		{
			try
			{
				var data = await _cliente.GetByIdAsync(cliente.Id);
				return Ok(data);
			}
			catch (WebException ex) { return BadRequest(ex.Message); }
			catch (Exception ex) { return BadRequest(ex.Message); }
		}

		[HttpGet]
		[Route("GetAllAsync")]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				var data = await _cliente.GetAllAsync();
				return Ok(data);
			}
			catch (WebException ex) { return BadRequest(ex.Message); }
			catch (Exception ex) { return BadRequest(ex.Message); }
		}

		[HttpPost]
		[Route("AddAsync")]
		public async Task<IActionResult> AddAsync([FromBody] Cliente cliente)
		{
			try
			{
				var data = await _cliente.AddAsync(cliente);
				await _unitOfWork.Commit();
				return Ok(data);
			}
			catch (WebException ex) { return BadRequest(ex.Message); }
			catch (Exception ex) { return BadRequest(ex.Message); }
		}

		[HttpPut]
		[Route("UpdateAsync")]
		public async Task<IActionResult> UpdateAsync([FromBody] Cliente cliente)
		{
			try
			{
				await _cliente.UpdateAsync(cliente);
				await _unitOfWork.Commit();
			}
			catch (WebException ex) { return BadRequest(ex.Message); }
			catch (Exception ex) { return BadRequest(ex.Message); }
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
			catch (WebException ex) { return BadRequest(ex.Message); }
			catch (Exception ex) { return BadRequest(ex.Message); }
			return Ok(true);
		}

		[HttpGet]
		[Route("GetNewCode")]
		public async Task<string> GetNewCode()
		{
			return await Task.Run(() =>
			{
				return (Guid.NewGuid()
							.ToString()
							.ToUpper()
							.Replace("-", string.Empty)
							.Substring(0, 19));
			});
		}
	}
}
