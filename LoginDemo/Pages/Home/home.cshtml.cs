using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Home
{
    public class homeModel : PageModel
    {
        private readonly jingshenContext _db;
        private readonly UserLog _userlog;
        /// <summary>
        /// 首页轮播图
        /// </summary>
        public List<HmAd> hmAdShouYe { get; set; }
        /// <summary>
        /// 首页新闻中心文章轮播图
        /// </summary>
        public List<HmArticle> NewArticlesLunbo { get; set; }
        /// <summary>
        /// 首页新闻中心文章
        /// </summary>
        public List<HmArticle> NewArticles{ get; set; }
        /// <summary>
        /// 首页医生列表,首页出诊信息
        /// </summary>
        public List<HmDoctor> HmDoctors { get; set; }
        /// <summary>
        /// 首页名医名项
        /// </summary>
        public List<HmArticle> GoodDoctorArticles { get; set; }
        /// <summary>
        /// 首页医护天地
        /// </summary>
        public List<HmArticle> MedicalArticles { get; set; }
        /// <summary>
        /// 首页招标公告
        /// </summary>
        public List<HmArticle> TenderArticles { get; set; }
        /// <summary>
        /// 首页招聘公告
        /// </summary>
        public List<HmArticle> RecruitmentArticles { get; set; }
        /// <summary>
        /// 首页通知公告
        /// </summary>
        public List<HmArticle> NoticeArticles { get; set; }


        public homeModel(jingshenContext db, UserLog userlog)
        {
            _db = db;
            _userlog = userlog;
        }
        public  void OnGet()
        {
             _userlog.SetLog(HttpContext);
            hmAdShouYe = _db.HmAd.Take(5).ToList();
            NewArticlesLunbo = _db.GetArticles(21).OrderByDescending(x=>x.Type).ThenByDescending(x=>x.Addtime).Take(5).ToList();
            NewArticles = _db.GetArticles(21).OrderByDescending(x => x.Addtime).Take(10).ToList();
            HmDoctors = _db.HmDoctor.ToList();
            GoodDoctorArticles = _db.GetArticles(37).OrderByDescending(x=>x.Addtime).Take(11).ToList();
            MedicalArticles = _db.GetArticles(33).OrderByDescending(x=>x.Type).ThenByDescending(x => x.Addtime).Take(2).ToList();
            SetGongGao();

        }

        private void SetGongGao()
        {
            int a = _db.GetArticles(39).Count;
            TenderArticles = a >= 10 ? _db.GetArticles(39).Take(10).ToList() : _db.GetArticles(39).ToList();
            a = _db.GetArticles(23).Count;
            RecruitmentArticles= a >= 10 ? _db.GetArticles(23).Take(10).ToList() : _db.GetArticles(23).ToList();
            a = _db.GetArticles(22).Count;
            NoticeArticles = a >= 10 ? _db.GetArticles(22).Take(10).ToList() : _db.GetArticles(22).ToList();

        }
    }
}