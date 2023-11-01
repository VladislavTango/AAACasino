using AAAcasino.ViewModels.Base;
using System.Collections.Generic;

namespace AAAcasino.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region base property main
        private IPageViewModel _selectedPageViewModel;
        private IList<IPageViewModel> _clientPageViewModels;
        public IList<IPageViewModel> ClientPageViewModels
        {
            get => _clientPageViewModels;
            set => Set(ref _clientPageViewModels, value);
        }
        public IPageViewModel SelectedPageViewModel
        {
            get => _selectedPageViewModel;
            set
            {
                Set(ref _selectedPageViewModel, value);
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Title => $"{SelectedPageViewModel?.Title}";
        #endregion
    }
}
