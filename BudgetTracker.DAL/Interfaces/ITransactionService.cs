using BudgetTracker.Models;

namespace BudgetTracker.Interfaces;
public interface ITransactionService : IBaseService<Transaction> {
	public IEnumerable<Transaction> GetAll(DateTime startDate, DateTime endDate);
}

