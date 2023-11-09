using AAAcasino.ViewModels.Base;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class QuizzesViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Викторины";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) {}
        #endregion
    }
}
