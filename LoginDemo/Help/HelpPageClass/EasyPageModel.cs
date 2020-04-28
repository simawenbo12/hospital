using DataBase.Models;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    public abstract class EasyPageModel<T>:PageModel where T:class
    {
        public PageData PageData { get; set; } = new PageData();//翻页数据
        protected readonly jingshenContext _db;
        protected readonly Log _log;
        [BindProperty]
        public T GetT { get; set; }
        public List<T> GetTs { get; set; }
        public string _thisUrl { get; set; } 
        public string _OperatingUrl { get; set; }
        public abstract void OnGet(int CurrentPage = 1);
        public abstract JsonResult OnGetDeleteEmpty(string url);      
        public abstract Task<JsonResult> OnGetDeleteAsync(int id);

        protected EasyPageModel(jingshenContext db, Log log, string thisUrl, string OperatingUrl)
        {
            _db = db;
            _log = log;
            _thisUrl = thisUrl;
            _OperatingUrl = OperatingUrl;
        }
    }
}
