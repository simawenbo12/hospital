using DataBase.Interface;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    /// <summary>
    /// 默认显示继承的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="IT"></typeparam>
    public abstract class MyPageModel<T, IT> : PageModel where T : class where IT : IBasic<T>
    {
        protected readonly IT _dao;
        [BindProperty]
        public T GetT { get; set; }
        public List<T> GetTs { get; set; }
        public PageData PageData { get; set; } = new PageData();//翻页数据
        public string _thisUrl { get; set; }
        public string _OperatingUrl { get; set; }
        protected MyPageModel(IT dao, string thisurl, string operatingurl)
        {
            _dao = dao;
            _thisUrl = thisurl;
            _OperatingUrl = operatingurl;
        }

        public virtual void OnGet(int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(_dao.FindAll(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }      
        /// <summary>
        /// 删除后若当前页面无数据,返回上一页
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual JsonResult OnGetDeleteEmpty(string url)
        {
            return PageListHelp.GetSizeNum(_dao.FindAll(), PageData, url);
        }
        public virtual async Task<JsonResult> OnGetDeleteAsync(int id)
        {
            try
            {
                bool ok;
                int num = await _dao.DeleteTrueAsync(id);
                ok = num > 0 ? true : false;
                return new JsonResult(ok);
            }
            catch (Exception)
            {
                return new JsonResult(false);

            }
        }
    }

}
