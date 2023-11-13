using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels
{
    class LogInViewModel : ViewModel, IPageViewModel
    {
        #region IPage property
        public string Title => "LogIn";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { return; }
        #endregion
        #region ViewModel property
        private string? _name = string.Empty;
        private string? _messengeLine = null;
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public string? MessengeLine
        {
            get => _messengeLine;
            set => Set(ref _messengeLine, value);
        }
        #endregion
        #region commands
        public ICommand LogInCommand { get; }
        private void OnLogInCommand(object parameter)
        {
            UserModel? user = UserExistence(parameter);

            if (user == null)
                MessengeLine = "Неправильный логин или пароль";
            else
            {
                MainViewModel.User = user;
                MainViewModel.SelectedPageViewModel = user.DefalutUser ?
                    MainViewModel.ClientPageViewModels[(int)NumberClientPage.USER_PAGE] :
                    MainViewModel.ClientPageViewModels[(int)NumberClientPage.ADMIN_PAGE];
                MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
                MainViewModel.SelectedPageViewModel.SetAnyModel(null);
            }
        }
        private bool CanLogInCommand(object parameter) => true;
        public ICommand SignUpCommand { get; }
        private void OnSignUpCommand(object parameter)
        {
            UserModel? user = UserExistence(parameter);

            if (user != null)
                MessengeLine = "Данный пользователь уже существует";
            else
            {
                var passBox = parameter as PasswordBox;
                MainViewModel.User = new UserModel(_name, passBox.Password);
                MainViewModel.User.Balance = 100.0d;
                MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.USER_PAGE];
                MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
                MainViewModel.SelectedPageViewModel.SetAnyModel(null);
                MainWindowViewModel.applicationContext.Add(MainViewModel.User);
                MainWindowViewModel.applicationContext.SaveChanges();
            }
        }
        private bool CanSignUpCommand(object parameter) => true;
        private UserModel? UserExistence(object pass)
        {
            var passBox = pass as PasswordBox;
            string _pass = passBox.Password;

            List<UserModel> list = new List<UserModel>();
            list = (from user in MainWindowViewModel.applicationContext.userModels.ToList()
                    where user.Username == _name && user.Password == _pass
                    select user).ToList();

            return list.Count == 1 ? list.First() : null;
        }
        #endregion
        public LogInViewModel()
        {
            LogInCommand = new LamdaCommand(OnLogInCommand, CanLogInCommand);
            SignUpCommand = new LamdaCommand(OnSignUpCommand, CanSignUpCommand);
        }
    }
}