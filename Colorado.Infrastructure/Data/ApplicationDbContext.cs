using Colorado.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Colorado.Infrastructure.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<Cliente> Clientes { get; set; }
	}
}
