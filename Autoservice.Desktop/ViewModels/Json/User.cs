using System.Collections;
using System.Collections.Generic;

namespace Autoservice.Desktop.ViewModels.Json
{
    class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
    class Users
    {
        public List<User> UserList { get; set; }
    }
}
