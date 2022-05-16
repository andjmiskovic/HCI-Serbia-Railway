using SerbiaRailway.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.services
{
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

        public bool Login(string username, string password)
        {
            string p = getPassword(username);
            if (p == null) { 
                Console.WriteLine("Username does not exists.");
                return false;
            }
            return password.Equals(p);
        }

        public string getPassword(string username)
        {
            foreach(Client c in Clients)
            {
                if (c.Username.Equals(username))
                    return c.Password;
            }
            foreach (Manager m in Managers)
            {
                if (m.Username.Equals(username))
                    return m.Password;
            }
            return null;
        }
    }
}
