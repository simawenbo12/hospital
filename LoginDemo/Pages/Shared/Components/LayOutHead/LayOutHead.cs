using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Pages.Shared.Components.LayOutHead
{
    public class LayOutHead : ViewComponent
    {
        private readonly jingshenContext _db;
        public LayOutHead(jingshenContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tss = HttpContext.Request.Path;
            var tss2 = tss.ToString().Split('/');
            int nv;
            try
            {
            nv=string.IsNullOrEmpty(tss2[3]) ? -1 : int.Parse(tss2[3]);
            }
            catch (Exception)
            {
                nv = -1;
            }
            var te = new LayOutHomeNavigation
            {
                First = _db.HmArticleCategory.Where(x => x.ParentId == 0 && x.Id != 0).ToList(),
                Secend = _db.HmArticleCategory.Where(x => x.ParentId != 0).ToList(),
            };
            if (nv != -1)
            {
                var temp = _db.HmArticleCategory.Find(nv);
                if(temp!=null)
                te.Navigation = temp.ParentId==0?temp.Id:(int)temp.ParentId;
            }
            return View(te);
        }
      
    }
    public class LayOutHomeNavigation
    {
        public List<HmArticleCategory> First { get; set; }
        public List<HmArticleCategory> Secend { get; set; }
        public int Navigation { get; set; }
    }
}
