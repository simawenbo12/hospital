using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using DataBase.TempModel;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager
{
    public class AdModel : MyPageModel<HmAd,IHmAd>
    {
        public List<HmAdTempMod> HmAdTempModels { get; set; }
        private readonly Log _log;        
        public AdModel(IHmAd dao, Log http) : base(dao, $"/{DefaultData.Manager}/AdMod/index", $"/{DefaultData.Manager}/AdMod/AdOperating")
        {
            _log = http;
        }
        public override void OnGet(int CurrentPage = 1)
        {
            HmAdTempModels = PageListHelp.GetPageList(_dao.GetHmAdTempModels(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }
        public override JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(_dao.GetHmAdTempModels(), PageData, url);
        }
        public override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            _log.SetLog(HttpContext, $"删除广告列表 ID :{id}");
            return base.OnGetDeleteAsync(id);
        }

    }
}