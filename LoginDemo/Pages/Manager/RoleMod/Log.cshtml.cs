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
using Microsoft.EntityFrameworkCore;

namespace LoginDemo.Pages.Manager.RoleMod
{
    public class LogModel : PageModel
    {
        private readonly jingshenContext _dao;
        public List<HmAdminLogTempMod> GetTs { get; set; }
        public PageData PageData { get; set; } = new PageData();//翻页数据
        public string _thisUrl { get; set; } = $"/{DefaultData.Manager}/RoleMod/Log";
        public LogModel(jingshenContext db)
        {
            _dao = db;
        }

        public void OnGet(int CurrentPage = 1)
        {
            GetTs = PageListHelp.GetPageList(GetData(), PageData, CurrentPage, $"{_thisUrl}?Currentpage=");
        }

        private List<HmAdminLogTempMod> GetData()
        {
            var name = _dao.HmAdmin.AsNoTracking().ToList();
            var log = _dao.HmAdminLog.AsNoTracking().ToList().OrderByDescending(x=>x.LogTime);
            var list = new List<HmAdminLogTempMod>();
            foreach (var x in log)
            {
                var temp = new HmAdminLogTempMod() { HmAdminLog = x,DateTime=HelpClass.ConvertToTime(x.LogTime) };
                foreach (var y in name)
                {
                    if (x.AdminId == y.Id)
                    {
                        temp.HmAdmin = y;
                        list.Add(temp);
                        break;
                    }

                }
            }
            return list;

        }

    }
   

}