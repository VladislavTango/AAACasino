using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using System.Windows.Input;

namespace AAAcasino.ViewModels
{
    class LogInViewModel : ViewModel, IPageViewModel
    {
        public string Title => "LogIn";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { return; }

        private string? _name = string.Empty;
        private string? _pass = string.Empty;
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
        public string? MessengeLine
        {
            get => _messengeLine;
            set => Set(ref _messengeLine, value);
        }
        private bool СheckСondition()
        {
            if ((_name.Length < 4 || _pass.Length < 4))
            {
                MessengeLine = "Имя пользователя и пароль должны быть больше 4 символов";
                return false;
            }
            return true;
        }
        #region commands
        public ICommand LogInCommand { get; }
        private void OnLogInCommand(object parameter)
        {

        }
        private bool CanLogInCommand(object parameter) => true;
        public ICommand SignUpCommand { get; }
        private void OnSignUpCommand(object parameter)
        {

        }
        private bool CanSignUpCommand(object parameter) => true;
        #endregion
        public LogInViewModel()
        {
            LogInCommand = new LamdaCommand(OnLogInCommand, CanLogInCommand);
            SignUpCommand = new LamdaCommand(OnSignUpCommand, CanSignUpCommand);
        }
    }
}