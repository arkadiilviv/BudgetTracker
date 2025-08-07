namespace BudgetTracker.Interfaces
{
	public interface IBaseService<T>
	{
		Task AddAsync(T category);
		Task Delete(T category);
		IEnumerable<T> GetAll();
		Task<T> GetById(int id);
		void UpdateModel(T category);
	}
}
