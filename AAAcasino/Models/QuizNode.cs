using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AAAcasino.Models
{
    internal class QuizNode : ViewModel
    {
        private string? _question;    
        public string? Question
        {
            get => _question;
            set => Set(ref _question, value);
        }
        private ObservableCollection<string>_answers = new ObservableCollection<string>();
        public ObservableCollection<string> Answers
        {
            get => _answers;
            set => Set(ref _answers, value);
        }
        private ushort _correctAnswerNumber;
        public ushort correctAnswerNumber
        {
            get => _correctAnswerNumber; 
            set => Set(ref _correctAnswerNumber, value);
        }
    }
}
