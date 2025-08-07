using BudgetTracker.Interfaces;
using BudgetTracker.Models;

namespace BudgetTracker.DAL.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly BudgetContext _context;
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

		public IEnumerable<Transaction> GetAll(DateTime startDate, DateTime endDate)
		{
			return _context.Transactions
				.Where(t => t.Date >= startDate && t.Date <= endDate)
				.OrderByDescending(t => t.Date);
		}

		public Task<Transaction> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdateModel(Transaction category)
		{
			throw new NotImplementedException();
		}

		public TransactionService(BudgetContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}
