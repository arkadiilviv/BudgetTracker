using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
	public class Transaction
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public Category Category { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }

	}
}
