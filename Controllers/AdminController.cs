using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skartwebapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Skartwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public AdminController(onlineshoppingContext context)
        {
            db = context;
        }
        [Route("admin-dashboard")]
        [HttpGet]
        public IActionResult getRetailers()
        {
            var retailers = db.TblRetailers.Where(x => x.Approved == 0).ToList();
            if (retailers.Count > 0)
            {
                // return Request.CreateResponse(HttpStatusCode.OK, retailers);
                return Ok(retailers);
            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "No retailers");
                return Ok( "No Retailers" );
            }
        }
        [Route("approve-retailer")]
        [HttpPut]
        public IActionResult ApproveRetailer(int retailerid, string retaileremail)
        {
            var retailer = db.TblRetailers.Find(retailerid);
            retailer.Approved = 1;
            db.Entry(retailer).State = EntityState.Modified;
            db.SaveChanges();
            // return Request.CreateResponse(HttpStatusCode.OK, "Approved");
            return Ok("Approved");
        }

        [Route("send-email")]
        [HttpGet]
        public IActionResult SendEmail(string retaileremail)
        {
            string from = "onlineshoppinglti@gmail.com";
            string subject = "Welcome to online shopping";
            string body = "Hello , online shopping welcomes you to be a contributor as a retailer";
            SmtpClient smtp = new SmtpClient();
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(from);
            mm.To.Add(retaileremail);
            mm.Subject = subject;
            mm.Body = body;
            smtp.Send(mm);
            // return Request.CreateResponse(HttpStatusCode.OK, "Done");
            return Ok("Done");
        }
    }
}
