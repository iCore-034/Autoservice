using System.Windows.Input;
using System.Windows;
using System;
using System.Text.Json;
using System.IO;
using Autoservice.Desktop.ViewModels.Json;

namespace Autoservice.Desktop.ViewModels
{
    internal class SignUpViewModel : ViewModel
    {
        public SignUpViewModel()
        {
            OnSignInClick = new RelayCommand(SignInAsync, CanLogin);
        }
        private string _login;
        private string _password;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #region SignIn

        public ICommand OnSignInClick { get; }
        //проверка на поля
        private bool CanLogin(object obj)
        {
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                return false;
            }
            return true;
        }

        //сама регистрация
        private async void SignInAsync(object obj)
        {
            try
            {
                User user = new()
                {
                    Login = _login,
                    Password = _password,
                    IsAdmin = false
                };
                
                JsonSign.PutUser(user);

                OpenViewModel.MainNavigator.CurrentViewModel = new SignInViewModel();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        #endregion

        #region Back
        public ICommand OnBackClick => OpenViewModel.MainNavigator.UpdateCurrentViewModelCommand;
        #endregion
    }
}
