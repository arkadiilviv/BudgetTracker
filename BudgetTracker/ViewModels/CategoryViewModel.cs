using Avalonia.Controls;
using BudgetTracker.Models;
using BudgetTracker.Services;
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
		private CategoryService _categoryService;
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
				_categoryService.UpdateModel(Model);
			}
		}
		partial void OnNameChanged(string value)
		{
			if (!Design.IsDesignMode)
			{
				Model.Name = value;
				_categoryService.UpdateModel(Model);
			}
		}
		public override string ToString() => Name;
		public CategoryViewModel()
		{
		}
		public CategoryViewModel(Category model, CategoryService categoryService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			Model = model ?? throw new ArgumentNullException(nameof(model));
			Name = model.Name;
			Icon = model.Icon ?? string.Empty;
			ColorCode = model.ColorCode ?? 0;
		}

		public static CategoryViewModel FromModel(Category category, CategoryService categoryService)
		{
			return new CategoryViewModel(category, categoryService);
		}
	}
}
