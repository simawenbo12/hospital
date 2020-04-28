using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Pages.Shared.Components.LayOutFoot
{
    public class LayOutFoot: ViewComponent
    {
        private readonly jingshenContext _db;

        public LayOutFoot(jingshenContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var te = new LayOutFootNavigation
            {
                HmSystemfoot=_db.HmSystemfoot.AsNoTracking().FirstOrDefault(),
                HmSystemfriendlinks=_db.HmSystemfriendlink.AsNoTracking().ToList()
            };

            return View(te);
        }

    }
    public class LayOutFootNavigation
    {
        public HmSystemfoot HmSystemfoot { get; set; }
        public List<HmSystemfriendlink> HmSystemfriendlinks { get; set; }
    }
}
