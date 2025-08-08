using BudgetTracker.Interfaces;
using BudgetTracker.Models;

namespace BudgetTracker.DAL.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly BudgetContext _context;
		public async Task AddAsync(Transaction item)
		{
			await _context.AddAsync(item);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Transaction item)
		{
			_context.Remove(item);
			await _context.SaveChangesAsync();
		}

		public IEnumerable<Transaction> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Transaction> GetAll(DateTime startDate, DateTime endDate)
		{
			return _context.Transactions
				.Where(t => t.Date >= startDate.Date && t.Date <= endDate.Date.AddDays(1))
				.OrderByDescending(t => t.Date);
		}

		public Task<Transaction> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdateModel(Transaction item)
		{
			throw new NotImplementedException();
		}

		public TransactionService(BudgetContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}
