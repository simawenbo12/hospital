using DataBase.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    /// <summary>
    /// 操作继承的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="IT"></typeparam>
    public class MyPageModelForIU<T,IT>:PageModel where T : class where IT : IBasic<T>
    {
        protected readonly IT _dao;
        [BindProperty]
        public T GetT { get; set; }
        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public string _OriginUrl { get; set; }
        public LocalStr LocalStr { get; set; }
        public MyPageModelForIU(IT dao, string OriginUrl,LocalStr localStr)
        {
            _dao = dao;
            _OriginUrl = OriginUrl;
            LocalStr = localStr;
        }

        public virtual IActionResult OnGet(int? id)
        {
            if (id != null)
            {
                GetT = _dao.FindByID((int)id);
            }
            return Page();
        }

        public virtual async Task<IActionResult> OnPost()
        {

            var dara = await _dao.CreateOrUpdate(GetT);
            return RedirectToPage(_OriginUrl);
        }




    }
}
