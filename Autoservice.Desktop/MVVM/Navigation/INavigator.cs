using System.Windows.Input;

namespace Autoservice.Desktop.MVVM.Navigation
{
    internal interface INavigator
    {
        ViewModel CurrentViewModel { get; set; }
        Command UpdateCurrentViewModelCommand { get; }
    }
}
