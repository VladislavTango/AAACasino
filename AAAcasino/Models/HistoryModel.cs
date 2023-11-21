using AAAcasino.ViewModels.Base;

namespace AAAcasino.Models
{
    internal class HistoryModel :ViewModel
    {
        private int _id;
        public int Id
        {
            get => _id; 
            set => _id = value;
        }

        #region slot
        private int _SlotWgames=0;
        public int SlotWgames
        {
            get => _SlotWgames;
            set => Set(ref _SlotWgames, value);
        }


        private int _SlotLgames=0;
        public int SlotLgames
        {
            get => _SlotLgames;
            set => Set(ref _SlotLgames, value);
        }


        private double _SlotLMoney = 0;
        public double SlotLMoney
        {
            get => _SlotLMoney;
            set => Set(ref _SlotLMoney, value);
        }


        private double _SlotWMoney = 0;
        public double SlotWMoney
        {
            get => _SlotWMoney;
            set => Set(ref _SlotWMoney, value);
        }
        #endregion

        #region Roulette
        private int _RouletteWgames = 0;
        public int RouletteWgames
        {
            get => _RouletteWgames;
            set => Set(ref _RouletteWgames, value);
        }


        private int _RouletteLgames = 0;
        public int RouletteLgames
        {
            get => _RouletteLgames;
            set => Set(ref _RouletteLgames, value);
        }


        private double _RouletteWMoney = 0;
        public double RouletteWMoney
        {
            get => _RouletteWMoney;
            set => Set(ref _RouletteWMoney, value);
        }


        private double _RouletteLMoney = 0;
        public double RouletteLMoney
        {
            get => _RouletteLMoney;
            set => Set(ref _RouletteLMoney, value);
        }
#endregion

        #region OneHand
        private int _OneHandWgames;//график 1
        public int OneHandWgames
        {
            get => _OneHandWgames;
            set => Set(ref _OneHandWgames, value);
        }


        private int _OneHandLgames;//график 1
        public int OneHandLgames
        {
            get => _OneHandLgames;
            set => Set(ref _OneHandLgames, value);
        }


        private double _OneHandWMoney;
        public double OneHandWMoney
        {
            get => _OneHandWMoney;
            set => Set(ref _OneHandWMoney, value);
        }


        private double _OneHandLMoney;
        public double OneHandLMoney
        {
            get => _OneHandLMoney;
            set => Set(ref _OneHandLMoney, value);
        }
        #endregion


        private double _TotalWin = 0;//текст
        public double TotalWin
        {
            get => _TotalWin;
            set => Set(ref _TotalWin, value);
        }   


        
        private double _TotalLoose =0;//текст
        public double TotalLoose
        {
            get => _TotalLoose;
            set => Set(ref _TotalLoose, value);
        }



        private double _TotalPlus = 0;//текст
        public double TotalPlus
        {
            get => _TotalPlus;
            set => Set(ref _TotalPlus, value);
        }

    }
}
