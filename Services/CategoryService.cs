using BudgetTracker.Models;
using BudgetTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BudgetTracker.Services
{
	public class CategoryService
	{
		private BudgetContext _context;
		public CategoryService(BudgetContext context)
		{
			_context = context;
		}

		public ObservableCollection<CategoryViewModel> GetAll()
		{
			var categories = _context.Categories.Select(cat => new CategoryViewModel(_context, cat));
			return new ObservableCollection<CategoryViewModel>(categories);
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

		public async Task AddAsync(CategoryViewModel category)
		{
			if (category == null)
			{
				throw new ArgumentNullException(nameof(category), "Category cannot be null.");
			}
			_context.Categories.Add(category.Model);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCategory(CategoryViewModel category)
		{
			_context.Categories.Remove(category.Model);
			await _context.SaveChangesAsync();
		}
	}
}
