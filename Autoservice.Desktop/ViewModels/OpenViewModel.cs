using Autoservice.Desktop.MVVM.Navigation;

namespace Autoservice.Desktop.ViewModels
{
    internal class OpenViewModel : ViewModel
    {
        public static INavigator MainNavigator { get; set; } = new Navigator(new SignInViewModel());
    }
}
