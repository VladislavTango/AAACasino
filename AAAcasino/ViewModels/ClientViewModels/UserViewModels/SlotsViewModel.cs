using AAAcasino.ViewModels.Base;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class SlotsViewModel : ViewModel, IPageViewModel
    {
        public string Title => "Слоты";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { }
    }
}
