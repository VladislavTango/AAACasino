using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using Microsoft.Identity.Client;
using System.CodeDom;
using System.IO.Packaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels.AdminViewModels
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
        private string? _answStr;
        public string AnswStr
        {
            get => _answStr;
            set => Set(ref _answStr, value);
        }

        private QuizNode? _selectedQuest = null;
        public QuizNode? SelectedQuest
        {
            get => _selectedQuest;
            set => Set(ref _selectedQuest, value);
        }

        private Answer _selectedAnswer = new Answer(null);
        public Answer SelectedAnswer
        {
            get => _selectedAnswer;
            set => Set(ref _selectedAnswer, value);
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
        private void OnAddAnswerCommand(object parameter)
        {
            if (_selectedQuest != null)
            {
                _selectedQuest.Answers.Add(new Answer(AnswStr));
                AnswStr = string.Empty;
            }
        }
        private bool CanAddAnswerCommand(object parameter) => AnswStr != "";
        public ICommand RemoveQuizNodeCommand { get; set; }
        private void OnRemoveQuizNodeCommand(object parameter)
        {
                _quizModel.QuizNodes.Remove(SelectedQuest);
                AnswStr = string.Empty;
                Quest = string.Empty;
        }
        private bool CanRemoveQuizNodeCommand(object parameter) => _selectedQuest != null;
        public ICommand RemoveAnswerCommand { get; set; }
        private void OnRemoveAnswerCommand(object parameter)
        {
            _selectedQuest.Answers.Remove(_selectedAnswer);
        }
        private bool CanRemoveAnswerCommand(object parameter) => _selectedAnswer != null && _selectedQuest != null;
        #endregion
        public CreationQuizViewModel()
        {
            AddQuizNodeCommand = new LamdaCommand(OnAddQuizNodeCommand, CanAddQuizNodeCommand);
            SaveQuizCommand = new LamdaCommand(OnSaveQuizCommand, CanSaveQuizCommand);
            AddAnswerCommand = new LamdaCommand(OnAddAnswerCommand, CanAddAnswerCommand);
            RemoveQuizNodeCommand = new LamdaCommand(OnRemoveQuizNodeCommand, CanRemoveQuizNodeCommand);
            RemoveAnswerCommand = new LamdaCommand(OnRemoveAnswerCommand, CanRemoveAnswerCommand);
        }
    }
}
