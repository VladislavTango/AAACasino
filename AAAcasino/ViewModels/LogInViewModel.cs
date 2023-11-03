using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace AAAcasino.ViewModels
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
        private string? _pass = string.Empty;
        private string? _passSec = string.Empty;
        private string? _messengeLine = null;
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        public string? Pass
        {
            get => _pass;
            set => Set(ref _pass, value);
        }
        public string PassSec
        {
            get => _passSec;
            set => Set(ref _passSec, value);
        }
        public string? MessengeLine
        {
            get => _messengeLine;
            set => Set(ref _messengeLine, value);
        }
        #endregion
        #region commands
        private bool СheckСondition()
        {
            if ((_name.Length < 4 || _pass.Length < 4))
            {
                MessengeLine = "Имя пользователя и пароль должны быть больше 4 символов";
                return false;
            }
            return true;
        }
        public ICommand LogInCommand { get; }
        private void OnLogInCommand(object parameter)
        {
            UserModel? user = UserExistence();

            if (user == null)
                MessengeLine = "Неправильный логин или пароль";
            else
            {
                MainViewModel.User = user;
                MainViewModel.SelectedPageViewModel = user.DefalutUser ?
                    MainViewModel.ClientPageViewModels[(int)NumberClientPage.PROFILE_PAGE] :
                    MainViewModel.ClientPageViewModels[(int)NumberClientPage.ADMIN_PAGE];
            }
        }
        private bool CanLogInCommand(object parameter) => true;
        public ICommand SignUpCommand { get; }
        private void OnSignUpCommand(object parameter)
        {
            UserModel? user = UserExistence();

            if (user != null)
                MessengeLine = "Данный пользователь уже существует";
            else
            {
                MainViewModel.User = new UserModel(_name, _pass);
                MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.LOGIN_PAGE]; // Вместо 1 enum
                MainWindowViewModel.applicationContext.Add(MainViewModel.User);
                MainWindowViewModel.applicationContext.SaveChanges();
            }
        }
        private bool CanSignUpCommand(object parameter) => true;
        private UserModel? UserExistence()
        {
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