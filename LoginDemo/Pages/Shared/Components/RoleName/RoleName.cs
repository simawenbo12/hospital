using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Pages.Shared.Components.RoleName
{
    public class RoleName: ViewComponent
    {
        public string Name { get; set; } = "请登录";

        public async Task<IViewComponentResult> InvokeAsync()
        {
            AuthenticateResult auth = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (auth.Succeeded)
            {
                Name = auth.Principal.Identity.Name;
            }
            return View("", Name);
        }
    }
}
