using AAAcasino.Infrastructure.Commands;
using AAAcasino.Infrastructure.Commands.Base;
using AAAcasino.Models;
using AAAcasino.Services.Database;
using AAAcasino.ViewModels.Base;
using Microsoft.EntityFrameworkCore;
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

                if (value)
                {
                    QuizModels = new ObservableCollection<QuizModel>(MainWindowViewModel.applicationContext.quizModels.ToList());
                }
                else
                {
                    QuizModels.Clear();
                }
            }
        }
        #region Commands
        public ICommand AddQuizCommand { get; set; }
        private void OnAddQuizCommand(object parameter)
        {
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.CREATION_PANEL_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
            MainViewModel.SelectedPageViewModel.SetAnyModel(new QuizModel());
        }
        private bool CanAddQuizCommand(object parameter) => true;
        public ICommand DeleteQuizCommand { get; set; }
        private void OnDeleteQuizCommand(object quiz)
        {
            var _quiz = quiz as QuizModel;
            QuizModels.Remove(_quiz);

            Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    _quiz = (from quizm in db.quizModels.Include(qm => qm.QuizNodes).ThenInclude(qn => qn.Answers).ToList()
                             where quizm.ID == _quiz.ID
                             select quizm).ToList().First();
                    db.quizModels.Remove(_quiz);
                    db.SaveChanges();
                }
            });
        }
        private bool CanDeleteQuizCommand(object quiz) => true;
        public ICommand EditQuizCommand { get; set; }
        private void OnEditQuizCommand(object quiz)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                QuizCurrent = quiz as QuizModel;

                Task task = Task.Run(() =>
                {
                    QuizCurrent = (from qm in db.quizModels.Include(quizm => quizm.QuizNodes).ThenInclude(qn => qn.Answers).ToList()
                                   where qm.ID == QuizCurrent.ID
                                   select qm).ToList().First();
                });

                task.Wait();

                MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.CREATION_PANEL_PAGE];
                MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
                MainViewModel.SelectedPageViewModel.SetAnyModel(QuizCurrent);
            }
        }
        private bool CanEditQuizCommand(object quiz) => true;
        #endregion
        #endregion
        public AdminViewModel()
        {
            AddQuizCommand = new LamdaCommand(OnAddQuizCommand, CanAddQuizCommand);
            DeleteQuizCommand = new LamdaCommand(OnDeleteQuizCommand, CanDeleteQuizCommand);
            EditQuizCommand = new LamdaCommand(OnEditQuizCommand, CanEditQuizCommand);
        }
    }
}