using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.SystemManager
{
    public class SystemFootModel : PageModel
    {
        private readonly jingshenContext _db;
        private readonly Log _log;
        private readonly LocalStr _localstr;
        [BindProperty]
        public IFormFile UploadFile { get; set; }
        [BindProperty]
        public HmSystemfoot GetT { get; set; }

        public SystemFootModel(jingshenContext db, Log log, LocalStr localstr)
        {
            _db = db;
            _log = log;
            _localstr = localstr;
        }

        public void OnGet()
        {
            GetT = _db.HmSystemfoot.FirstOrDefault();
        }

        public async Task<IActionResult> OnPost()
        {
            await Upload.UploadPictureAsync(_localstr, UploadFile, GetT);
            _db.HmSystemfoot.Update(GetT);
            await _db.SaveChangesAsync();
            _log.SetLog(HttpContext, $"编辑网站底部设置 id :{GetT.Id}");
            return RedirectToPage($"/{DefaultData.Manager}/SystemManager/SystemFoot");
        }


    }
}