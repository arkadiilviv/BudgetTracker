using BudgetTracker.Interfaces;
using BudgetTracker.Models;

namespace BudgetTracker.DAL.Services
{
	public class TransactionService : ITransactionService
	{
		public Task AddAsync(Transaction category)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Transaction category)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Transaction> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Transaction> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdateModel(Transaction category)
		{
			throw new NotImplementedException();
		}
	}
}
