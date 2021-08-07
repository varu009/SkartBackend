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
    public class EmailController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public EmailController(onlineshoppingContext context)
        {
            db = context;
        }
        public bool CheckEmail(string email)
        {
            var isValidEmail = db.TblUsers.Where(w => w.Useremail == email).FirstOrDefault();
            if (isValidEmail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        [Route("SendUserEmail")]
        [HttpGet]
        public async Task<int> SendEmail(string to)
        {
            if (CheckEmail(to) == true)
            {
                string from = "onlineshoppinglti@gmail.com";
                string subject = "Welcome to online shopping";
                Random generator = new Random();
                int r = generator.Next(1000, 10000);
                string body = "Hello , Your otp is " + r;

                SmtpClient smtp = new SmtpClient();

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(from);
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                await Task.Run(() => smtp.SendAsync(mm, null));
                return r;
            }
            else
            {
                return 0;
            }

        }
        public bool CheckRetailerEmail(string email)
        {
            var isValidEmail = db.TblRetailers.Where(w => w.Retaileremail == email).FirstOrDefault();
            if (isValidEmail != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        [Route("SendRetailerEmail")]
        [HttpGet]
        public async Task<int> SendRetailerEmail(string to)
        {
            if (CheckRetailerEmail(to) == true)
            {
                string from = "onlineshoppinglti@gmail.com";
                string subject = "Welcome to online shopping";
                Random generator = new Random();
                int r = generator.Next(1000, 10000);
                string body = "Hello , Your otp is " + r;

                SmtpClient smtp = new SmtpClient();

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(from);
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                await Task.Run(() => smtp.SendAsync(mm, null));
                return r;
            }
            else
            {
                return 0;
            }

        }

        [Route("UpdateUserPassword")]
        [HttpPut]
        public dynamic UpdatePassword(string email, string password)
        {
            //var query = from user in tblUser where user.email == email select user;
            var query = db.TblUsers.Find(email);
            query.Userpassword = password;
            db.Entry(query).State =EntityState.Modified;
            db.SaveChanges();
            // return Request.CreateResponse(HttpStatusCode.OK, "Valid");
            return Ok("Valid");
        }

        public int getid(string retaileremail)
        {
            TblRetailer retailer = new TblRetailer();
            retailer.Retailerid = db.TblRetailers.First(x => x.Retaileremail == retaileremail).Retailerid;
            return retailer.Retailerid;
        }

        [Route("UpdateRetailerPassword")]
        [HttpPut]
        public dynamic UpdateRetailerPassword(string retaileremail, string retailerpassword)
        {
            //var query = from user in tblUser where user.email == email select user;
            int retailerid = getid(retaileremail);
            var query = db.TblRetailers.Find(retailerid);
            query.Retailerpassword = retailerpassword;
           db.Entry(query).State = EntityState.Modified;
            db.SaveChanges();
            // return Request.CreateResponse(HttpStatusCode.OK, "Valid");
            return Ok("Valid");
        }

    }
}
