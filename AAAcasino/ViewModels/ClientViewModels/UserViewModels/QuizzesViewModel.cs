using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class QuizzesViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Викторины";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) 
        {
            QuizModels = new ObservableCollection<QuizModel>(MainWindowViewModel.applicationContext.quizModels.ToList());
        }
        #endregion
        private ObservableCollection<QuizModel>? _quizModels = null;
        public ObservableCollection<QuizModel>? QuizModels
        {
            get => _quizModels;
            set => Set(ref  _quizModels, value);
        }
        #region Commands
        public ICommand GoToQuizCommand { get; set; }
        private void OnGoToQuizCommand(QuizModel quiz) 
        {
            
        }
        #endregion
    }
}