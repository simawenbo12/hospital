using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.GongGaoMod
{
    public class GongGaoReadModel : PageModel
    {
        private readonly jingshenContext _db;
        public HmGonggao HmGonggao { get; set; }

        public GongGaoReadModel(jingshenContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            HmGonggao = _db.HmGonggao.Find(id);
        }
    }
}