namespace BudgetTracker.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public Avalonia.Media.Color Color { get; set; }
		public override string ToString() => Name;
	}
}
