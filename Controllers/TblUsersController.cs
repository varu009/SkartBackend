using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skartwebapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Threading.Tasks;

namespace Skartwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUsersController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public TblUsersController(onlineshoppingContext context)
        {
            db = context;
        }
       // onlineshoppingEntities db = new onlineshoppingEntities();
        [Route("do-login")]
        [HttpGet]
        public IActionResult checkLogin(string useremail, string userpassword)
        {
            try
            {
                var result = db.TblUsers.Where(x => x.Useremail == useremail).FirstOrDefault();
                var pass = db.TblUsers.Where(x => x.Userpassword == userpassword).FirstOrDefault();
                if (result != null && pass != null)
                {
                    //return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    return Ok( "Success" );
                }
                else
                {
                    // return Request.CreateResponse(HttpStatusCode.OK, "Invalid");
                    return Ok( "invalid token" );
                }
            }
            catch (Exception e)
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "Invalid");
                return Ok("invalid token" );
            }
        }

        public bool CheckEmail(string email)
        {
            var isValidEmail = db.TblUsers.Where(w => w.Useremail == email).FirstOrDefault();
            if (isValidEmail == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckEmail1(string email, string userpassword)
        {
            try
            {
                var isValidEmail = db.TblUsers.Where(w => w.Useremail == email).FirstOrDefault();
                var pass = db.TblUsers.Where(w => w.Userpassword == userpassword).FirstOrDefault();
                if (isValidEmail != null && pass != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }


        [Route("RegisterUser")]
        [HttpPost]
        public IActionResult UserRegister(string useremail, string username, string userphone,
               string userpassword, string useraddress)
        {
            if (CheckEmail(useremail))
            {
                TblUser user = new TblUser()
                {
                    Useremail = useremail,
                    Username = username,
                    Userphone = userphone,
                    Userpassword = userpassword,
                    Useraddress = useraddress,
                    //Userapartment = userapartment,
                    //userstreet = userstreet,
                    //usertown = usertown,
                    //userstate = userstate,
                    //userpincode = userpincode,
                    //usercountry = usercountry
                };
                db.TblUsers.Add(user);
                db.SaveChanges();
               // return Request.CreateResponse(HttpStatusCode.OK, "success");
                return Ok("Success");
            }
            //return Request.CreateResponse(HttpStatusCode.OK, "invalid");
            return Ok("invalid token");

        }
        [Route("userchangepassword")]
        [HttpPut]
        public IActionResult changeUserPassword(string useremail, string userpassword, string newpassword)
        {
            if (CheckEmail1(useremail, userpassword))
            {
                var query = db.TblUsers.Find(useremail);
                query.Userpassword = newpassword;
                db.Entry(query).State = EntityState.Modified;
                db.SaveChanges();
               // return Request.CreateResponse(HttpStatusCode.OK, "valid");
                return Ok( "Success" );
            }
            else
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "invalid");
                //return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return Ok("invalid");
            }
        }

    }
}
