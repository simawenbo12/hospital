using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.GongGaoMod
{
    public class indexModel : EasyPageModel<HmGonggao>
    {
        public indexModel(jingshenContext db, Log log) : base(db, log, $"/{DefaultData.Manager}/GongGaoMod/index", $"/{DefaultData.Manager}/GongGaoMod/GongGaoOperating")
        {

        }

        public override void OnGet(int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(_db.HmGonggao.ToList(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }
        public IActionResult OnGetByTitle(string title, int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(GetTitleData(title), PageData, CurrentPage, $"{_thisUrl}/?handler=ByTitle&title={title}&Currentpage=");
            return Page();
        }
        private List<HmGonggao> GetTitleData(string title)
        {
            var temp = _db.HmGonggao.ToList().Where(x =>
            {
                if (x.Title.Contains(title)) return true;
                else return false;
            }).ToList();
            return temp;
        }

        public async override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            _db.HmGonggao.Remove(_db.HmGonggao.Find(id));
            int a = await _db.SaveChangesAsync();
            if (a > 0)
            {
                _log.SetLog(HttpContext, $"删除公告 ID :{id}");
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        public override JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(_db.HmGonggao.ToList(), PageData, url);
        }
    }
}