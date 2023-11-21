using AAAcasino.Infrastructure.Commands;
using AAAcasino.ViewModels.Base;
using ScottPlot.Renderable;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class SlotsViewModel : ViewModel, IPageViewModel
    {
        public string Title => "Слоты";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { }
        public SlotsViewModel() {
            GoToQuizCommand = new LamdaCommand(OnGoToQuizCommand, CanGoToQuizCommand);
        }

        public ICommand GoToQuizCommand { get; set; }
        private bool CanGoToQuizCommand(object quiz) => true;
        private void OnGoToQuizCommand(object quiz)
        {
            if(Convert.ToInt16(quiz)==0)
            MainViewModel.SelectedPageViewModel = MainViewModel.SlotPageViewModels[(int)NumberSlotPage.MINESLOT_PAGE];
            if (Convert.ToInt16(quiz) == 1)
                MainViewModel.SelectedPageViewModel = MainViewModel.SlotPageViewModels[(int)NumberSlotPage.ONEHAND_PAGE];
            if (Convert.ToInt16(quiz) == 2)
                MainViewModel.SelectedPageViewModel = MainViewModel.SlotPageViewModels[(int)NumberSlotPage.ROULETTE_PAGE];
            MainViewModel.SelectedPageViewModel.MainViewModel = MainViewModel;
            MainViewModel.SelectedPageViewModel.SetAnyModel(null);
        }
    }
}
