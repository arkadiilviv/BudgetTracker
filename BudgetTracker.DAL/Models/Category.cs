using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
	public class Category
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
		public uint? ColorCode { get; set; }
		public Category() { }
		public Category(int id, string name, uint colorCode = 0)
		{
			Id = id;
			Name = name;
			ColorCode = colorCode;
		}
		public Category(Category category)
		{
			Id = category.Id;
			Name = category.Name;
			ColorCode = category.ColorCode;
		}

		public override string ToString()
		{
			return Name ?? "Unnamed Category";
		}
	}
}
