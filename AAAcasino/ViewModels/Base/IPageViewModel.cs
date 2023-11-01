namespace AAAcasino.ViewModels.Base
{
    internal interface IPageViewModel
    {
        string Title { get; }
        MainWindowViewModel MainViewModel { get; set; }
        public void SetAnyModel(object? model);
    }
}