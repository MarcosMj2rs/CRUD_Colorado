using Colorado.Core.Entities;
using Colorado.Core.Interfaces;
using Colorado.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Colorado.Infrastructure.Repositories
{
	public class CustomerRepositoryAsync : GenericRepositoryAsync<Cliente>, ICustomerRepositoryAsync
	{
		private readonly DbSet<Cliente> _cliente;
		public CustomerRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
		{
			_cliente = dbContext.Set<Cliente>();
		}
	}
}
