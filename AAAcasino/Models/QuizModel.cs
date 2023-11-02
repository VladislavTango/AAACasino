using AAAcasino.ViewModels.Base;
using System.Collections.Generic;

namespace AAAcasino.Models
{
    internal class QuizModel
    {
        private int _id;
        public int ID
        {
            get => _id;
            set => _id = value;
        }
        private List<QuizNode> _quizNodes;
        public List<QuizNode> QuizNodes { get => _quizNodes; }
        public void AddQuizNode(QuizNode node) { _quizNodes.Add(node); }
        public void RemoveQuizNode(QuizNode node) { _quizNodes.Remove(node); }
    }
}
