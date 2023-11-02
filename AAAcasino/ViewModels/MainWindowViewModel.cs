using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.Services.Database;
using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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

        private UserModel? _user = null;
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
            SelectedPageViewModel = ClientPageViewModels[0];
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
