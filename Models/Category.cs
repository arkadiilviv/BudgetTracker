using Avalonia.Controls;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BudgetTracker.Models
{
	public class Category
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Icon { get; set; }
		public uint? ColorCode { get; set; }
		public Category() { }
		public Category(int id, string name, string? icon = null, uint colorCode = 0)
		{
			Id = id;
			Name = name;
			Icon = icon;
			ColorCode = colorCode;
		}
		public Category(Category category)
		{
			Id = category.Id;
			Name = category.Name;
			Icon = category.Icon;
			ColorCode = category.ColorCode;
		}
	}
}
