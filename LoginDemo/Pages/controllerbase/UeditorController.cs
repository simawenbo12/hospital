using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UEditor.Core;

namespace LoginDemo.Pages.controllerbase
{
    [Route("api/[controller]")]
    public class UeditorController: Controller
    {
        private readonly UEditor.Core.UEditorService _se;

        public UeditorController(UEditorService se)
        {
            _se = se;
        }

        public ContentResult Get()//不写post和get则默认2个都执行同一个方法
        {
            var response = _se.UploadAndGetResponse(HttpContext);
            return Content(response.Result, response.ContentType);
        }

    }
}
