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
    public class FilterController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public FilterController(onlineshoppingContext context)
        {
            db = context;
        }
        [Route("Filterbyprice")]
        public IActionResult GetPrice(string price)
        {

            switch (price)
            {
                case "0-1999":
                    // return Request.CreateResponse(HttpStatusCode.OK, db.TblProducts.Where(s => s.Productprice < 2000).ToList());
                    return Ok(new { Status = "ok", TblProducts = db.TblProducts.Where(s => s.Productprice < 2000).ToList() });
                case "2000-9999":
                    //return Request.CreateResponse(HttpStatusCode.OK, db.TblProducts.Where(s => s.Productprice >= 2000 && s.Productprice <= 9999).ToList());
                    return Ok(new { Status = "ok", TblProducts = db.TblProducts.Where(s => s.Productprice >= 2000 && s.Productprice <= 9999).ToList() });
                case "10000-80000":
                    // return Request.CreateResponse(HttpStatusCode.OK, db.TblProducts.Where(s => s.Productprice >= 10000).ToList());
                    return Ok(new { Status = "ok", TblProducts = db.TblProducts.Where(s => s.Productprice >= 10000).ToList() });
                default:
                    // return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Product price out of range");
                    return BadRequest(" Product price out of range");
            }
        }

        [Route("Filterbycategory")]
        public IActionResult GetCategory(string category)
        {
            // IEnumerable<> lstUser = new List<UserDetail>();

            switch (category)
            {
                case "Mobile and Accessories":
                    var mob = (from p in db.TblProducts
                               join c in db.TblCategories
                               on p.Categoryid equals c.Categoryid
                               select new
                               {
                                   p.Productname,
                                   p.Productprice,
                                   p.Productdescription,
                                   p.Productbrand,
                                   p.Productimage,
                                   c.Categoryid,
                                   c.Categoryname
                               }).Where(c => c.Categoryname == "Mobile and Accessories").ToList();

                    // return Request.CreateResponse(HttpStatusCode.OK, mob);
                    return Ok ( mob );

                case "TV and Home Entertainment":
                    var ent = (from p in db.TblProducts
                               join c in db.TblCategories
                               on p.Categoryid equals c.Categoryid
                               select new
                               {
                                   p.Productname,
                                   p.Productprice,
                                   p.Productdescription,
                                   p.Productbrand,
                                   p.Productimage,
                                   c.Categoryid,
                                   c.Categoryname
                               }).Where(c => c.Categoryname == "TV and Home Entertainment").ToList();

                    // return Request.CreateResponse(HttpStatusCode.OK, ent);
                    return Ok( ent );
                case "Watches":
                    var watch = (from p in db.TblProducts
                                 join c in db.TblCategories
                                 on p.Categoryid equals c.Categoryid
                                 select new
                                 {
                                     p.Productname,
                                     p.Productprice,
                                     p.Productdescription,
                                     p.Productbrand,
                                     p.Productimage,
                                     c.Categoryid,
                                     c.Categoryname
                                 }).Where(c => c.Categoryname == "Watches").ToList();

                    // return Request.CreateResponse(HttpStatusCode.OK, watch);
                    return Ok( watch );
                case "Shoes":
                    var shoes = (from p in db.TblProducts
                                 join c in db.TblCategories
                                 on p.Categoryid equals c.Categoryid
                                 select new
                                 {
                                     p.Productname,
                                     p.Productprice,
                                     p.Productdescription,
                                     p.Productbrand,
                                     p.Productimage,
                                     c.Categoryid,
                                     c.Categoryname
                                 }).Where(c => c.Categoryname == "Shoes").ToList();

                    //return Request.CreateResponse(HttpStatusCode.OK, shoes);
                    return Ok(shoes);
                case "Clothing":
                    var clothes = (from p in db.TblProducts
                                   join c in db.TblCategories
                                   on p.Categoryid equals c.Categoryid
                                   select new
                                   {
                                       p.Productname,
                                       p.Productprice,
                                       p.Productdescription,
                                       p.Productbrand,
                                       p.Productimage,
                                       c.Categoryid,
                                       c.Categoryname
                                   }).Where(c => c.Categoryname == "Clothing").ToList();

                    // return Request.CreateResponse(HttpStatusCode.OK, clothes);
                    return Ok( clothes );

                default:
                    // return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No such category");
                    return BadRequest("No such Category");
            }
        }

        [Route("SearchProduct")]
        public IActionResult GetSearchProduct(string search)
        {
            var result = db.TblProducts.Where(x => x.Productname.StartsWith(search) || search == null).ToList();

           // return Request.CreateResponse(HttpStatusCode.OK, result);
            return Ok(result);
        }


    }
}
