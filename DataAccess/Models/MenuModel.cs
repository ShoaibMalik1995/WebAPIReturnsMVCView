using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class MenuModel
    {       
        public Menu menu { get; set; }        
    }

    public class Menu
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; }
        public string MeneURL { get; set; }
        public string MenuId { get; set; }
        public string Description { get; set; }
        public int AcademyId { get; set; }
        public string GroupName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
