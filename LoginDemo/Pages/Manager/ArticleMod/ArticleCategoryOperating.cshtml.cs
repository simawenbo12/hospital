using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginDemo.Pages.Manager.ArticleMod
{
    public class ArticleCategoryOperatingModel : PageModel
    {
        private readonly jingshenContext _db;
        public List<SelectListItem> SelectListItems { get; set; }
        [BindProperty]
        public HmArticleCategory GetT { get; set; }
        [BindProperty]
        public string  SelectP { get; set; }
        private readonly Log _log;
        public string _OriginUrl { get; set; } = $"/{DefaultData.Manager}/ArticleMod/ArticleCategory";
        public ArticleCategoryOperatingModel(jingshenContext db,Log log)
        {
            _log = log;
            _db = db;
        }

        public void OnGet(int? id)
        {
            SetSelectListItem();
            if (id != null)
            {
                GetT = _db.HmArticleCategory.Find(id);
                SelectP = GetT.ParentId.ToString();                
            }
        }
        public async Task<IActionResult> OnPost()
        {
            GetT.ParentId = uint.Parse(SelectP);
            if (GetT.Id == 0)//添加
            {                
                _db.HmArticleCategory.Add(GetT);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"添加了文章分类 ID :{GetT.Id}");
            }
            else//编辑
            {
                _db.HmArticleCategory.Update(GetT);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"编辑了文章分类 ID :{GetT.Id}");
            }
            return RedirectToPage(_OriginUrl);
        }

        private void SetSelectListItem()
        {
            SelectListItems = new List<SelectListItem>();
            var temp = HelpClass.GetHmArticleCategoryOrder(_db.HmArticleCategory.ToList());
            foreach (var x in temp)
            {
                SelectListItems.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            }
        }
    }
}