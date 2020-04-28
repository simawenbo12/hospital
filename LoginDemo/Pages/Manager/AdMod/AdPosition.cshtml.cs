using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using LoginDemo.Help;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager
{
    public class AdPositionModel : MyPageModel<HmAdPosition, IHmAdPosition>
    {
        private readonly Log _log;
        public AdPositionModel(IHmAdPosition dao,Log log) : base(dao, $"/{DefaultData.Manager}/AdMod/AdPosition", $"/{DefaultData.Manager}/AdMod/AdPositionOperating")
        {
            _log = log;
        }
        public async override Task<JsonResult> OnGetDeleteAsync(int id)
        {           
            var ok = await base.OnGetDeleteAsync(id);
            if ((bool)ok.Value)
                _log.SetLog(HttpContext, $"删除广告位置 ID :{id}");
            return ok;
        }



    }
}