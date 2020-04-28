using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.ArticleMod
{
    public class ArticleCategoryModel : EasyPageModel<HmArticleCategory>
    {
        public List<HmArticleCategory> List { get; set; }
        public ArticleCategoryModel(jingshenContext db,Log log):base(db,log, $"/{DefaultData.Manager}/ArticleMod/ArticleCategory", $"/{DefaultData.Manager}/ArticleMod/ArticleCategoryOperating")
        {           
        }

        public override void OnGet(int CurrentPage = 1)
        {
            List = PageListHelp.GetPageList(HelpClass.GetHmArticleCategoryOrder(_db.HmArticleCategory.ToList()), PageData, CurrentPage, $"{_thisUrl}?Currentpage="); 
        }
        public override JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(HelpClass.GetHmArticleCategoryOrder(_db.HmArticleCategory.ToList()), PageData, url);
        }
        public override async Task<JsonResult> OnGetDeleteAsync(int id)
        {
            bool d1 = _db.HmArticleCategory.Where(x => x.ParentId == id).FirstOrDefault() == null ? true : false ;
            bool d2=_db.HmArticle.Where(x=>x.CateId==id).FirstOrDefault()==null ? true:false;
            if (d1 && d2)
            {
                _db.HmArticleCategory.Remove(_db.HmArticleCategory.Find(id));
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"删除了文章分类 ID :{id}");
                return new JsonResult(true);
            }
            return new JsonResult(false);
        }
    }
}