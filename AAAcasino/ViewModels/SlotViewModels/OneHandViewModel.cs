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
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AAAcasino.ViewModels.SlotViewModels
{
    class OneHandViewModel : ViewModel, IPageViewModel
    {
        bool IsGameStarted = false;
        double NowBet;
        string lastnormtbox;
        public string Title => "aaaaaaaaaaaaaaaaaa";
        public ObservableCollection<ImageSource> squares { get; private set; }
        public ObservableCollection<ImageSource> TwoCombos { get; private set; }
        public ObservableCollection<ImageSource> ThreeCombos { get; private set; }
        public void SetAnyModel(object? model)
        {
            balance = $"Баланс: {(double)MainViewModel.User.Balance}";
        }

        public Dictionary<int, string> images = new Dictionary<int, string>()
        {
            [0] = @"/Resources/OneHandMat.jpg",
            [1] = @"/Resources/OneHandMat-2.jpg",
            [2] = @"/Resources/OneHandMat-3.jpg",
            [3] = @"/Resources/OneHandMat-9.jpg",
            [4] = @"/Resources/OneHandMat-5.jpg",
            [5] = @"/Resources/OneHandMat-6.jpg",
            [6] = @"/Resources/OneHandMat-8.jpg",
            [7] = @"/Resources/OneHandMat-7.jpg",
            [8] = @"/Resources/OneHandMat-4.jpg",
            [9] = @"/Resources/OneHandMat.jpg",
            [10] = @"/Resources/OneHandMat-2.jpg",
            [12] = @"/Resources/OneHandMat-3.jpg",
            [13] = @"/Resources/OneHandMat-5.jpg",
            [14] = @"/Resources/OneHandMat-6.jpg",
            [15] = @"/Resources/OneHandMat-7.jpg",
            [16] = @"/Resources/OneHandMat-8.jpg",
            [17] = @"/Resources/OneHandMat-9.jpg",
            [18] = @"/Resources/OneHandMat.jpg",
            [19] = @"/Resources/OneHandMat-2.jpg",
            [20] = @"/Resources/OneHandMat-3.jpg",
            [21] = @"/Resources/OneHandMat-5.jpg",
            [22] = @"/Resources/OneHandMat-6.jpg",
            [23] = @"/Resources/OneHandMat-8.jpg",
            [11] = @"/Resources/OneHandMat-9.jpg"
            

        };
     

        public OneHandViewModel() {
            squares = SetSlot(squares);
            TwoCombos = CombosSet(TwoCombos,0);
            ThreeCombos=CombosSet(ThreeCombos,1);
            iia = new LamdaCommand(OniiaCommand, CaniiaCommand);
            start = new LamdaCommand(OnStartCommand, CanStartCommand);
            BackToMenu = new LamdaCommand(OnBackToMenuCommand, CanBackToMenuCommand);
        }
        public ICommand BackToMenu { get; }
        private bool CanBackToMenuCommand(object parameter) => true;
        private void OnBackToMenuCommand(object parameter)
        {
            //хуй говна поклюй
        }

        async void Go(int num,int row)
        {
            await Task.Delay(500*row);
            for (int i = 1; i <= num; i++)
            {
                await Task.Run(async () =>
                {
                    if (IsGameStarted)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            squares[row] = new BitmapImage(new Uri(images[RetIndex(i + 1)], UriKind.RelativeOrAbsolute));
                            squares[row + 3] = new BitmapImage(new Uri(images[RetIndex(i + 2)], UriKind.RelativeOrAbsolute));
                            squares[row + 6] = new BitmapImage(new Uri(images[RetIndex(i + 3)], UriKind.RelativeOrAbsolute));
                        });
                        await Task.Delay(50);
                    }
                    else
                    {
                        i = num;
                    }
                }
                );
            }
            if (row == 2)
            {
                IsGameStarted = false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Winner();
                });
            }
        }
        private string _Bet="Ставка";
        public string Bet
        {
            get => _Bet;
            set => Set(ref _Bet,NormalFormOfTextBox(value));
        }
        private string _balance;
        public string balance
        {
            get => _balance;
            set => Set(ref _balance,value);
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
        public ICommand start { get; }
        private bool CanStartCommand(object parameter) => true;
        private void OnStartCommand(object parameter)
        {
            if (IsGameStarted == false)
            {
                try
                {
                    NowBet = Convert.ToDouble(Bet);
                    MainViewModel.User.Balance = MainViewModel.User.Balance - NowBet;
                    balance = "Баланс:" + Convert.ToString(MainViewModel.User.Balance);
                    Random rnd = new Random();
                    IsGameStarted = true;
                    Task.Run(() => { Go(rnd.Next(100, 150), 0); });
                    Task.Run(() => { Go(rnd.Next(150, 200), 1); });
                    Task.Run(() => { Go(rnd.Next(200, 250), 2); });
                }
                catch { MessageBox.Show("Сделай нормальнную ставку"); }
            }
            else
                IsGameStarted = false;

        }//добавление в лист ставок
        int RetIndex(int index)
        {
            if (index >= 23)
            {
                return RetIndex(index - 23);                
            }
            return index;
        }
        ObservableCollection<ImageSource> CombosSet(ObservableCollection<ImageSource> sq,int set)
        {
            sq = new ObservableCollection<ImageSource>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3+set; j++) {
                    if (j != 3+set-1)
                    {
                        sq.Add(new BitmapImage(new Uri(images[i], UriKind.RelativeOrAbsolute)));
                    }
                    else {
                        sq.Add(new BitmapImage(new Uri(string.Empty, UriKind.RelativeOrAbsolute)));
                    }
                }

            }
            return sq;
        }
        ObservableCollection<ImageSource> SetSlot(ObservableCollection<ImageSource> sq)
        {
            sq = new ObservableCollection<ImageSource>();
            for (int i = 0; i < 9; i++)
            {
                //MessageBox.Show(System.IO.Path.GetFullPath(images[i]));
                if(i<=2)
                    sq.Add(new BitmapImage(new Uri(images[0],UriKind.RelativeOrAbsolute)));
                if(i>2&&i<=5)
                    sq.Add(new BitmapImage(new Uri(images[8], UriKind.RelativeOrAbsolute)));
                if(i>5)
                    sq.Add(new BitmapImage(new Uri(images[6], UriKind.RelativeOrAbsolute)));
            }
            return sq;
        }
        #region event
        public ICommand iia { get; }
        private bool CaniiaCommand(object parameter) => true;
        private void OniiaCommand(object parameter)
        {
            if (Bet == "Ставка") { Bet = string.Empty; }
        }
        #endregion
        public MainWindowViewModel MainViewModel { get; set; }

        
        void Winner() {
            string[] str = squares[3].ToString().Split("pack://application:,,,");
            if (squares[3].ToString() == squares[4].ToString() && squares[4].ToString() == squares[5].ToString()) {
                if (str[1] == images[0].ToString()) (NowBet) *= 10;
                if (str[1] == images[1].ToString()) (NowBet) *= 20;
                if (str[1] == images[2].ToString()) (NowBet) *= 40;
                if (str[1] == images[3].ToString()) (NowBet) *= 50;
                if (str[1] == images[4].ToString()) (NowBet) *= 60;
                if (str[1] == images[5].ToString()) (NowBet) *= 70;
                if (str[1] == images[6].ToString()) (NowBet) *= 80; 
                if (str[1] == images[7].ToString()) (NowBet) *= 100;
                if (str[1] == images[8].ToString()) (NowBet) *= 150;
            }
            else if (squares[3].ToString() == squares[4].ToString()) {
                MessageBox.Show("да");
                if (squares[3].ToString() == images[1].ToString())(NowBet) *= 2;
                if (str[1] == images[2].ToString())(NowBet) *= 4;
                if (str[1] == images[3].ToString())(NowBet) *= 5;
                if (str[1] == images[4].ToString())(NowBet) *= 6;
                if (str[1] == images[5].ToString())(NowBet) *= 7;
                if (str[1] == images[6].ToString())(NowBet) *= 8;
                if (str[1] == images[7].ToString())(NowBet) *= 10;
                if (str[1] == images[8].ToString())(NowBet) *= 15;

            }
            else if (str[1] != squares[4].ToString()) NowBet = 0;
                balance = $"Баланс: {(double)MainViewModel.User.Balance+NowBet}";
        }
    }

}
