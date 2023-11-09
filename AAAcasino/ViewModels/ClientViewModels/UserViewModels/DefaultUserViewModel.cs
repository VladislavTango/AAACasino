using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class DefaultUserViewModel : ViewModel, IPageViewModel
    {
        #region IPageVM
        public string Title => _userSelectedPage == null ? "UserPage" : _userSelectedPage.Title;
        public MainWindowViewModel MainViewModel { get; set; } = null!;
        public void SetAnyModel(object? model)
        {
            _userSelectedPage = MainViewModel.ClientPageViewModels[(int)NumberClientPage.PROFILE_PAGE];
        }
        #endregion
        #region Content component
        private IPageViewModel? _userSelectedPage = null;
        public IPageViewModel? UserSelectedPage
        {
            get => _userSelectedPage;
            set => Set(ref _userSelectedPage, value);
        }
        private Visibility _visableTopPanel;
        public Visibility VisableTopPanel
        {
            get => _visableTopPanel;
            set => Set(ref _visableTopPanel, value);
        }
        #endregion
        #region Commands
        public ICommand SwitchToProfileCommand { get; set; }
        private void OnSwitchToProfileCommand(object parameter)
        {
            UserSelectedPage = MainViewModel.ClientPageViewModels[(int)NumberClientPage.PROFILE_PAGE];
            UserSelectedPage.MainViewModel = MainViewModel;
        }
        private bool CanSwitchToProfileCommand(object parameter)
            => UserSelectedPage != MainViewModel.ClientPageViewModels[(int)NumberClientPage.PROFILE_PAGE];
        public ICommand SwitchToQuizzesCommand { get; set; }
        private void OnSwitchToQuizzesCommand(object parameter)
        {
            UserSelectedPage = MainViewModel.ClientPageViewModels[(int)NumberClientPage.QUIZZES_PAGE];
            UserSelectedPage.MainViewModel = MainViewModel;
        }
        private bool CanSwitchToQuizzesCommand(object parameter)
           => UserSelectedPage != MainViewModel.ClientPageViewModels[(int)NumberClientPage.QUIZZES_PAGE];
        public ICommand SwitchToSlotsCommand { get; set; }
        private void OnSwitchToSlotsCommand(object parameter)
        {
            UserSelectedPage = MainViewModel.ClientPageViewModels[(int)NumberClientPage.QUIZZES_PAGE];
            UserSelectedPage.MainViewModel = MainViewModel;
        }
        private bool CanSwitchToSlotsCommand(object parameter)
            => UserSelectedPage != MainViewModel.ClientPageViewModels[(int)NumberClientPage.SLOTS_PAGE];
        #endregion
        public DefaultUserViewModel()
        {
            _visableTopPanel = Visibility.Visible;
            SwitchToProfileCommand = new LamdaCommand(OnSwitchToProfileCommand, CanSwitchToProfileCommand);
            SwitchToQuizzesCommand = new LamdaCommand(OnSwitchToQuizzesCommand, CanSwitchToQuizzesCommand);
            SwitchToSlotsCommand = new LamdaCommand(OnSwitchToSlotsCommand, CanSwitchToSlotsCommand);
        }
    }
}