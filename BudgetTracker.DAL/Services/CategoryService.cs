using BudgetTracker.Models;

namespace BudgetTracker.Services
{
	public class CategoryService
	{
		private BudgetContext _context;
		public CategoryService(BudgetContext context)
		{
			_context = context;
		}

		public IEnumerable<Category> GetAll()
		{
			return _context.Categories;
		}

		public async Task<Category> GetById(int id)
		{
			var category = await _context.Categories.FindAsync(id);
			if (category == null)
			{
				throw new KeyNotFoundException($"Category with ID {id} not found.");
			}
			return category;
		}

		public async Task AddAsync(Category category)
		{
			if (category == null)
			{
				throw new ArgumentNullException(nameof(category), "Category cannot be null.");
			}
			_context.Categories.Add(category);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCategory(Category category)
		{
			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();
		}

		public void UpdateModel(Category category)
		{
			_context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
		}
	}
}
