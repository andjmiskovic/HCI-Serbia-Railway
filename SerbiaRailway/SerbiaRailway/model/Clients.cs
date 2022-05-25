using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerbiaRailway.model
{
    class Clients
    {
        public List<Client> ClientUsers { get; set; }

        public Clients()
        {
        }

        public Clients(List<Client> clientUsers)
        {
            ClientUsers = clientUsers;
        }
    }
}
