using AAAcasino.ViewModels.Base;

namespace AAAcasino.ViewModels.ClientViewModels.UserViewModels
{
    internal class ProfileViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Профиль";
        public MainWindowViewModel MainViewModel {  get; set; }
        public void SetAnyModel(object? model)  {}
        #endregion
    }
}
