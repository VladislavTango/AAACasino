using AAAcasino.Infrastructure.Commands;
using AAAcasino.Infrastructure.Commands.Base;
using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace AAAcasino.ViewModels.ClientViewModels.AdminViewModels
{
    internal class AdminViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Админ";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { }
        #endregion
        #region Users tab
        private ObservableCollection<UserModel> _userModels = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> UserModels
        {
            get => _userModels;
            set => Set(ref _userModels, value);
        }

        private bool _userListIsOpen = false;
        public bool UserListIsOpen
        {
            get => _userListIsOpen;
            set
            {
                Set(ref _userListIsOpen, value);

                if (!value)
                {
                    ObservableCollection<UserModel> users = _userModels;
                    Task.Run(() =>
                    {
                        MainWindowViewModel.applicationContext.UpdateRange(users);
                        MainWindowViewModel.applicationContext.SaveChanges();
                    });
                    UserModels.Clear();
                }
                else
                {
                    UserModels = new ObservableCollection<UserModel>(MainWindowViewModel.applicationContext.userModels.ToList());
                }
            }
        }
        #endregion
        #region Quizes tab
        private QuizModel? _quizCurrent = new QuizModel();
        public QuizModel? QuizCurrent
        {
            get => _quizCurrent;
            set => Set(ref _quizCurrent, value);
        }

        private ObservableCollection<QuizModel> quizModels = new ObservableCollection<QuizModel>();
        public ObservableCollection<QuizModel> QuizModels
        {
            get => quizModels;
            set => Set(ref quizModels, value);
        }

        private bool _quizListIsOpen = false;
        public bool QuizListIsOpen
        {
            get => _quizListIsOpen;
            set
            {
                Set(ref _quizListIsOpen, value);

                if (!value)
                {
                    ObservableCollection<QuizModel> quizzes = QuizModels;
                    Task.Run(() =>
                    {
                        MainWindowViewModel.applicationContext.UpdateRange(quizzes);
                        MainWindowViewModel.applicationContext.SaveChanges();
                    });
                    QuizModels.Clear();
                }
                else
                {
                    QuizModels = new ObservableCollection<QuizModel>(MainWindowViewModel.applicationContext.quizModels.ToList());
                }
            }
        }
        #region
        public ICommand AddQuizCommand { get; set; }
        private void OnAddQuizCommand(object parameter)
        {
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.CREATION_PANEL_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
            MainViewModel.SelectedPageViewModel.SetAnyModel(QuizCurrent);
        }
        public bool CanAddQuizCommand(object parameter) => true;

        #endregion
        #endregion
        public AdminViewModel()
        {
            AddQuizCommand = new LamdaCommand(OnAddQuizCommand, CanAddQuizCommand);
        }
    }
}
