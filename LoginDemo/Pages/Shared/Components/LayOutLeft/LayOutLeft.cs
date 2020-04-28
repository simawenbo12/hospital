using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Pages.Shared.Components.LayOutLeft
{
    public class LayOutLeft : ViewComponent
    {
        private readonly jingshenContext _db;
        public List<HmArticleCategory> HmArticleCategories { get; set; }
        public HmArticleCategory HmArticleCategory { get; set; }
        public int Aid { get; set; }
        public bool isZero { get; set; } = false;
        public LayOutLeft(jingshenContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var aid = HttpContext.Request.Path.ToString().Split('/')[3];
            SetDate(int.Parse(aid));

            return View(new LayOutLeftNavigation {HmArticleCategories=HmArticleCategories,HmArticleCategory=HmArticleCategory,isZero=isZero,Aid=Aid });
        }
        private void SetDate(int aid)
        {
            var temp = _db.HmArticleCategory.Find(aid);
            if (temp.ParentId == 0)
            {
                Aid = aid;
                isZero = true;
                HmArticleCategory = temp;
                HmArticleCategories = _db.HmArticleCategory.AsNoTracking().Where(x => x.ParentId == aid).ToList();
            }
            else
            {
                HmArticleCategory = _db.HmArticleCategory.Find((int)temp.ParentId);
                Aid = aid;
                aid = HmArticleCategory.Id;
                HmArticleCategories = _db.HmArticleCategory.AsNoTracking().Where(x => x.ParentId == aid).ToList();               
            }

        }
    }
    public class LayOutLeftNavigation
    {
        public List<HmArticleCategory> HmArticleCategories { get; set; }
        public HmArticleCategory HmArticleCategory { get; set; }
        public int Aid { get; set; }
        public bool isZero { get; set; } = false;
    }
}
