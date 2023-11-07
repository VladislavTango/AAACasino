using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AAAcasino.Models
{
    internal class QuizNode : ViewModel
    {
        private int _id;
        public int ID
        {
            get => _id;
            set => _id = value;
        }
        private string? _question;    
        public string? Question
        {
            get => _question;
            set => Set(ref _question, value);
        }
        private ObservableCollection<Answer>_answers = new ObservableCollection<Answer>();
        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            set => Set(ref _answers, value);
        }
    }
}
