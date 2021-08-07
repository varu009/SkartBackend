using System;
using System.Collections.Generic;

#nullable disable

namespace Skartwebapi.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int Userid { get; set; }
        public string Username { get; set; }
        public string Useremail { get; set; }
        public string Userphone { get; set; }
        public string Userpassword { get; set; }
        public string Useraddress { get; set; }

        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
