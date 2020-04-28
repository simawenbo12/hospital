using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Home
{
    public class DoctorModel : PageModel
    {
        private readonly jingshenContext _db;
        public  HmDoctor GetT { get; set; }
        public DoctorModel(jingshenContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            GetT = _db.HmDoctor.Find(id);
        }
    }
}