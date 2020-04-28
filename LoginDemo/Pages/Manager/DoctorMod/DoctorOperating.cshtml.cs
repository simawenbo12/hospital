using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginDemo.Pages.Manager.DoctorMod
{
    public class DoctorOperatingModel : PageModel
    {
        private readonly jingshenContext _db;
        private readonly Log _log;
        private readonly LocalStr _localstr;
        [BindProperty]
        public HmDoctor HmDoctor { get; set; }
        [BindProperty]
        public IFormFile UploadFile { get; set; }
        public List<SelectListItem> SelectListItems { get; set; } =new List<SelectListItem>
        {
            new SelectListItem{Value="1",Text="初中"},
            new SelectListItem{Value="2",Text="高中"},
            new SelectListItem{Value="3",Text="本科"},
            new SelectListItem{Value="4",Text="硕士"},
            new SelectListItem{Value="5",Text="博士"},
            new SelectListItem{Value="6",Text="博士后"},
        };
        [BindProperty]
        public string SelecrP { get; set; }

        public DoctorOperatingModel(jingshenContext db, Log log, LocalStr localstr)
        {
            _db = db;
            _log = log;
            _localstr = localstr;
        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                HmDoctor = _db.HmDoctor.Find(id);
                SelecrP = HmDoctor.Edu.ToString(); ;
            }

        }
        public async Task<IActionResult> OnPost()
        {
             await Upload.UploadPictureAsync(_localstr, UploadFile, HmDoctor);
            bool isCreate = HmDoctor.Id == 0 ? true : false;
            HmDoctor.Edu = int.Parse(SelecrP);            
            if (isCreate)
            {
                _db.HmDoctor.Add(HmDoctor);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"增加了医生 ID :{HmDoctor.Id}");
            }
            else
            {
                _db.HmDoctor.Update(HmDoctor);
                await _db.SaveChangesAsync();
                _log.SetLog(HttpContext, $"编辑了医生 ID :{HmDoctor.Id}");
            }

            return RedirectToPage("/Manager/DoctorMod/index");
        }

    }
}