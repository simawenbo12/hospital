using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UEditor.Core;

namespace LoginDemo.Pages.Manager.ArticleMod
{
    public class ArticleOperatingModel : PageModel
    {
        private readonly UEditor.Core.UEditorService _se;
        private readonly jingshenContext _db;
        private readonly Log _log;
        private readonly LocalStr _localstr;
        [BindProperty]
        public string editorValue { get; set; }//富文本编辑器内容
        [BindProperty]
        public HmArticle HmArticle { get; set; }
        [BindProperty]
        public string  SelectP { get; set; }
        public List<SelectListItem> SelectListItems { get; set; }
        [BindProperty]
        public IFormFile UploadFile { get; set; }
        [BindProperty]
        public bool ishow { get; set; }

        public ArticleOperatingModel(UEditorService se, jingshenContext db, Log log, LocalStr localStr)
        {
            _se = se;
            _db = db;
            _log = log;
            _localstr = localStr;
        }

        public void OnGet(int? id)
        {
            SetSelectListItem();
            if (id != null)
            {
                HmArticle = _db.HmArticle.Find(id);
                SelectP =HmArticle.CateId.ToString();
                ishow = HmArticle.IsShow == 1 ? true : false;
            }
            else
            {
            ishow = true;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            await Upload.UploadPictureAsync(_localstr, UploadFile, HmArticle);
            HmArticle.CateId = int.Parse(SelectP);
            bool isCreate = HmArticle.Id == 0 ? true:false;
            HmArticle.Content = editorValue;
            HmArticle.IsShow = ishow ? 1 : 2;

            HmArticle.Addtime = (int)HelpClass.ConvertToTime(DateTime.Now);           
            if (isCreate)
            {
                _db.HmArticle.Add(HmArticle);
                 HttpContext.Request.Cookies.TryGetValue("adminid", out string adminnum);
                await _db.SaveChangesAsync();
                _db.HmReview.Add(new HmReview {Adminid=int.Parse(adminnum),Id=HmArticle.Id });
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext,$"增加了文章 ID :{HmArticle.Id}");

            }
            else
            {
                _db.HmArticle.Update(HmArticle);
                var temp = _db.HmReview.Find(HmArticle.Id);
                temp.Review = 1;
                _db.HmReview.Update(temp);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"编辑了文章 ID :{HmArticle.Id}");
            }
            
            ViewData["data"] = editorValue;
            return RedirectToPage("/Manager/ArticleMod/index");

            

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