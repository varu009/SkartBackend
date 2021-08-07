using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skartwebapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skartwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblRetailersController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public TblRetailersController(onlineshoppingContext context)
        {
            db = context;
        }
        [Route("retailer-login")]
        [HttpGet]
        public IActionResult getRetailer(string retaileremail, string retailerpassword)
        {
            var retailer = db.TblRetailers.Where(x => x.Retaileremail == retaileremail
            && x.Retailerpassword == retailerpassword && x.Approved == 1).ToList();
            if (retailer.Count > 0)
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "valid");
                return Ok(new { Status = "ok", Message = "Success" });
            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "Invalid");
                return Ok(new { Status = "ok", Message = "Invalid" });

            }
        }

        [Route("get-retailerid")]
        [HttpGet]
        public IActionResult getRetailerId(string retaileremail)
        {
            var retailer = db.TblRetailers.Where(x => x.Retaileremail == retaileremail).Select(x => x.Retailerid);
            if (retailer != null)
            {
                //return Request.CreateResponse(HttpStatusCode.OK, retailer);
                return Ok(new { Status = "ok",retailer });
            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "Invalid");
                return Ok(new { Status = "ok", Message = "Invalid" });
            }
        }

        [Route("retailer-register")]
        [HttpPost]

        public IActionResult register(string retailername, string retaileremail, string retailerpassword)
        {
            try
            {
                TblRetailer retailer = new TblRetailer()
                {
                    Retailername = retailername,
                    Retaileremail = retaileremail,
                    Retailerpassword = retailerpassword,
                    Approved = 0

                };

                db.TblRetailers.Add(retailer);
                db.SaveChanges();
                // return Request.CreateResponse(HttpStatusCode.OK, "valid");
                return Ok(new { Status = "ok", Message = "Success" });
            }
            catch (Exception e)
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "invalid");
                return Ok(new { Status = "ok", Message = "Invalid" });
            }
        }

        public bool CheckRetailer(string retaileremail, string oldpassword)
        {
            var result = db.TblRetailers.Where(x => x.Retaileremail == retaileremail);
            try
            {
                var pass = db.TblRetailers.Where(x => x.Retailerpassword == oldpassword);
            }
            catch (Exception e)
            {
                return false;
            }
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getid(string retaileremail, string retailerpassword)
        {
            TblRetailer retailer = new TblRetailer();
            if (CheckRetailer(retaileremail, retailerpassword))
            {
                try
                {
                    retailer.Retailerid = db.TblRetailers.First(x => x.Retaileremail == retaileremail &&
                    x.Retailerpassword == retailerpassword).Retailerid;
                    return retailer.Retailerid;
                }
                catch (Exception e)
                {
                    return 0;
                }

            }
            return 0;

        }

        [Route("ChangePassword")]
        [HttpPut]
        public IActionResult ChangePass(string retaileremail, string retailerpassword, string newpassword)
        {

            if (CheckRetailer(retaileremail, retailerpassword))
            {
                int retailerid = getid(retaileremail, retailerpassword);
                if (retailerid != 0)
                {
                    var query = db.TblRetailers.Find(retailerid);
                    query.Retailerpassword = newpassword;
                   db.Entry(query).State = EntityState.Modified;
                    db.SaveChanges();
                    // return Request.CreateResponse(HttpStatusCode.OK, "valid");
                    return Ok(new { Status = "ok", Message = "Success" });
                }
                else
                {
                    //return Request.CreateResponse(HttpStatusCode.OK, "invalid");
                    return Ok(new { Status = "ok", Message = "Invalid" });
                }

            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "invalid");
                return Ok(new { Status = "ok", Message = "Invalid" });
            }
        }

    }
}
