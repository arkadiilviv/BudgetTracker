using BudgetTracker.Models;
using Microsoft.EntityFrameworkCore;


namespace BudgetTracker
{
	public class BudgetContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public string DbPath { get; }

		public BudgetContext()
		{
			var folder = AppDomain.CurrentDomain.BaseDirectory;
			var dbFolder = "Data";
			var dataPath = Directory.CreateDirectory(System.IO.Path.Join(folder, dbFolder));
			DbPath = System.IO.Path.Join(dataPath.FullName, "budget.db");
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite($"Data Source={DbPath}");
	}
}
