using System;
using System.Collections.Generic;

#nullable disable

namespace Skartwebapi.Models
{
    public partial class TblOrder
    {
        public int Orderid { get; set; }
        public DateTime? Orderdate { get; set; }
        public int? Userid { get; set; }
        public string? Useremail { get; set; }
        public int? Productid { get; set; }
        public int? Orderquantity { get; set; }
        public double? Orderprice { get; set; }
        public int? Retailerid { get; set; }

        public virtual TblProduct Product { get; set; }
        public virtual TblUser User { get; set; }
    }
}
