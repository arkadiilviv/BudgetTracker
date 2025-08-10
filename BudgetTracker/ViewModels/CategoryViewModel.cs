using Avalonia.Controls;
using BudgetTracker.Interfaces;
using BudgetTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BudgetTracker.ViewModels
{
	public partial class CategoryViewModel : ViewModelBase
	{
		private ICategoryService _categoryService;
		public Category Model { get; }
		[ObservableProperty]
		private string _name;
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
		public CategoryViewModel(Category model, ICategoryService categoryService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			Model = model ?? throw new ArgumentNullException(nameof(model));
			Name = model.Name;
			ColorCode = model.ColorCode ?? 0;
		}

		public static CategoryViewModel FromModel(Category category, ICategoryService categoryService)
		{
			return new CategoryViewModel(category, categoryService);
		}
	}
}
