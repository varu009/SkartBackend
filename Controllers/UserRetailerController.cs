using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skartwebapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skartwebapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserRetailerController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public UserRetailerController(onlineshoppingContext context)
        {
            db = context;
        }
        [Route("GetUserProfile")]
        public IActionResult GetUserProfile(string uemail)
        {
            DateTime newdate;

            var uprof = (from u in db.TblUsers
                         join o in db.TblOrders
                         on u.Userid equals o.Userid//useremail
                         join p in db.TblProducts
                         on o.Productid equals p.Productid
                         select new
                         {
                             u.Useremail,
                             u.Username,
                             u.Userphone,
                             u.Useraddress,
                             //u.Userapartment,
                             //u.Userstreet,
                             //u.Usertown,
                             //u.Userstate,
                             //u.Userpincode,
                             //u.Usercountry,
                             o.Orderid,
                             o.Orderdate,
                             p.Productname,
                             p.Productprice,
                             p.Productbrand,
                             p.Productdescription,
                             o.Orderquantity,

                         }).Where(u => u.Useremail == uemail).ToList();

            return Ok( uprof);
        }
        [Route("GetRetailerProfile")]
        public IActionResult GetRetailerProfile(string retaileremail)
        {
            var rprof = (from r in db.TblRetailers
                         join p in db.TblProducts
                         on r.Retailerid equals p.Retailerid
                         join o in db.TblOrders
                         on p.Productid equals o.Productid
                         let RetailerRevenue = p.Productprice * o.Orderquantity
                         select new
                         {
                             r.Retailerid,
                             r.Retailername,
                             r.Retaileremail,
                             p.Productname,
                             p.Productprice,
                             p.Productbrand,
                             o.Useremail,
                             o.Orderquantity,
                             RetailerRevenue

                         }).Where(r => r.Retaileremail == retaileremail).ToList();

            return Ok( rprof);
        }

        [Route("GetProfile")]
        [HttpGet]
        public IActionResult GetProfile(string uemail)
        {
            var result = db.TblUsers.Where(x => x.Useremail == uemail).ToList();
            return Ok( result);
        }
    }
}
