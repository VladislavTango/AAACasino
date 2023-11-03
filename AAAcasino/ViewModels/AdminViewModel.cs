using AAAcasino.ViewModels.Base;
using System.Windows.Navigation;

namespace AAAcasino.ViewModels
{
    internal class AdminViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Админ";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { return; }
        #endregion
        private IPageViewModel _selectedPageViewModel;
        public IPageViewModel SelectedPageViewModel
        {
            get => _selectedPageViewModel;
            set
            {
                Set(ref _selectedPageViewModel, value);
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}
