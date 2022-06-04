using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class Clients
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
