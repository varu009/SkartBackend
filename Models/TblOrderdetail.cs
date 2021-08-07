using System;
using System.Collections.Generic;

#nullable disable

namespace Skartwebapi.Models
{
    public partial class TblOrderdetail
    {
        public int? Orderid { get; set; }
        public int? Productid { get; set; }
        public int? Userid { get; set; }
        public int? Orderquantity { get; set; }
        public double? Orderprice { get; set; }

        public virtual TblOrder Order { get; set; }
        public virtual TblProduct Product { get; set; }
        public virtual TblUser User { get; set; }
    }
}
