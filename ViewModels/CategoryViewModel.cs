using Avalonia.Controls;
using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BudgetTracker.ViewModels
{
	public partial class CategoryViewModel : ViewModelBase
	{
		private BudgetContext _context;
		public Category Model { get; }
		[ObservableProperty]
		private string _name;
		[ObservableProperty]
		private string _icon;
		[ObservableProperty]
		private uint _colorCode;

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
		partial void OnColorCodeChanged(uint value)
		{
			if (!Design.IsDesignMode)
			{
				Model.ColorCode = value;
				_context.SaveChanges();
			}
		}
		partial void OnNameChanged(string value)
		{
			if (!Design.IsDesignMode)
			{
				Model.Name = value;
				_context.SaveChanges();
			}
		}
		public override string ToString() => Name;
		public CategoryViewModel()
		{
		}
		public CategoryViewModel(BudgetContext context, Category model)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			Model = model ?? throw new ArgumentNullException(nameof(model));
			Name = model.Name;
			Icon = model.Icon ?? string.Empty;
			ColorCode = model.ColorCode ?? 0;
		}
	}
}
