using System.Collections.Generic;

namespace SerbiaRailway.model
{
    public class Managers
    {
        public List<Manager> ManagerUsers { get; set; }

        public Managers()
        {
        }

        public Managers(List<Manager> managerUsers)
        {
            ManagerUsers = managerUsers;
        }
    }
}
