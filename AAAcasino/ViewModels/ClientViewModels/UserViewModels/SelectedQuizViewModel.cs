using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class SelectedQuizViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => $"{QuizModel.Name}";
        public MainWindowViewModel MainViewModel { get; set; } = null!;
        public void SetAnyModel(object? model)
        {
            try
            {
                QuizModel = model != null ? (QuizModel)model : throw new ArgumentNullException(nameof(model));
                _userAnsswers = new List<int>(new int[_quizModel.QuizNodes.Count]);
            }
            catch
            {
                MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.QUIZZES_PAGE];
            }
        }
        #endregion
        private QuizModel _quizModel;
        public QuizModel QuizModel
        {
            get => _quizModel;
            set => Set(ref _quizModel, value);
        }

        private int _questNumber = 0;

        private List<int> _userAnsswers = null!;
    }
}
