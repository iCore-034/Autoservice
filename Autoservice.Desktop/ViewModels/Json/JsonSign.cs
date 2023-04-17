using System.IO;
using System.Text.Json;

namespace Autoservice.Desktop.ViewModels.Json
{
    internal static class JsonSign
    {
        private static string _path = @"Settings\Users.json";
        public static Users GetUserList()
        {
            return JsonSerializer.Deserialize<Users>(File.ReadAllText(_path));
        }

        public static void PutUser(User user)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            Users users = GetUserList();
            users.UserList.Add(user);

            string jsonString = JsonSerializer.Serialize(users, options);
            File.WriteAllText(_path, jsonString);
        }
    }
}
