using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
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

        private byte[]? _questImageBytes = null;
        public byte[]? QuestImageBytes
        {
            get => _questImageBytes;
            set => Set(ref _questImageBytes, value);
        }

        [NotMapped]
        private string? _strAnswCreation = null;
        [NotMapped]
        public string? StrAnswCreation
        {
            get => _strAnswCreation;
            set => Set(ref _strAnswCreation, value);
        }

        private ObservableCollection<Answer>_answers = new ObservableCollection<Answer>();
        public ObservableCollection<Answer> Answers
        {
            get => _answers;
            set => Set(ref _answers, value);
        }
    }
}
