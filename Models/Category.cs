using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetTracker.Models
{
	public class Category
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Icon { get; set; }
		public uint ColorCode { get; set; }
		[NotMapped]
		public Avalonia.Media.Color Color
		{
			get
			{
				return Avalonia.Media.Color.FromUInt32(ColorCode);
			}
			set
			{
				ColorCode = value.ToUInt32();
			}
		}
		public override string ToString() => Name;
	}
}
