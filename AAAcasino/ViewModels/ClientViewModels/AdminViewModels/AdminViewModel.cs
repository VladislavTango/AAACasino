using AAAcasino.Models;
using AAAcasino.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace AAAcasino.ViewModels.ClientViewModels.AdminViewModels
{
    internal class AdminViewModel : ViewModel, IPageViewModel
    {
        #region IPage
        public string Title => "Админ";
        public MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model) { return; }
        #endregion
        #region Users tab
        private ObservableCollection<UserModel> _userModels = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> UserModels
        {
            get => _userModels;
            set => Set(ref _userModels, value);
        }

        private bool _userListIsOpen = false;
        public bool UserListIsOpen
        {
            get => _userListIsOpen;
            set
            {
                Set(ref _userListIsOpen, value);

                if(!value)
                {
                    Task.Run(() =>
                    {
                        MainWindowViewModel.applicationContext.UpdateRange(UserModels);
                        MainWindowViewModel.applicationContext.SaveChanges();
                        UserModels.Clear();
                    });
                }
                else
                {
                    UserModels = new ObservableCollection<UserModel>(MainWindowViewModel.applicationContext.userModels.ToList());
                }
            }
        }
        #endregion
        #region Quizes tab
            
        #endregion
    }
}
