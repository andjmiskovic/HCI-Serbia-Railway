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
        private List<Client> Clients { get; set; }
        private List<Manager> Managers { get; set; }

        public LoginService()
        {
            Clients = new List<Client>
            {
                new Client("pera", "123", "Pera", "Peric")
            };

            Managers = new List<Manager>
            {
               new Manager("ana", "123", "Ana", "Anicic")
            };
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
            foreach (Client c in Clients)
            {
                if (c.Username.Equals(username))
                    return UserType.CLIENT;
            }
            foreach (Manager m in Managers)
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
                foreach (Client c in Clients)
                {
                    if (c.Username.Equals(username))
                        return c.Password;
                }
            }
            if (UserType.MANAGER == type)
            {
                foreach (Manager m in Managers)
                {
                    if (m.Username.Equals(username))
                        return m.Password;
                }
            }
            return null;
        }
    }
}
