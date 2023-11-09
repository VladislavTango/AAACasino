using AAAcasino.ViewModels.Base;
using AAAcasino.ViewModels;
using System.Collections.Generic;
using System.Windows;
using AAAcasino.ViewModels.SlotViewModels;
using AAAcasino.ViewModels.ClientViewModels;
using AAAcasino.ViewModels.ClientViewModels.AdminViewModels;
using AAAcasino.ViewModels.ClientViewModels.UserViewModels;

namespace AAAcasino
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new MainWindow()
            {
                DataContext = new MainWindowViewModel()
                {
                    ClientPageViewModels = new List<IPageViewModel>
                    {
                        new LogInViewModel(),
                        new DefaultUserViewModel(),
                        new ProfileViewModel(),
                        new QuizzesViewModel(),
                        new SlotsViewModel(),
                        new AdminViewModel(),
                        new CreationQuizViewModel(),
                    },
                    SlotPageViewModels = new List<IPageViewModel>
                    {
                        new SlotMineViewModel()
                    }
                }
            }.Show();
        }
    }
}