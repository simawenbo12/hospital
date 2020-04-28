using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoginDemo.Pages.Home
{
    public class NavigationModel : PageModel
    {
        private readonly jingshenContext _db;
        public PageData PageData { get; set; } = new PageData();//翻页数据
        public List<HmArticle> HmArticles { get; set; }
        public List<HmArticle_Review> HmArticle_Reviews { get; set; }
        public string _thisUrl { get; set; }
        public int Aid { get; set; }
        public NavigationModel(jingshenContext db)
        {
            _db = db;
           
        }

        public void OnGet(int aid,int CurrentPage=1)
        {
            Aid = aid;
            SetDate(aid);             
            _thisUrl = $"{HttpContext.Request.Path.ToString().Split("?Currentpage=")[0]}";
            HmArticle_Reviews = PageListHelp.GetPageList(HmArticle_Reviews, PageData, CurrentPage, $"{_thisUrl}?Currentpage=",15);

        }
        /// <summary>
        /// 设置文章
        /// </summary>
        /// <param name="aid"></param>
        private void SetDate(int aid)
        {
            HmArticles = _db.GetArticles(aid).OrderByDescending(x=>x.Type).ThenByDescending(x=>x.Addtime).ToList();
            HmArticle_Reviews = new List<HmArticle_Review>();
            foreach (var x in HmArticles)
            {

                var temp = new HmArticle_Review { HmArticle = x };
                temp.hmReview = _db.HmReview.Find(x.Id);
                if (temp.hmReview.Review != 2||temp.HmArticle.IsShow==2) continue;
                HmArticle_Reviews.Add(temp);
            }

        }
        

    }
    public class HmArticle_Review
    {
        public HmArticle HmArticle { get; set; }
        public HmReview hmReview { get; set; }
    }
}