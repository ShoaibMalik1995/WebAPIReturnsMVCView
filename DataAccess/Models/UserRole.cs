using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string[] LockerURL { get; set; }
    }
}
