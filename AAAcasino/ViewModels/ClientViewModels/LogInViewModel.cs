using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.Services.Database;
using AAAcasino.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels
{
    class LogInViewModel : ViewModel, IPageViewModel
    {
        #region IPage property
        public string Title => "Вход";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { return; }
        #endregion
        #region ViewModel property
        private string? _userName = string.Empty;
        private string? _messengeLine = null;
        public string? UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
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
            Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    UserModel? user = db.ExistUser(_userName, (parameter as PasswordBox).Password);

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
            });
        }
        private bool CanLogInCommand(object parameter) => true;
        public ICommand GoToSignUpCommand { get; }
        private void OnGoToSignUpCommand(object parameter)
        {
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.SIGNUP_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel; 
        }
        private bool CanGoToSignUpCommand(object parameter) => true;
        #endregion
        public LogInViewModel()
        {
            LogInCommand = new LamdaCommand(OnLogInCommand, CanLogInCommand);
            GoToSignUpCommand = new LamdaCommand(OnGoToSignUpCommand, CanGoToSignUpCommand);
        }
    }
}