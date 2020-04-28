using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager
{
    public class BLoginModel : PageModel
    {
        private readonly jingshenContext _dao;
        private readonly Log _log;
        [BindProperty]
        public HmAdmin HmAdmin { get; set; }
        public BLoginModel(jingshenContext dao, Log ac)
        {
            _log = ac;
            _dao = dao;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var user = _dao.HmAdmin.Find(HmAdmin.Id);
            var userrole = _dao.HmRbacRoleAdmin.ToList().Where(x => x.AdminId == HmAdmin.Id).FirstOrDefault();
            if (userrole!=null && user.Password.Equals(HelpClass.GetMD5(HmAdmin.Password)))
            {
                var claimsIdentity = new ClaimsIdentity(
                     new List<Claim>
                     {
                         new Claim(ClaimTypes.Name,user.Username),
                         new Claim(ClaimTypes.Role,userrole.RoleId.ToString())
                     }
                    , CookieAuthenticationDefaults.AuthenticationScheme);
                 HttpContext.Response.Cookies.Append("adminid",user.Id.ToString());
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                _log.SetLog(HttpContext,$"后台登录", (int)user.Id);
                return RedirectToPage("/Manager/index");
            }
            else
            {
             ViewData["Error"] = "账号或者密码出错";
            return Page();
            }
        }

    }
}