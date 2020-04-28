using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginDemo.Pages.Manager
{
    public class AdOperatingModel : MyPageModelForIU<HmAd, IHmAd>
    {
        private readonly IHmAdPosition _hmadpositiondao;
        public List<SelectListItem> SLitem { get; set; }
        private readonly Log _log;
        [BindProperty]
        public string  SelectP { get; set; }
        public AdOperatingModel(IHmAd dao, IHmAdPosition hmAdPosition, LocalStr localStr,Log log ) :base(dao, $"/{DefaultData.Manager}/AdMod/index", localStr)
        {
            _log = log;
            _hmadpositiondao = hmAdPosition;
        }
        public override IActionResult OnGet(int? id)
        {
            if (id != null)
            {
                GetT = _dao.FindByID((int)id);
                SelectP = GetT.PositionId.ToString();
            }
            SLitem = _hmadpositiondao.GetSelectListItems();
            return Page();
        }
        public  async override Task<IActionResult> OnPost()
        {
            await  Upload.UploadPictureAsync(LocalStr, UploadFile, GetT);
             GetT.PositionId = int.Parse(SelectP);
            bool isCreate = GetT.Id == 0 ? true : false;
             await base.OnPost();
            if (isCreate)
            {
                _log.SetLog(HttpContext,$"添加广告列表 ID :{GetT.Id}");
            }
            else
            {
                _log.SetLog(HttpContext, $"编辑广告列表 ID :{GetT.Id}");

            }

            return RedirectToPage(_OriginUrl);
        }
        
    }
}