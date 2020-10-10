using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Order
    {
        #region properties
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        #endregion
    }
}
