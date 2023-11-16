using AAAcasino.ViewModels.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace AAAcasino.Models
{
    internal class Answer : ViewModel
    {
        private int _id;
        public int ID
        {
            get => _id;
            set => _id = value;
        }
        private string? _str;
        public string? Str
        {
            get => _str;
            set => Set(ref _str, value);
        }

        private bool _correctness = false;
        public bool IsCorrect
        {
            get => _correctness;
            set => Set(ref _correctness, value);
        }

        [NotMapped]
        private bool _userAnswer = false;
        [NotMapped]
        public bool UserAnswer
        {
            get => _userAnswer;
            set => Set(ref _userAnswer, value);
        }
        public Answer(string? str)
        {
            Str = str;
        }
    }
}