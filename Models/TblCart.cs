using System;
using System.Collections.Generic;

#nullable disable

namespace Skartwebapi.Models
{
    public partial class TblCart
    {
        public int Cartid { get; set; }
        public int Productid { get; set; }
        public int? Cartquantity { get; set; }
        public string? Useremail { get; set; }

        public virtual TblProduct Product { get; set; }
    }
}
