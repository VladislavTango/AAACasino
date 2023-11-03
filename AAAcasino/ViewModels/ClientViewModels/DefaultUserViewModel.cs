using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels
{
    internal class DefaultUserViewModel : ViewModel, IPageViewModel
    {
        #region IPageVM
        public string Title => "User page";
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
        #region commands
        public ICommand ProfileOpenCommand { get; set; }
        private bool CanProfileOpenCommand(object parameter) => true;
        //ы
        private void OnProfileOpenCommand(object parameter)
        {
            SelectedPageViewModel = MainViewModel.SlotPageViewModels[(int)NumberClientPage.PROFILE_PAGE];
            SelectedPageViewModel.MainViewModel = MainViewModel;
        }
        #endregion
        public DefaultUserViewModel()
        {
            ProfileOpenCommand = new LamdaCommand(OnProfileOpenCommand, CanProfileOpenCommand);
        }
    }
}
