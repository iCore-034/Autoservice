using Autoservice.Desktop.MVVM.Navigation;

namespace Autoservice.Desktop.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        public INavigator Navigator { get; set; } = new Navigator(new CarViewModel());
    }
}
