using AAAcasino.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using LiveCharts.Wpf;
using LiveCharts;
using System.Windows;
using System.Windows.Input;
using AAAcasino.Infrastructure.Commands;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class UserProfileViewModel : ViewModel, IPageViewModel
    {
        public string Title =>"ПРОФИЛЬ";

        public MainWindowViewModel MainViewModel { get ; set ; }
        public void SetAnyModel(object? model)
        {
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Победы",
                    Values = new ChartValues<double> { MainViewModel.User.History.RouletteWgames },
                    PushOut = 15,
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Green},
                new PieSeries
                {
                    Title = "Поражения",
                    Values = new ChartValues<double> {MainViewModel.User.History.RouletteLgames},
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Red

                },
            };//рулетка
            SeriesCollection1 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Получил",
                    Values = new ChartValues<double> { MainViewModel.User.History.OneHandWMoney },
                    PushOut = 15,
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Green},
                new PieSeries
                {
                    Title = "Проиграл",
                    Values = new ChartValues<double> {MainViewModel.User.History.OneHandLMoney},
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Red

                },
            };//слоты
            SeriesCollection2 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Победы",
                    Values = new ChartValues<double> { MainViewModel.User.History.SlotWgames },
                    PushOut = 15,
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Green},
                new PieSeries
                {
                    Title = "Поражения",
                    Values = new ChartValues<double> {MainViewModel.User.History.SlotLgames},
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Red

                },
            };//минёр
            SeriesCollection3 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Получил",
                    Values = new ChartValues<double> { MainViewModel.User.History.RouletteWMoney },
                    PushOut = 15,
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Green},
                new PieSeries
                {
                    Title = "Проиграл",
                    Values = new ChartValues<double> {MainViewModel.User.History.RouletteLMoney},
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Red

                },
            };//рулетка
            SeriesCollection4 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Победы",
                    Values = new ChartValues<double> { MainViewModel.User.History.OneHandWgames },
                    PushOut = 15,
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Green},
                new PieSeries
                {
                    Title = "Поражения",
                    Values = new ChartValues<double> {MainViewModel.User.History.OneHandLgames},
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Red

                },
            };//слоты
            SeriesCollection5 = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Получил",
                    Values = new ChartValues<double> { MainViewModel.User.History.SlotWMoney },
                    PushOut = 15,
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Green},
                new PieSeries
                {
                    Title = "Проиграл",
                    Values = new ChartValues<double> {MainViewModel.User.History.SlotLMoney},
                    DataLabels = true,
                    Fill=System.Windows.Media.Brushes.Red

                },
            };//минёр



            Name = MainViewModel.User.Username;
            balance = MainViewModel.User.Balance.ToString();
            TotalLoose = MainViewModel.User.History.TotalLoose.ToString();
            TotalWin = MainViewModel.User.History.TotalWin.ToString();
            TotalPlus = MainViewModel.User.History.TotalPlus.ToString();

        }
        public UserProfileViewModel() {
            //double da = 150;
            //SeriesCollection = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Победы",
            //        Values = new ChartValues<double> { MainViewModel.User.History.RouletteLgames },
            //        PushOut = 15,
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Red},
            //    new PieSeries
            //    {
            //        Title = "Поражения",
            //        Values = new ChartValues<double> {56},
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Green

            //    }, 
            //};//рулетка
            //SeriesCollection1 = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Получил",
            //        Values = new ChartValues<double> { 80 },
            //        PushOut = 15,
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Red},
            //    new PieSeries
            //    {
            //        Title = "Проиграл",
            //        Values = new ChartValues<double> {20},
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Green

            //    },
            //};//слоты
            //SeriesCollection2 = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Победы",
            //        Values = new ChartValues<double> { 55 },
            //        PushOut = 15,
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Red},
            //    new PieSeries
            //    {
            //        Title = "Поражения",
            //        Values = new ChartValues<double> {45},
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Green

            //    },
            //};//минёр
            //SeriesCollection3 = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Получил",
            //        Values = new ChartValues<double> { 22 },
            //        PushOut = 15,
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Red},
            //    new PieSeries
            //    {
            //        Title = "Проиграл",
            //        Values = new ChartValues<double> {78},
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Green

            //    },
            //};//рулетка
            //SeriesCollection4 = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Победы",
            //        Values = new ChartValues<double> { 33 },
            //        PushOut = 15,
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Red},
            //    new PieSeries
            //    {
            //        Title = "Поражения",
            //        Values = new ChartValues<double> {67},
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Green

            //    },
            //};//слоты
            //SeriesCollection5 = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Получил",
            //        Values = new ChartValues<double> { 51 },
            //        PushOut = 15,
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Red},
            //    new PieSeries
            //    {
            //        Title = "Проиграл",
            //        Values = new ChartValues<double> {49},
            //        DataLabels = true,
            //        Fill=System.Windows.Media.Brushes.Green

            //    },
            //};//минёр
            BackToMenu = new LamdaCommand(OnBackToMenuCommand, CanBackToMenuCommand);

        }
        public ICommand BackToMenu { get; }

        private bool CanBackToMenuCommand(object parameter) => true;
        private void OnBackToMenuCommand(object parameter)
        {
            MainViewModel.SelectedPageViewModel = MainViewModel.ClientPageViewModels[(int)NumberClientPage.USER_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
            MainViewModel.SelectedPageViewModel.SetAnyModel(null);

        }


        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public SeriesCollection SeriesCollection2 { get; set; }
        public SeriesCollection SeriesCollection3 { get; set; }
        public SeriesCollection SeriesCollection4 { get; set; }
        public SeriesCollection SeriesCollection5 { get; set; }



        private void Chart_OnDataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show($"{chartPoint.SeriesView.Title} sold {chartPoint.Y} widgets.");
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }
        private string _balance;
        public string balance
        {
            get => _balance;
            set => Set(ref _balance, value);
        }
        private string _TotalWin;
        public string TotalWin
        {
            get => _TotalWin;
            set => Set(ref _TotalWin, value);
        }
        private string _TotalLoose;
        public string TotalLoose
        {
            get => _TotalLoose;
            set => Set(ref _TotalLoose, value);
        }
        private string _TotalPlus;
        public string TotalPlus
        {
            get => _TotalPlus;
            set => Set(ref _TotalPlus, value);
        }

    }

       
    
}
