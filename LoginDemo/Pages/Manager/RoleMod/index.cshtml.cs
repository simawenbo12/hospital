using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using DataBase.TempModel;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.RoleMod
{
    public class indexModel : MyPageModel<HmAdmin,IHmAdmin>
    {
        public List<HmAdminTempMod> HmAdminTempMods { get; set; }
        private readonly Log _log;
        public indexModel(IHmAdmin dao,Log log) : base(dao, $"/{DefaultData.Manager}/RoleMod/index", $"/{DefaultData.Manager}/RoleMod/AdminOperating")
        {
            _log = log;
        }

        public override void OnGet(int CurrentPage = 1)
        {
            HmAdminTempMods = PageListHelp.GetPageList(_dao.FindAndRole(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }
        public override JsonResult OnGetDeleteEmpty(string url)
        {
          return  PageListHelp.GetSizeNum(_dao.FindAndRole(), PageData, url);
        }
        public override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            _log.SetLog(HttpContext, $"删除管理员列表 ID :{id}");
            return base.OnGetDeleteAsync(id);
        }

    }
}