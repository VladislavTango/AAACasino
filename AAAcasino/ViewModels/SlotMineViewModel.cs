using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System;

namespace AAAcasino.ViewModels
{
    internal class SlotMineViewModel : ViewModel, IPageViewModel
    {
        #region Ipage
        public string Title => "Slot";

        public MainWindowViewModel MainViewModel { get; set; }

        public void SetAnyModel(object? model)
        {
            balanse = $"Баланс: {MainViewModel.User.Balance}";
        }
        #endregion
        public ObservableCollection<Button> Buttons { get; private set; }
        public ObservableCollection<Button> Buttons2 { get; private set; }
        public ObservableCollection<Button> Buttons3 { get; private set; }
        public ObservableCollection<Button> Buttons4 { get; private set; }
        public ObservableCollection<Button> Buttons5 { get; private set; }
        bool GameStarted = false;

        public static double ThisMn = 0;
        public static double NextMn = 0;

        public static double NextBid = 0;

        bool[] field = new bool[25];


        Button[] btn = new Button[25];
        bool[] Onclick = new bool[25];
        int itc = 0;
        ObservableCollection<Button> CreateButtons(ObservableCollection<Button> but)
        {
            but = new ObservableCollection<Button>();
            for (int i = 0 + itc; i < 5 + itc; i++)
            {
                btn[i] = new Button();
                btn[i].CommandParameter = i;
                btn[i].Background = Brushes.Blue;
                btn[i].Height = 100;
                btn[i].Command = ColorChange;
                but.Add(btn[i]);
            }
            itc += 5;
            return but;
        }

        void GetField()
        {
            for (int i = 0; i < field.Length; i++)
                field[i] = true;
            int num = Bombs + 1;
            int counter = 0;
            Random rnd = new Random();
            while (num > counter)
            {
                field[rnd.Next(0, 24)] = false;
                counter++;
            }

        }
        string NormalFormOfTextBox(string str)
        {
            if (str.Last() == '-') return str.Remove(str.Length - 1);
            try
            {
                if (!Char.IsDigit(str, str.Length - 1))
                {
                    if (str.Last() == '.' || str.Last() == ',')
                    {
                        if (str.Count(p => p == ',') >= 2 || str.Last() == '.') return str.Remove(str.Length - 1);
                        str = str.Replace('.', ',');
                    }
                    else return str.Remove(str.Length - 1);
                }
            }
            catch { return "0"; }
            string[] col = str.Split(',');
            try
            {
                if (col[1].Length > 2) return str.Remove(str.Length - 1);
            }
            catch { }
            return str;
        }

        public SlotMineViewModel()
        {
            ColorChange = new LamdaCommand(OnColorChangeCommand, CanColorChangeCommand);
            Start = new LamdaCommand(OnStartCommand, CanStartCommand);
            Buttons = CreateButtons(Buttons);
            Buttons2 = CreateButtons(Buttons2);
            Buttons3 = CreateButtons(Buttons3);
            Buttons4 = CreateButtons(Buttons4);
            Buttons5 = CreateButtons(Buttons5);
            Stop = new LamdaCommand(OnStopCommand, CanStopCommand);
        }

