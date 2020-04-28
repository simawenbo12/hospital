using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.SystemManager
{
    public class FriendLinksOperatingModel : PageModel
    {

        private readonly jingshenContext _db;
        private readonly Help.HelpPageClass.Log _log;
        [BindProperty]
        public HmSystemfriendlink GetT { get; set; }
        public FriendLinksOperatingModel(jingshenContext db, Log log)
        {
            _db = db;
            _log = log;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                GetT = _db.HmSystemfriendlink.Find(id);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            bool isCreate = GetT.Id == 0 ? true : false;
            if (isCreate)
            {
                _db.HmSystemfriendlink.Add(GetT);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"增加了友情链接 ID :{GetT.Id}");
            }
            else
            {
                _db.HmSystemfriendlink.Update(GetT);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"编辑了友情链接 ID :{GetT.Id}");
            }
            return RedirectToPage("/Manager/SystemManager/FriendLinks");
        }

    }
}