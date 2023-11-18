using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.Services.Database;
using AAAcasino.ViewModels.Base;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels
{
    internal class SignUpViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Регистрация";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { }
        #endregion
        #region line property vm
        private string? _userName = null;
        public string? Username
        {
            get => _userName;
            set => Set(ref _userName, value);
        }
        private string? _firstPassword = null;
        public string? FirstPassword
        {
            get => _firstPassword;
            set => Set(ref _firstPassword, value);
        }
        private string? _secondPassword = null;
        public string? SecondPassword
        {
            get => _secondPassword; 
            set => Set(ref _secondPassword, value);
        }

        private string? _messengeLine = null;
        public string? MessengeLine
        {
            get => _messengeLine;
            set => Set(ref _messengeLine, value);
        }
        #endregion
        #region commands
        public ICommand SignUpCommand { get; }
        private void OnSignUpCommand(object parameter)
        {
            FirstPassword = (parameter as PasswordBox).Password;

            if (FirstPassword.Count() < 4 || _userName.Count() < 4)
                MessengeLine = "Количество символов меньше 4";
            else if (FirstPassword != SecondPassword)
                MessengeLine = "Пароли не совпадают";
            else
            {
                Task.Run(() =>
                {
                    using (ApplicationContext applicationContext = new ApplicationContext())
                    {
                        UserModel? user = (from um in applicationContext.userModels.ToList()
                                           where um.Username == _userName
                                           select um).ToList().FirstOrDefault();
                        if (user == null)
                        {
                            user = new UserModel(_userName, _firstPassword);
                            user.Balance = 100;
                            MainViewModel.User = user;

                            applicationContext.userModels.Add(user);
                            applicationContext.SaveChanges();

                            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.USER_PAGE];
                            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
                            MainViewModel.SelectedPageViewModel.SetAnyModel(null);
                        }
                        else
                            MessengeLine = "Данный пользователь существует";
                    }
                });
            }
        }
        private bool CanSignUpCommand(object parameter) => true;
        public ICommand GoToLogInCommand { get; }
        private void OnGoToLogInCommand(object parameter)
        {
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.LOGIN_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
        }
        private bool CanGoToLogInCommand(object parameter) => true;
        public ICommand SecondPBLostFocusCommand { get; }
        private void OnSecondPBLostFocusCommand(object parameter) => SecondPassword = (parameter as PasswordBox).Password;
        private bool CanSecondPBLostFocusCommand(object parameter) => true;
        #endregion
        public SignUpViewModel()
        {
            SignUpCommand = new LamdaCommand(OnSignUpCommand, CanSignUpCommand);
            GoToLogInCommand = new LamdaCommand(OnGoToLogInCommand, CanGoToLogInCommand);
            SecondPBLostFocusCommand = new LamdaCommand(OnSecondPBLostFocusCommand, CanSecondPBLostFocusCommand);
        }
    }
}
