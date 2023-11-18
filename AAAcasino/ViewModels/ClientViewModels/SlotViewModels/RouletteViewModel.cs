using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using AAAcasino.ViewModels.ClientViewModels.AdminViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AAAcasino.ViewModels.SlotViewModels
{

    class RouletteViewModel : ViewModel, IPageViewModel {
        public string Title => "da";
        double total_bet = 0;
        public MainWindowViewModel MainViewModel { get; set; }
        string lastnormtbox;
        private string[] wheel = new string[] { "0", "6", "33", "10", "35", "22", "29", "4", "27", "16", "19", "8", "31", "18", "25", "14", "21", "2", "23", "12", "15", "30", "17", "34", "1", "28", "9", "36", "11", "24", "13", "26", "3", "20", "5", "32", "7" };
        private bool Is_Gmae_Started=true;
        public void SetAnyModel(object? model)
        {
            balance = $"Баланс: {(double)MainViewModel.User.Balance}";
        }

        public ObservableCollection<Button> Buttons { get; private set; }
        public ObservableCollection<string> BidList { get; set; }
        public ObservableCollection<int> ScetList { get; set; }
        public ObservableCollection<double> BetList { get; set; }
        public Dictionary<int, List<int>> numbers=new Dictionary<int, List<int>>()
            {
                [37] = new List<int>(){ 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 },
                [38] = new List<int>() { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 },
                [39] = new List<int>() { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 },
                [40] = new List<int>() { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 },
                [41] = new List<int>() { 25, 26, 27, 28, 29, 30 },
                [42] = new List<int>() { 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 },
                [43] = new List<int>() { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 },
                [44] = new List<int>() { },
                [45] = new List<int>() { },
                [46] = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                [47] = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 },
                [48] = new List<int>() { 7, 8, 9, 10, 11, 12 }
            };

    private double winner=0;
        public RouletteViewModel()
        {
            number_1 = "32";
            DrawNumberSlots(0);
            Start = new LamdaCommand(OnStartCommand, CanStartCommand);
            Buttons = new ObservableCollection<Button>();
            Buttons = SetNewBtn(Buttons);
            btn = new LamdaCommand(OnBetNumCommand, CanBetNumCommand);
            ScetList = new ObservableCollection<int>();
            BetList = new ObservableCollection<double>();
            BidList = new ObservableCollection<string>();
            DeleteSelectedItem = new LamdaCommand(OnDeleteSelectedItemCommand, CanDeleteSelectedItemCommand);
            BackToMenu = new LamdaCommand(OnBackToMenuCommand, CanBackToMenuCommand);

        }
        public ICommand BackToMenu { get; }
        private bool CanBackToMenuCommand(object parameter) => true;
        private void OnBackToMenuCommand(object parameter)
        {
            //хуй говна поклюй
        }
        ObservableCollection<Button> SetNewBtn(ObservableCollection<Button> but) {
            but = new ObservableCollection<Button>();
            Button[] btn = new Button[37];
            for (int i = 1; i < 37; i++)
            {
                btn[i] = new Button();
                if (i <= 12) {
                    btn[i].Content = $"{i * 3}";
                    btn[i].CommandParameter = i * 3;
                }
                if (i > 12 && i <= 24) {
                    btn[i].Content = $"{(i - 13) * 3 + 2}";
                    btn[i].CommandParameter = (i - 13) * 3 + 2;
                }
                if (i > 24 && i <= 36)
                {
                    btn[i].Content = $"{(i - 25) * 3 + 1}";
                    btn[i].CommandParameter = (i - 25) * 3 + 1;
                }
                if (i == 37) {

                }
                btn[i].Foreground = Brushes.White;
                btn[i].FontSize = 70;
                btn[i].Command = new LamdaCommand(OnBetNumCommand, CanBetNumCommand);
                if (i == 37)
                    btn[i].Background = Brushes.Green;
                if (Convert.ToInt16(btn[i].CommandParameter) % 2 == 0 && i != 0) btn[i].Background = Brushes.Red;
                if (Convert.ToInt16(btn[i].CommandParameter) % 2 % 2 != 0 && i != 0) btn[i].Background = Brushes.Black;
                but.Add(btn[i]);
            }
            return but;
        }
        int RetIndex(int index) {
            if (index > 36)
                return index - 37;
            return index;
        }
        void DrawNumberSlots(int adder) {
            int index = wheel.ToList().IndexOf(number_1) + adder;
            number_1 = wheel[RetIndex(index)];
            number_2 = wheel[RetIndex(index + 1)];
            number_3 = wheel[RetIndex(index + 2)];
            number_4 = wheel[RetIndex(index + 3)];
            number_5 = wheel[RetIndex(index + 4)];
            color_1 = ColorSet(color_1, number_1);
            color_2 = ColorSet(color_2, number_2);
            color_3 = ColorSet(color_3, number_3);
            color_4 = ColorSet(color_4, number_4);
            color_5 = ColorSet(color_5, number_5);
        }
        string ColorSet(string color, string number) {
            int index = wheel.ToList().IndexOf(number);
            if (wheel[index] == "0")
                return "green";
            if (Convert.ToInt16(wheel[index]) % 2 == 0) return "red";
            if (Convert.ToInt16(wheel[index]) % 2 != 0) return "black";
            return "отсоси уёбище";
        }
        string NormalFormOfTextBox(string str)
        {

            try
            {
                if (!char.IsDigit(str, str.Length - 1))
                {
                    if (str.Last() == '.' || str.Last() == ',')
                    {
                        if (str.Count(p => p == ',') >= 2 || str.Last() == '.') return str.Remove(str.Length - 1);
                        str = str.Replace('.', ',');
                    }
                    else return str.Remove(str.Length - 1);
                }
            }
            catch { return ""; }
            string[] col = str.Split(',');
            try
            {
                if (col[1].Length > 2) return str.Remove(str.Length - 1);
            }
            catch { }
            try { Convert.ToDouble(str); }
            catch { return lastnormtbox; }
            lastnormtbox = str;
            return str;
        }
        #region colors
        private string _color_1;
        public string color_1
        {
            get => _color_1;
            set => Set(ref _color_1, value);
        }
        private string _color_2;
        public string color_2
        {
            get => _color_2;
            set => Set(ref _color_2, value);
        }
        private string _color_3;
        public string color_3
        {
            get => _color_3;
            set => Set(ref _color_3, value);
        }
        private string _color_4;
        public string color_4
        {
            get => _color_4;
            set => Set(ref _color_4, value);
        }

        private string _color_5;
        public string color_5
        {
            get => _color_5;
            set => Set(ref _color_5, value);
        }
        #endregion

        #region Number
        private string _number_1;
        public string number_1
        {
            get => _number_1;
            set => Set(ref _number_1, value);
        }
        private string _number_2;
        public string number_2
        {
            get => _number_2;
            set => Set(ref _number_2, value);
        }
        private string _number_3;
        public string number_3
        {
            get => _number_3;
            set => Set(ref _number_3, value);
        }
        private string _number_4;
        public string number_4
        {
            get => _number_4;
            set => Set(ref _number_4, value);
        }

        private string _number_5;
        public string number_5
        {
            get => _number_5;
            set => Set(ref _number_5, value);
        }

        private int _selectedIndex;
        public int selectedIndex
        {
            get => _selectedIndex;
            set => Set(ref _selectedIndex, value);
        }
        #endregion
        #region another
        private string _Bid;
        public string Bid
        {
            get => _Bid;
            set => Set(ref _Bid, NormalFormOfTextBox(value));
        }
        private string _blance;
        public string balance
        {
        get => _blance;
        set => Set(ref _blance, value);
        }
        private string _TotalBet = "Общая ставка:";
        public string TotalBet
        {
            get => _TotalBet;
            set => Set(ref _TotalBet, value);
        }

        #endregion
        void Adder(int param, string name)
        {
            if (ScetList.Contains(param))
                MessageBox.Show("на это уже сделана ставка");
            else
            {
                    if (Convert.ToDouble(Bid) > 0)
                    {
                        BidList.Insert(0, name + " Ставка:" + Bid);
                        ScetList.Insert(0, param);
                        BetList.Insert(0, Convert.ToDouble(Bid));
                        total_bet += Convert.ToDouble(Bid);
                        TotalBet = $"Общаяя ставка:{total_bet}";
                    }
                    else {MessageBox.Show("Сначала сделай ставку больше нуля");}
            }
        }
        #region кнопки


        public ICommand btn { get; }
        private bool CanBetNumCommand(object parameter) => true;
        private void OnBetNumCommand(object parameter) {
            if (Convert.ToInt16(parameter) <= 36)
            {
                Adder(Convert.ToInt16(parameter), Convert.ToString(parameter));
            }
            else
            {
                switch (Convert.ToInt16(parameter)) {
                    case 37: Adder(Convert.ToInt16(parameter), "2 to 1");break;
                    case 38: Adder(Convert.ToInt16(parameter), "2 to 1"); break;
                    case 39: Adder(Convert.ToInt16(parameter), "2 to 1"); break;
                    case 40: Adder(Convert.ToInt16(parameter), "3 rd 12"); break;
                    case 41: Adder(Convert.ToInt16(parameter), "ODD"); break;
                    case 42: Adder(Convert.ToInt16(parameter), "19 - 36"); break;
                    case 43: Adder(Convert.ToInt16(parameter), "2 nd 12"); break;
                    case 44: Adder(Convert.ToInt16(parameter), "на все красные"); break;
                    case 45: Adder(Convert.ToInt16(parameter), "на все чёрные"); break;
                    case 46: Adder(Convert.ToInt16(parameter), "1 st 12"); break;
                    case 47: Adder(Convert.ToInt16(parameter), "1 to 18"); break;
                    case 48: Adder(Convert.ToInt16(parameter), "EVEN"); break;
                }
            }
        }//добавление в лист ставок

        public ICommand DeleteSelectedItem { get; }
        private bool CanDeleteSelectedItemCommand(object parameter) => true;
        private void OnDeleteSelectedItemCommand(object parameter)
        {
            try
            {
                total_bet -= BetList[selectedIndex];
                TotalBet = $"Общаяя ставка:{total_bet}";
                BetList.Remove(BetList[selectedIndex]);
                ScetList.Remove(ScetList[selectedIndex]);
                BidList.Remove(BidList[selectedIndex]);
            }
            catch { }
        }//удаление элемента
        public ICommand Start { get; }
        private bool CanStartCommand(object parameter) => Is_Gmae_Started;//start игры
        private void OnStartCommand(object parameter)
        {
            if (ScetList.Count == 0) MessageBox.Show("сделай ставку");
            else
            {
                if (total_bet <= MainViewModel.User.Balance)
                {
                    Is_Gmae_Started = false;
                    MainViewModel.User.Balance = Convert.ToDouble(MainViewModel.User.Balance) - total_bet;
                    balance = $"Баланс {MainViewModel.User.Balance}";
                    Random rnd = new Random();
                    Task.Run(() => { Go(rnd.Next(70, 150)); });
                }
                else
                MessageBox.Show("У тебя не хватает баллов");
            }
        }
        async void Go(int num)
        {
            for (int i = 1; i<=num; i++)
            {
                await Task.Run(async () =>
                {
                    DrawNumberSlots(1);
                    await Task.Delay(50+i);
                }
                );
            }
            for (int i = 0; i < ScetList.Count(); i++) {
                if (ScetList[i] < 36)
                {
                    if (ScetList[0] ==0)
                        MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 50;
                    if (ScetList[i] == Convert.ToInt32(number_3)&&ScetList[i]!=0)
                        MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 35;
                }
                else {
                    switch (ScetList[i]) {
                        case 37: {
                                if (numbers[37].Contains(Convert.ToInt16(number_3))) {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 3;
                                }
                                break; }
                        case 38:
                            {
                                if (numbers[38].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 3;
                                }
                                break;
                            }
                        case 39:
                            {
                                if (numbers[39].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 3;
                                }
                                break;
                            }
                        case 40:
                            {
                                if (numbers[40].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 3;

                                }
                                break;
                            }
                        case 41:
                            {
                                if (numbers[41].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 6;

                                }
                                break;
                            }
                        case 42:
                            {
                                if (numbers[42].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 2;

                                }
                                break;
                            }
                        case 43:
                            {
                                if (numbers[43].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 3;


                                }
                                break;
                            }
                        case 44:
                            {
                                if ((Convert.ToInt16(number_3)%2==0))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 2;

                                }
                                break;
                            }
                        case 45:
                            {
                                if (Convert.ToInt16(number_3)%2!=0)
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 2;

                                }
                                break;
                            }
                        case 46:
                            {
                                if (numbers[46].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 3;

                                }
                                break;
                            }
                        case 47:
                            {
                                if (numbers[47].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 2;

                                }
                                break;
                            }
                        case 48:
                            {
                                if (numbers[48].Contains(Convert.ToInt16(number_3)))
                                {
                                    MainViewModel.User.Balance = MainViewModel.User.Balance + BetList[i] * 6;

                                }
                                break;
                            }



                    }
                }
            }
            balance = $"Баланс {MainViewModel.User.Balance}";

            Is_Gmae_Started = true;
        }
                #endregion     
    }

}
