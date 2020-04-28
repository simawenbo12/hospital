using DataBase.Models;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Pages.Shared.Components.LayOut
{
    public class LayOut : ViewComponent
    {
        public string _LayOutHtml { get; set; }
        private readonly jingshenContext _db;
        public LayOut(jingshenContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            AuthenticateResult auth = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (auth.Succeeded)
            {
                var name = auth.Principal.Identity.Name;
                string roleid = "";
                foreach (var x in auth.Principal.Identities.FirstOrDefault().Claims)
                {
                    if (!x.Value.Equals(name)) { roleid = x.Value; break; }
                }
             
             _LayOutHtml = SetStr(_db, roleid);
            }        
            return View("",_LayOutHtml);
        }
        /// <summary>
        /// 读取数据库,动态拼接导航栏
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="roleid">存储在cookies的roleID</param>
        /// <returns></returns>
         string SetStr(jingshenContext db, string roleid)
        {
            var Power = db.HmRbacPower.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            var First = Power.Where(x => x.ParentId == 0).OrderBy(x => x.Sort).ToList();//获取全部一级目录
            var Secend = Power.Where(x => x.ParentId != 0).OrderBy(x => x.Sort).ToList();//获取全部二级目录
            if (roleid.Equals("1"))//特殊管理员权限
            {
                foreach (var f in First)
                {
                    stringBuilder.Append($"<li><a href=\"#\"><i class=\"{f.Icon}\"></i><span class=\"nav-label\">{f.Name}</span><span class=\"fa arrow\"></span> </a> <ul class=\"nav nav-second-level\"><li>");
                    foreach (var s in Secend)
                    {
                        if (f.Id == s.ParentId)
                            stringBuilder.Append($"<a class=\"J_menuItem\" href=\"/{DefaultData.Manager}/{s.Controller + "/" + s.Action}\">{s.Name}</a>");
                    }
                    stringBuilder.Append("</li> </ul></li> ");
                }
            }
            else
            {
                var Role = db.HmRbacPowerRole.Where(x => x.RoleId == int.Parse(roleid)).OrderBy(x => x.PowerId).ToList();//升序排列powerrole
                var tempSecend = Secend.Join(Role, x => x.Id, y => y.PowerId, (x, y) => new { x.Id, x.Name, x.ParentId, x.Controller, x.Action, x.Icon, x.Sort }).OrderBy(x => x.Sort).ToList();
                var tempFirst = First.Join(tempSecend, x => x.Id, y => y.ParentId, (x, y) => new { x.Id, x.Name, x.Icon, x.Sort }).OrderBy(x => x.Sort).Distinct().ToList();
                foreach (var f in tempFirst)
                {
                    stringBuilder.Append($"<li><a href=\"#\"><i class=\"fa fa-home\"></i><span class=\"nav-label\">{f.Name}</span><span class=\"fa arrow\"></span> </a> <ul class=\"nav nav-second-level\"><li>");
                    foreach (var s in tempSecend)
                    {
                        if (f.Id == s.ParentId)
                            stringBuilder.Append($"<a class=\"J_menuItem\" href=\"/{DefaultData.Manager}/{s.Controller + "/" + s.Action}\">{s.Name}</a>");
                    }
                    stringBuilder.Append("</li> </ul></li> ");
                }


            }
            return stringBuilder.ToString();


        }
    }
}
