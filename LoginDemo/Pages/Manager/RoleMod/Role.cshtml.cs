using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager.RoleMod
{
    public class RoleModel : MyPageModel<HmRbacRole,IHmRole>
    {
        private readonly Log _log;
        public RoleModel(IHmRole db,Log log):base(db, $"/{DefaultData.Manager}/RoleMod/Role", $"/{DefaultData.Manager}/RoleMod/RoleOperating")
        {
            _log = log;
        }

        public async override Task<JsonResult> OnGetDeleteAsync(int id)
        {
            var ok= await base.OnGetDeleteAsync(id);
            if ((bool)ok.Value)
                _log.SetLog(HttpContext, $"删除角色 ID :{id}");
            return ok;
        }
    }
}