using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.Services.Database;
using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace AAAcasino.ViewModels
{
    enum NumberClientPage
    {
        LOGIN_PAGE = 0,
        USER_PAGE = 1,
        PROFILE_PAGE = 2,
        QUIZZES_PAGE = 3,
        SLOTS_PAGE = 4,
        ADMIN_PAGE = 5,
        CREATION_PANEL_PAGE = 6,
    }
    enum NumberSlotPage
    {
        MINESLOT_PAGE = 0,
    }
    internal class MainWindowViewModel : ViewModel
    {
        #region base property main
        private IPageViewModel _selectedPageViewModel;
        private IList<IPageViewModel> _clientPageViewModels;
        private IList<IPageViewModel> _slotPageViewModels;
        public IList<IPageViewModel> ClientPageViewModels
        {
            get => _clientPageViewModels;
            set => Set(ref _clientPageViewModels, value);
        }
        public IList<IPageViewModel> SlotPageViewModels
        {
            get => _slotPageViewModels;
            set => Set(ref _slotPageViewModels, value);
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

        private UserModel? _user = new UserModel(string.Empty, string.Empty);
        public UserModel? User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        private bool _init = true;
        private Visibility _imgVis = Visibility.Visible;
        public Visibility ImgVis
        {
            get => _imgVis;
            set => Set(ref _imgVis, value);
        }
        public ICommand InitCommand { get; }
        private bool CanInitCommand(object parameter) => _init;
        private void OnInitCommand(object parameter)
        {
            SelectedPageViewModel = ClientPageViewModels[(int)NumberClientPage.LOGIN_PAGE];
            SelectedPageViewModel.MainViewModel = this;
            _init = false;
            ImgVis = Visibility.Collapsed;
        }

        public static ApplicationContext applicationContext { get; set; } = new ApplicationContext();
        public MainWindowViewModel()
        {
            InitCommand = new LamdaCommand(OnInitCommand, CanInitCommand);
        }
    }
}
