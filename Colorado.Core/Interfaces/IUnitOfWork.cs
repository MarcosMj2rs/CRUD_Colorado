using System;
using System.Threading.Tasks;

namespace Colorado.Core.Interfaces
{
	public  interface IUnitOfWork : IDisposable
	{
		Task<int> Commit();
	}
}
