using System.Windows.Input;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.Json;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Autoservice.Desktop.ViewModels.Json;
using System;

namespace Autoservice.Desktop.ViewModels
{
    internal class SignInViewModel : ViewModel
    {
        public SignInViewModel()
        {
            OnLoginClick = new RelayCommand(LogInAsync, CanLogin);
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
        #region Login
        //проверка на поля
        private bool CanLogin(object obj)
        {
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                return false;
            }
            return true;
        }

        //сам вход
        private async void LogInAsync(object obj)
        {
            try
            {
                var users = JsonSign.GetUserList();
                foreach (var user in users.UserList)
                {
                    if (user.Login == Login && 
                        user.Password == Password)
                    {
                        Configuration.IsAdmin = user.IsAdmin;
                        OpenViewModel.MainNavigator.CurrentViewModel = new MainViewModel();
                        return;
                    }
                }
                throw new Exception("Неправильно введён пароль/логин или пользователя не существует.");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public ICommand OnLoginClick { get; }

        #endregion

        #region SignIn
        public ICommand OnSignInClick => OpenViewModel.MainNavigator.UpdateCurrentViewModelCommand;
        #endregion

        
    }
    
}
