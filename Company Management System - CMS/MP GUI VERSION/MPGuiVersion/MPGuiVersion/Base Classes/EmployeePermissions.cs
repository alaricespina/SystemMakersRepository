using System;
using System.Collections.Generic;
using System.Text;

namespace MPGuiVersion
{
    public class EmployeePermissions
    {
        public bool accessInventory { get; set; }
        public bool accessBookkeeping { get; set; }
        public bool accessTasks { get; set; }
        public bool accessEmployees { get; set; }
        public bool accessPermissions { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }
}
