using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public int CateId { get; set; }
        public virtual Category Cate { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
