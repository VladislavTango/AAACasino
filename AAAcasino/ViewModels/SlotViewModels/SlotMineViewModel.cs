using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System;
using AAAcasino.Services.Database;
using System.Threading.Tasks;

namespace AAAcasino.ViewModels.SlotViewModels
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
        string lastnormtbox;

        public ObservableCollection<Button> Buttons { get; private set; }
      
        bool GameStarted = false;

        public static double ThisMn = 0;
        public static double NextMn = 0;
        public  int ThisBombs = 0;

        public static double NextBid = 0;

        bool[] field = new bool[25];


        Button[] btn = new Button[25];
        bool[] Onclick = new bool[25];
        ObservableCollection<Button> CreateButtons(ObservableCollection<Button> but)
        {
            but = new ObservableCollection<Button>();
            for (int i = 0 ; i < 25; i++)
            {
                btn[i] = new Button();
                btn[i].CommandParameter = i;
                btn[i].Background = Brushes.Blue;
                btn[i].Command = ColorChange;
                btn[i].BorderBrush= new SolidColorBrush(Colors.Black);
                but.Add(btn[i]);
            }
            return but;
        }

        void GetField()
        {
            for (int i = 0; i < field.Length; i++)
                field[i] = true;
            int num = ThisBombs + 1;
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

        public SlotMineViewModel()
        {
            ColorChange = new LamdaCommand(OnColorChangeCommand, CanColorChangeCommand);
            Start = new LamdaCommand(OnStartCommand, CanStartCommand);
            BackToMenu = new LamdaCommand(OnBackToMenuCommand, CanBackToMenuCommand);
            Buttons = CreateButtons(Buttons);

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
                    if (field[num] == true)
                        Buttons[num].Background = Brushes.Green;
                    else
                        Buttons[num].Background = Brushes.Red;

                    if (field[num] == false)
                    {
                        GameStarted = false;
                        Task.Run(() => ToDB(false));
                        StartColor = "LightBlue";
                        StartContent = "Старт";
                    }
                    #endregion
                    ThisMn += Math.Round(ThisBombs * 0.2 + 0.1, 2);
                    NextMn = ThisMn + Math.Round(ThisBombs * 0.2 + 0.1, 2);
                    Mn = $"Множитель={Math.Round(ThisMn, 2)}X";
                    NMn = $"Следующий множитель={Math.Round(NextMn, 2)}X";
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
        private string _StartColor= "LightBlue";
        public string StartColor
        {
            get => _StartColor;
            set => Set(ref _StartColor, value);
        }
        private string _StartContent = "Старт";
        public string StartContent
        {
            get => _StartContent;
            set => Set(ref _StartContent, value);
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
                        StartColor = "Red";
                        StartContent = "Стоп";
                        GameStarted = true;
                        ToFalse();
                        GetField();
                        ToBlue();
                        ThisMn = 0;
                        ThisBombs = Bombs;
                        MainViewModel.User.Balance -= Convert.ToDouble(Bid);
                        balanse = $"баланс={MainViewModel.User.Balance}";
                    }
                    else {                        
                        GameStarted = false;
                        MainViewModel.User.Balance = Math.Round(Convert.ToDouble(Bid) * ThisMn + MainViewModel.User.Balance, 2);
                        balanse = $"баланс={MainViewModel.User.Balance}";
                        Task.Run(() => ToDB(true));
                        StartColor = "LightBlue";
                        StartContent = "Старт";
                    }
                }
                else MessageBox.Show("У вас недостаточно баллов");
            }
            catch { MessageBox.Show("введите нормально"); }
        }
        public ICommand BackToMenu { get; }

        private bool CanBackToMenuCommand(object parameter) => true;
        private void OnBackToMenuCommand(object parameter)
        {
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.USER_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
            MainViewModel.SelectedPageViewModel.SetAnyModel(null);
        
    }
        #endregion
        async void ToDB(bool WorL) {
            try
            {
                await using (ApplicationContext db = new ApplicationContext())
                {
                    if (WorL)
                    {
                        MainViewModel.User.History.SlotWgames += 1;
                        MainViewModel.User.History.TotalPlus += Math.Round(Convert.ToDouble(Bid) * ThisMn);
                        MainViewModel.User.History.SlotWMoney += Math.Round(Convert.ToDouble(Bid) * ThisMn);
                    }
                    else
                    {
                        MainViewModel.User.History.SlotLgames += 1;
                        MainViewModel.User.History.TotalPlus -= Math.Round(Convert.ToDouble(Bid));
                        MainViewModel.User.History.SlotLMoney += Math.Round(Convert.ToDouble(Bid));
                    }
                    db.Update(MainViewModel.User);
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { MessageBox.Show("ПОМЕДЛЕННИЕ"); }
        }
        void ToFalse()
        {
            for (int i = 0; i < 25; i++)
                Onclick[i] = false;
        }
        void ToBlue()
        {
            for (int i = 0; i < 25; i++)
            {
                Buttons[i].Background = Brushes.Blue;  
            }
        }
    }
}
