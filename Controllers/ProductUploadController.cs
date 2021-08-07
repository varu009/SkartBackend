using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skartwebapi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Skartwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUploadController : ControllerBase
    {
        private readonly onlineshoppingContext db;
        public ProductUploadController(onlineshoppingContext context)
        {
            db = context;
        }
        
    }
}
