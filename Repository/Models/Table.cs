using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Order>();
        }

        public int TableId { get; set; }
        public bool Status { get; set; }
        public string TableName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
