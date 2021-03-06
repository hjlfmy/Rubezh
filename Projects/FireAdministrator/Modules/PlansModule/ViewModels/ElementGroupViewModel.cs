﻿using System.Collections.ObjectModel;
using Infrustructure.Plans.Services;

namespace PlansModule.ViewModels
{
	public class ElementGroupViewModel : ElementBaseViewModel
	{
		private ElementsViewModel _elementsViewModel;

		public ElementGroupViewModel(ObservableCollection<ElementBaseViewModel> sourceElements, ElementsViewModel elementsViewModel, string alias)
		{
			Source = sourceElements;
			IsBold = true;
			_elementsViewModel = elementsViewModel;
			Group = alias;
			Name = LayerGroupService.Instance[alias];
			_isVisible = true;
			_isSelectable = true;
		}

		public string Name { get; private set; }
		public string Group { get; private set; }

		bool _isVisible;
		public bool IsVisible
		{
			get
			{
				return _isVisible;
			}
			set
			{
				_isVisible = value;
				foreach (var elementViewModel in _elementsViewModel.AllElements)
					if (elementViewModel.DesignerItem.Group == Group)
						elementViewModel.IsVisible = value;
				OnPropertyChanged("IsVisible");
			}
		}

		bool _isSelectable;
		public bool IsSelectable
		{
			get
			{
				return _isSelectable;
			}
			set
			{
				_isSelectable = value;
				foreach (var elementViewModel in _elementsViewModel.AllElements)
					if (elementViewModel.DesignerItem.Group == Group)
						elementViewModel.IsSelectable = value;
				OnPropertyChanged("IsSelectable");
			}
		}
	}
}