using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UEditor.Core;

namespace LoginDemo.Pages.Manager.GongGaoMod
{
    public class GongGaoOperatingModel : PageModel
    {
        private readonly UEditor.Core.UEditorService _se;
        private readonly jingshenContext _db;
        private readonly Help.HelpPageClass.Log _log;
        [BindProperty]
        public string editorValue { get; set; }//富文本编辑器内容
        [BindProperty]
        public HmGonggao HmGonggao { get; set; }
        [BindProperty]
        public bool ishow { get; set; }
        [BindProperty]
        public DateTime DateTime { get; set; }
        public GongGaoOperatingModel(UEditorService se, jingshenContext db, Log log)
        {
            _se = se;
            _db = db;
            _log = log;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                HmGonggao = _db.HmGonggao.Find(id);
                ishow = HmGonggao.IsShow == 1 ? true : false;
                DateTime = HelpClass.ConvertToTime((uint)HmGonggao.Addtime);
            }


        }
        public async Task<IActionResult> OnPost()
        {
            bool isCreate = HmGonggao.Id == 0 ? true:false;
            HmGonggao.Content = editorValue;
            HmGonggao.Addtime =(int)HelpClass.ConvertToTime(DateTime);
            HmGonggao.IsShow = ishow ? 1 : 2;
            if (isCreate)
            {
                _db.HmGonggao.Add(HmGonggao);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"增加了公告 ID :{HmGonggao.Id}");
            }
            else
            {
                _db.HmGonggao.Update(HmGonggao);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"编辑了公告 ID :{HmGonggao.Id}");
            }
            return RedirectToPage("/Manager/GongGaoMod/index");
        }


    }
}