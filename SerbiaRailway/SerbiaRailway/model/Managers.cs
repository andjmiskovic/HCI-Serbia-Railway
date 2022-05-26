using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
