using AAAcasino.ViewModels.Base;
using System.Collections.Generic;

namespace AAAcasino.ViewModels
{
    internal class DefaultUserViewModel : ViewModel, IPageViewModel
    {
        #region IPageVM
        public string Title => $"{MainViewModel.User.Username}";
        public MainWindowViewModel MainViewModel { get; set; } = null!;
        public void SetAnyModel(object? model) { return; }
        #endregion
        private IPageViewModel _selectedPageViewModel;
        public IPageViewModel SelectedPageViewModel
        {
            get => _selectedPageViewModel;
            set
            {
                Set(ref _selectedPageViewModel, value);
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}
