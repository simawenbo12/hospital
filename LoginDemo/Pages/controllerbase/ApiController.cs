using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginDemo.Pages.controllerbase
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly jingshenContext _db;

        public ApiController(jingshenContext db)
        {
            _db = db;
        }
        [HttpGet("GetDoctor")]
        public JsonResult GetDoctor()
        {
            return new JsonResult(_db.HmDoctor.ToList());
        }
    }
}