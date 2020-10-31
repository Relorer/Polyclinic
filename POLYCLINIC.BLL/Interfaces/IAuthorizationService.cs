using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.BLL.Interfaces
{
    public interface IAuthorizationService
    {
        bool LogIn(string login, string password);

        void LogOut();

        User GetCurrentUser();
    }
}
