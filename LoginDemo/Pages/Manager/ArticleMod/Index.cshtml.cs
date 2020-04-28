using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using DataBase.TempModel;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.ArticleMod
{
    public class IndexModel : EasyPageModel<HmArticleTempMod>
    {
     
        public IndexModel(jingshenContext db, Log log) : base(db, log, $"/{DefaultData.Manager}/ArticleMod/index", $"/{DefaultData.Manager}/ArticleMod/ArticleOperating")
        {
          
        }
        public override void OnGet(int CurrentPage = 1)
        {
            
            GetTs = PageListHelp.GetPageList(GetData(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }
        public IActionResult OnGetByID(int cateid,int CurrentPage=1)
        {
            GetTs = PageListHelp.GetPageList(GetData().FindAll(x=>x.HmArticle.CateId== cateid), PageData, CurrentPage, $"{_thisUrl}/?handler=ByID&cateid={cateid}&Currentpage=");
            return Page();
        }
        public IActionResult OnGetByTitle(string title, int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(GetTitleData(title), PageData, CurrentPage, $"{_thisUrl}/?handler=ByTitle&title={title}&Currentpage=");
            return Page();
        }

        public async override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            _db.HmArticle.Remove(_db.HmArticle.Find(id));
            _db.HmReview.Remove(_db.HmReview.Find(id));
            int a=await _db.SaveChangesAsync();
            if (a > 0)
            {
                _log.SetLog(HttpContext, $"删除文章 ID :{id}");
            return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
            
        }
        public override JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(GetData(), PageData, url);
        }
        private List<HmArticleTempMod> GetData()
        {
            var list = new List<HmArticleTempMod>();
            var article = _db.HmArticle.OrderBy(x=>x.CateId).ThenByDescending(x=>x.Type).ThenByDescending(x=>x.Addtime).ToList();
            var category = _db.HmArticleCategory.ToList();
            foreach (var x in article)
            {
                var temp = new HmArticleTempMod() { DateTime = HelpClass.ConvertToTime((uint)x.Addtime), HmArticle = x };
                foreach (var y in category)
                {
                    if (x.CateId == y.Id)
                    {
                        temp.HmArticleCategory = y;
                        temp.HmReview = _db.HmReview.Find(x.Id);
                        list.Add(temp);
                        break;
                    }
                }
            }

            return list;
        }
        private List<HmArticleTempMod> GetTitleData(string title)
        {
            var temp = GetData().Where(x =>
            {
                var atitle = x.HmArticle.Title;
                if (atitle.Contains(title)) return true;
                else return false;
            }).ToList();

            return temp;
        }

    }

}