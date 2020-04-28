using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager
{
    public class AdPositionOperatingModel : MyPageModelForIU<HmAdPosition, IHmAdPosition>
    {
        private readonly Log _log;

        public AdPositionOperatingModel(IHmAdPosition dao,LocalStr localstr,Log log):base(dao, $"/{DefaultData.Manager}/AdMod/AdPosition", localstr)
        {
            _log = log;
        }
        public async override Task<IActionResult> OnPost()
        {
            bool isCreate = GetT.Id == 0 ? true : false;

            await base.OnPost();

            if (isCreate)
            {
                _log.SetLog(HttpContext, $"添加广告位置 ID: {GetT.Id}");
            }
            else
            {
                _log.SetLog(HttpContext, $"编辑广告位置 ID: {GetT.Id}");

            }

            return RedirectToPage(_OriginUrl);
        }


    }
}