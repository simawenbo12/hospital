using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using DataBase.TempModel;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager
{
    public class indexModel : PageModel
    {
  
        public async Task<IActionResult> OnGetLogOut()
        {
            HttpContext.Response.Cookies.Delete("adminid");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Manager/BLogin");
        }

    }
}