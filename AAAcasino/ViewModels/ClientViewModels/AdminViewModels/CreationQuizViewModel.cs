using AAAcasino.Infrastructure.Commands;
using AAAcasino.Models;
using AAAcasino.Services.Database;
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
        #region command
        public ICommand AddQuizNodeCommand { get; set; }
        private void OnAddQuizNodeCommand(object param)
        {
            QuizNode node = new QuizNode();
            node.Question = Quest;
            QuizModel.AddQuizNode(node);
            Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.quizModels.Update(QuizModel);
                    db.SaveChanges();
                }
            });
        }
        private bool CanAddQuizNodeCommand(object param) => true;
        public ICommand SaveQuizCommand { get; set; }
        private void OnSaveQuizCommand(object param)
        {
            Task.Run(() =>
            {
                int existance = (from q in MainWindowViewModel.applicationContext.quizModels.ToList()
                                 where q.ID == QuizModel.ID
                                 select q).Count();
                using(ApplicationContext db = new ApplicationContext())
                {
                    if (existance > 0)
                        db.quizModels.Update(QuizModel);
                    else
                        db.quizModels.Add(QuizModel);

                    db.SaveChanges();
                }
            });
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.ADMIN_PAGE];
        }
        private bool CanSaveQuizCommand(object parameter) => QuizModel.Name != "" && QuizModel.Name != null;
        public ICommand AddAnswerCommand { get; set; }
        private void OnAddAnswerCommand(object parameter)
        {
            int indexQN = QuizModel.QuizNodes.IndexOf(parameter as QuizNode);
            QuizModel.QuizNodes[indexQN].Answers.Add(new Answer(AnswStr));
            AnswStr = "";

            Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.quizModels.Update(QuizModel);
                    db.SaveChanges();
                }
            });
        }
        private bool CanAddAnswerCommand(object parameter) => AnswStr != "" && AnswStr != null;
        public ICommand RemoveQuizNodeCommand { get; set; }
        private void OnRemoveQuizNodeCommand(object parameter)
        {
            QuizModel.QuizNodes.Remove(parameter as QuizNode);
            Task.Run(() =>
            {
                var quest = parameter as QuizNode;
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var answ in quest.Answers)
                        db.answers.Remove(answ);

                    db.quizNodes.Remove(quest);
                    db.SaveChanges();
                }
            });

            AnswStr = string.Empty;
            Quest = string.Empty;
        }
        private bool CanRemoveQuizNodeCommand(object parameter) => true;
        public ICommand RemoveAnswerCommand { get; set; }
        private void OnRemoveAnswerCommand(object parameter)
        {
            var answer = parameter as Answer;

            int indexQN = -1;
            foreach (var qn in QuizModel.QuizNodes)
            {
                bool isFind = false;
                foreach (var answ in qn.Answers)
                {
                    if(answ ==  answer)
                    {
                        isFind = true;
                        indexQN = QuizModel.QuizNodes.IndexOf(qn);
                        break;
                    }
                }

                if (isFind)
                    break;
            }

            QuizModel.QuizNodes[indexQN].Answers.Remove(answer);

            using (ApplicationContext db = new ApplicationContext())
            {
                db.answers.Remove(answer);
                db.SaveChanges();
            }
        }
        private bool CanRemoveAnswerCommand(object parameter) => true;
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