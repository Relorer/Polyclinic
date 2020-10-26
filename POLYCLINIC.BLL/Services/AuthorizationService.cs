using POLYCLINIC.BLL.Properties;
using POLYCLINIC.Data;
using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.BLL
{
    public class AuthorizationService
    {
        static BaseContext context = new BaseContext();
        public static bool LogIn(string login, string password)
        {
            context.Users.Load();
            var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                Settings.Default["login"] = login;
                Settings.Default["password"] = password;
                Settings.Default.Save();
            }
            return user != null;
        }

        public static void LogOut()
        {
            Settings.Default["login"] = string.Empty;
            Settings.Default["password"] = string.Empty;
            Settings.Default.Save();
        }

        public static User GetCurrentUser()
        {
            context.Users.Load();
            string login = Settings.Default["login"].ToString();
            string password = Settings.Default["password"].ToString();
            return context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }
    }
}
