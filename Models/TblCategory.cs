using System;
using System.Collections.Generic;

#nullable disable

namespace Skartwebapi.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        public int Categoryid { get; set; }
        public string Categoryname { get; set; }
        public string Categorydescription { get; set; }

        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
