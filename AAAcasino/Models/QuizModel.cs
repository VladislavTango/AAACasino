using AAAcasino.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AAAcasino.Models
{
    internal class QuizModel :ViewModel
    {
        private int _id;
        public int ID
        {
            get => _id;
            set => _id = value;
        }

        private string? _name = null;
        public string? Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private int _reward = 0;
        public int Reward
        {
            get => _reward;
            set => Set(ref _reward, value); 
        }

        private ObservableCollection<QuizNode> _quizNodes = new ObservableCollection<QuizNode>();
        public ObservableCollection<QuizNode> QuizNodes { get => _quizNodes; }
        public void AddQuizNode(QuizNode node) { _quizNodes.Add(node); }
        public void RemoveQuizNode(QuizNode node) { _quizNodes.Remove(node); }
    }
}
