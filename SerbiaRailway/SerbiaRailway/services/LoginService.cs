using SerbiaRailway.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.services
{
    public enum UserType { CLIENT, MANAGER, UNAUTHENTICATED }

    class LoginService
    {

        public LoginService()
        {
        }

        public UserType Login(string username, string password)
        {
            UserType type = GetUserType(username);
            if (type != UserType.UNAUTHENTICATED)
            {
                if(GetPassword(username, type).Equals(password))
                    return type;
            }
            return UserType.UNAUTHENTICATED;
        }

        public UserType GetUserType(string username)
        {
            foreach (Client c in DataService.Data.Clients)
            {
                if (c.Username.Equals(username))
                    return UserType.CLIENT;
            }
            foreach (Manager m in DataService.Data.Managers)
            {
                if (m.Username.Equals(username))
                    return UserType.MANAGER;
            }
            return UserType.UNAUTHENTICATED;
        }

        public string GetPassword(string username, UserType type)
        {
            if (UserType.CLIENT == type)
            {
                foreach (Client c in DataService.Data.Clients)
                {
                    if (c.Username.Equals(username))
                        return c.Password;
                }
            }
            if (UserType.MANAGER == type)
            {
                foreach (Manager m in DataService.Data.Managers)
                {
                    if (m.Username.Equals(username))
                        return m.Password;
                }
            }
            return null;
        }
    }
}
