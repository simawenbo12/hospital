using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.SystemManager
{
    public class FriendLinksModel : EasyPageModel<HmSystemfriendlink>
    {
        public FriendLinksModel(jingshenContext db, Log log) : base(db, log, $"/{DefaultData.Manager}/SystemManager/FriendLinks", $"/{DefaultData.Manager}/SystemManager/FriendLinksOperating")
        {

        }

        public override void OnGet(int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(_db.HmSystemfriendlink.ToList(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }
        public IActionResult OnGetByTitle(string title, int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(GetTitleData(title), PageData, CurrentPage, $"{_thisUrl}/?handler=ByTitle&title={title}&Currentpage=");
            return Page();
        }

        private List<HmSystemfriendlink> GetTitleData(string title)
        {
            var temp = _db.HmSystemfriendlink.ToList().Where(x =>
            {
                if (x.Linkname.Contains(title)) return true;
                else return false;
            }).ToList();
            return temp;

        }

        public async override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            _db.HmSystemfriendlink.Remove(_db.HmSystemfriendlink.Find(id));
            int a = await _db.SaveChangesAsync();
            if (a > 0)
            {
                _log.SetLog(HttpContext, $"删除友情链接 ID :{id}");
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }

        public override JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(_db.HmSystemfriendlink.ToList(), PageData, url);
        }
    }
}