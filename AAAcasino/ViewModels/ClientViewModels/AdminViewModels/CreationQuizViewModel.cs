using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using Microsoft.Identity.Client;
using System.CodeDom;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels.AdminViewModels //test merge
{
    internal class CreationQuizViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Создание викторины";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model)
        {
            QuizModel = (QuizModel)model;
        }
        #endregion
        private QuizModel? _quizModel = new QuizModel();
        public QuizModel? QuizModel
        {
            get => _quizModel;
            set => Set(ref _quizModel, value);
        }
        private string? _quest;
        public string Quest
        {
            get => _quest;
            set => Set(ref _quest, value);
        }
        #region command
        public ICommand AddQuizNodeCommand { get; set; }
        private void OnAddQuizNodeCommand(object param)
        {
            QuizNode node = new QuizNode();
            node.Question = Quest;
            QuizModel.AddQuizNode(node);
        }
        private bool CanAddQuizNodeCommand(object param) => true;
        public ICommand SaveQuizCommand { get; set; }
        private void OnSaveQuizCommand(object param)
        {
            Task.Run(() =>
            {
                int existance = (from q in MainWindowViewModel.applicationContext.quizModels.ToList()
                                 where q == QuizModel
                                 select q).Count();
                if (existance > 0)
                    MainWindowViewModel.applicationContext.Update(QuizModel);
                else
                    MainWindowViewModel.applicationContext.Add(QuizModel);
                MainWindowViewModel.applicationContext.SaveChanges();
            });
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.ADMIN_PAGE];
        }
        private bool CanSaveQuizCommand(object parameter) => (QuizModel.Name != "" && QuizModel.Name != null);
        public ICommand AddAnswerCommand { get; set; }
        #endregion
        public CreationQuizViewModel()
        {
            AddQuizNodeCommand = new LamdaCommand(OnAddQuizNodeCommand, CanAddQuizNodeCommand);
            SaveQuizCommand = new LamdaCommand(OnSaveQuizCommand, CanSaveQuizCommand);
        }
    }
}