        public ICommand ColorChange { get; }
        private bool CanColorChangeCommand(object parameter) => true;
        private void OnColorChangeCommand(object parameter)
        {
            if (GameStarted)
            {
                int num = (int)parameter;
                if (Onclick[num] != true)
                {
                    Onclick[num] = true;
                    #region окрас
                    if (num < 5)
                    {
                        if (field[num] == true)
                            Buttons[num].Background = Brushes.Green;
                        else
                            Buttons[num].Background = Brushes.Red;
                    }
                    if (num > 4 && num <= 9)
                    {
                        if (field[num] == true)
                            Buttons2[num - 5].Background = Brushes.Green;
                        else
                            Buttons2[num - 5].Background = Brushes.Red;
                    }
                    if (num > 9 && num <= 14)
                    {
                        if (field[num] == true)
                            Buttons3[num - 10].Background = Brushes.Green;
                        else
                            Buttons3[num - 10].Background = Brushes.Red;
                    }
                    if (num > 14 && num <= 19)
                    {
                        if (field[num] == true)
                            Buttons4[num - 15].Background = Brushes.Green;
                        else
                            Buttons4[num - 15].Background = Brushes.Red;
                    }
                    if (num > 19 && num <= 24)
                    {
                        if (field[num] == true)
                            Buttons5[num - 20].Background = Brushes.Green;
                        else
                            Buttons5[num - 20].Background = Brushes.Red;
                    }
                    if (field[num] == false)
                        GameStarted = false;
                    #endregion
                    ThisMn += Math.Round((((Bombs) * 0.2) + 0.1), 2);
                    NextMn = ThisMn + Math.Round((((Bombs) * 0.2) + 0.1), 2);
                    Mn = $"Множитель={Math.Round(ThisMn, 2)}X";
                    NMn = $"Следующий множитель={Math.Round((NextMn), 2)}X";
                    BWin = $"Баланс после выйгрыша:{Math.Round(Convert.ToDouble(Bid) * ThisMn + MainViewModel.User.Balance, 2)}";
                    NBid = $"следующего результат:{Math.Round(Convert.ToDouble(Bid) * NextMn + MainViewModel.User.Balance, 2)}";
                }
            }
        }

        private int _Bombs = 0;
        public int Bombs
        {
            get => _Bombs;
            set => Set(ref _Bombs, value);
        }

        #region Строковые парамы
        private string _Mn = $"Множитель={ThisMn}X";
        public string Mn
        {
            get => _Mn;
            set => Set(ref _Mn, value);
        }
        private string _NMn = $"Следующий множитель={NextMn}X";
        public string NMn
        {
            get => _NMn;
            set => Set(ref _NMn, value);
        }
        private string _balanse;
        public string balanse
        {
            get => _balanse;
            set => Set(ref _balanse, value);
        }

        private string _Bid;
        public string Bid
        {
            get => _Bid;
            set => Set(ref _Bid, NormalFormOfTextBox(value));
        }
        private string _NBid = $"Следеующий результат:{NextBid}";
        public string NBid
        {
            get => _NBid;
            set => Set(ref _NBid, value);
        }
        private string _BWin = $"Баланс после выйгрыша:0";
        public string BWin
        {
            get => _BWin;
            set => Set(ref _BWin, value);
        }
        #endregion
        private double _height = 600;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        #region кнопки
        public ICommand Start { get; }
        private bool CanStartCommand(object parameter) => true;
        private void OnStartCommand(object parameter)
        {
            try
            {
                if (Convert.ToDouble(Bid) <= MainViewModel.User.Balance)
                {
                    if (!GameStarted)
                    {
                        GameStarted = true;
                        ToFalse();
                        GetField();
                        ToBlue();
                        ThisMn = 0;
                        MainViewModel.User.Balance -= Convert.ToDouble(Bid);
                        balanse = $"баланс={MainViewModel.User.Balance}";
                    }
                }
                else MessageBox.Show("У вас недостаточно баллов");
            }
            catch { MessageBox.Show("введите нормально"); }
        }
        public ICommand Stop { get; }

        private bool CanStopCommand(object parameter) => true;
        private void OnStopCommand(object parameter)
        {
            if (GameStarted)
            {
                GameStarted = false;
                MainViewModel.User.Balance = Math.Round(Convert.ToDouble(Bid) * ThisMn + MainViewModel.User.Balance, 2);
                balanse = $"баланс={MainViewModel.User.Balance}";
            }
        }
        #endregion
        void ToFalse()
        {
            for (int i = 0; i < 25; i++)
                Onclick[i] = false;
        }
        void ToBlue()
        {
            for (int i = 0; i < 5; i++)
            {
                Buttons[i].Background = Brushes.Blue;
                Buttons2[i].Background = Brushes.Blue;
                Buttons3[i].Background = Brushes.Blue;
                Buttons4[i].Background = Brushes.Blue;
                Buttons5[i].Background = Brushes.Blue;
            }
        }
    }
}
