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
    public class ArticleReviewModel : PageModel
    {
        private readonly jingshenContext _db;
        private readonly Log _log;
        public List<HmArticleTempMod> HmArticleTempMods { get; set; }
        public PageData PageData { get; set; } = new PageData();//翻页数据
        public string _thisUrl { get; set; } = "/Manager/ArticleMod/ArticleReview";

        public ArticleReviewModel(jingshenContext db, Log log)
        {
            _db = db;
            _log = log;
        }

        public  void OnGet(int CurrentPage = 1)
        {
            HmArticleTempMods = PageListHelp.GetPageList(GetData(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }
        public async Task<IActionResult> OnGetSetReview(int id,int review)
        {
            var data=_db.HmReview.Find(id);
            data.Review = review;
            data.Reviewtime =(int) HelpClass.ConvertToTime(DateTime.Now);
            _db.HmReview.Update(data);
            await _db.SaveChangesAsync();
            _log.SetLog(HttpContext, $"编辑审核状态 ID : {id}");
            return RedirectToPage("/Manager/ArticleMod/ArticleReview");
        }


        public IActionResult OnGetByTitle(string title, int CurrentPage = 1)
        {
            HmArticleTempMods = PageListHelp.GetPageList(GetTitleData(title), PageData, CurrentPage, $"{_thisUrl}/?handler=ByTitle&title={title}&Currentpage=");
            return Page();
        }
        private List<HmArticleTempMod> GetData()
        {
            var list = new List<HmArticleTempMod>();
            var article = _db.HmArticle.ToList().OrderByDescending(x=>x.Addtime);
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
                        temp.HmAdmin = _db.HmAdmin.Find(temp.HmReview.Adminid);
                        list.Add(temp);
                        break;
                    }
                }
            }
            list.OrderBy(x => x.HmReview.Review).ThenByDescending(x => x.HmArticle.Addtime);
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