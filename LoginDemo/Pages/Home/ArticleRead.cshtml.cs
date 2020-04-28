using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Home
{
    public class ArticleReadModel : PageModel
    {
        private readonly jingshenContext _db;
        public HmArticle HmArticle { get; set; }
        public ArticleReadModel(jingshenContext db)
        {
            _db = db;
        }

        public void OnGet(int articleid)
        {
            HmArticle = _db.HmArticle.Find(articleid);
        }
    }
}