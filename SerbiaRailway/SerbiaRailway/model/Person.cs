using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    public class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public Person(string Username, string Password, string Name, string LastName)
        {
            this.Username = Username;
            this.Password = Password;
            this.Name = Name;
            this.LastName = LastName;
        }
    }

    public class Client : Person
    {
        public List<Ticket> Reserved { get; set; }
        public List<Ticket> Bought { get; set; }

        public Client(string Username, string Password, string Name, string LastName)
            : base(Username, Password, Name, LastName)
        {
            Reserved = new List<Ticket>();
            Bought = new List<Ticket>();
        }
    }

    public class Manager : Person
    {
        public Manager(string Username, string Password, string Name, string LastName)
            : base(Username, Password, Name, LastName)
        {
        }
    }
}
