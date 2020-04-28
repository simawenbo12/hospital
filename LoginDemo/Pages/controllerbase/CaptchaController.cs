using CaptchaService;
using CaptchaService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Pages.controllerbase
{
    [IgnoreAntiforgeryToken]
    [Route("api/[controller]")]
    public class CaptchaController:Controller
    {
        private readonly ICaptchaFactory _captchafactory;

        public CaptchaController(ICaptchaFactory captchaFactory)
        {
            _captchafactory = captchaFactory;
        }
        [HttpGet("GetCaptcha")]
        public async Task<IActionResult> GetCaptcha()
        {
            var model = await _captchafactory.CreateAsync();
            Response.Cookies.Append("Captcha", model.Answer);
            return File(model.Image, model.ContentType);
        }
        [HttpPost("verify")]
        public async Task<VerifyResponse> Verify([FromBody] VerifyRequest model)
        {
            var response = await _captchafactory.VerifyAsync(model);
            return response;
        }


    }
}
