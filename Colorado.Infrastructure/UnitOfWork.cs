﻿using Colorado.Core.Interfaces;
using Colorado.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Colorado.Infrastructure
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;
		private bool disposed;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}
		public async Task<int> Commit()
		{
			return await _dbContext.SaveChangesAsync();
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					_dbContext.Dispose();
				}
			}
			disposed = true;
		}
	}
}
