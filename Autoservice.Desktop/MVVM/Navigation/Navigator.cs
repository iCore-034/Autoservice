using Autoservice.Desktop.ViewModels;

namespace Autoservice.Desktop.MVVM.Navigation
{
    internal class Navigator : ViewModel, INavigator
    {
        public Navigator() { }
        public Navigator(ViewModel currentViewModel)
        {
            _currentViewModel = currentViewModel;
            UpdateCurrentViewModelCommand = new RelayCommand(UpdateCurrentViewModelExecute, (obj) => true);
        }
        private ViewModel _currentViewModel;
        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        public Command UpdateCurrentViewModelCommand { get; }

        private void UpdateCurrentViewModelExecute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    //таблицы
                    case ViewType.Car:
                        CurrentViewModel = new CarViewModel();
                        break;
                    case ViewType.Order:
                        CurrentViewModel = new OrderViewModel();
                        break;
                    case ViewType.Service:
                        CurrentViewModel = new ServiceViewModel();
                        break;
                    case ViewType.ServiceOrder:
                        CurrentViewModel = new ServiceOrderViewModel();
                        break;
                    case ViewType.Client:
                        CurrentViewModel = new ClientViewModel();
                        break;
                    case ViewType.Employee:
                        CurrentViewModel = new EmployeeViewModel();
                        break;
                    case ViewType.Position:
                        CurrentViewModel = new PositionViewModel();
                        break;

                    case ViewType.SignUp:
                        CurrentViewModel = new SignUpViewModel();
                        break;
                    case ViewType.SignIn:
                        CurrentViewModel = new SignInViewModel();
                        break;

                    default:
                        CurrentViewModel = new MainViewModel();
                        break;
                }
            }
            else
            {
                CurrentViewModel = new MainViewModel();
            }
        }


    }
}
