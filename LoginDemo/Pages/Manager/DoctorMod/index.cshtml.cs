using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.DoctorMod
{
    public class indexModel : EasyPageModel<HmDoctor>
    {
        public indexModel(jingshenContext db, Log log) : base(db, log, $"/{DefaultData.Manager}/DoctorMod/index", $"/{DefaultData.Manager}/DoctorMod/DoctorOperating")
        {

        }

        public override void OnGet(int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(_db.HmDoctor.ToList(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }

        public async override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            _db.HmDoctor.Remove(_db.HmDoctor.Find(id));
            int a = await _db.SaveChangesAsync();
            if (a > 0)
            {
                _log.SetLog(HttpContext, $"删除医生 ID :{id}");
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }

        }

        public override JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(_db.HmDoctor.ToList(), PageData, url);
        }
    }
}