using SerbiaRailway.model;

namespace SerbiaRailway.services
{
    public enum UserType { CLIENT, MANAGER, UNAUTHENTICATED }

    class LoginService
    {
        public static Client CurrentlyLoggedClient { get; set; }

        public static UserType Login(string username, string password)
        {
            UserType type = GetUserType(username);
            if (type != UserType.UNAUTHENTICATED)
            {
                if (GetPassword(username, type).Equals(password))
                {
                    if (type == UserType.CLIENT)
                        CurrentlyLoggedClient = GetClientByUsername(username);
                    return type;
                }
            }
            return UserType.UNAUTHENTICATED;
        }

        private static Client GetClientByUsername(string username)
        {
            foreach (Client c in DataService.Data.GetClients())
            {
                if (c.Username.Equals(username))
                    return c;
            }
            return null;
        }

        internal static void AddTicket(Ticket ticket)
        {
            if(ticket.State == TicketState.RESERVED)
                CurrentlyLoggedClient.Reserved.Add(ticket);
            if (ticket.State == TicketState.BOUGHT)
                CurrentlyLoggedClient.Bought.Add(ticket);
        }

        public static UserType GetUserType(string username)
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

        public static string GetPassword(string username, UserType type)
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
