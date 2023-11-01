using AAAcasino.ViewModels.Base;
using AAAcasino.ViewModels;
using System.Collections.Generic;
using System.Windows;

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
                        new LogInViewModel()
                    }
                }
            }.Show();
        }
    }
}