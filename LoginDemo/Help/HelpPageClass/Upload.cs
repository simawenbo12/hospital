using DataBase.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    /// <summary>
    /// 上传图片静态类
    /// </summary>
    public static class Upload
    {
        public async static Task UploadPictureAsync(LocalStr localStr,IFormFile imgfile,HmAd hmAd)
        {
            if (imgfile == null) return;
            var fileName = ("wwwroot/uploadimages/"+ DateTime.Now.ToString("MMddHHmmss") +"."+ imgfile.FileName.Split('.')[1]);
            var str = localStr.UrlStr + "/" + fileName;
            hmAd.Image= fileName.Remove(0, 7);
            using (var fileStream = new FileStream(str, FileMode.Create))
            {
                await imgfile.CopyToAsync(fileStream);
            }
            
        }
        public async static Task UploadPictureAsync(LocalStr localStr, IFormFile imgfile, HmArticle hmArticle)
        {
            if (imgfile == null) return;
            var fileName = ("wwwroot/uploadimages/"+ DateTime.Now.ToString("MMddHHmmss") + "." + imgfile.FileName.Split('.')[1]);
            var str = localStr.UrlStr + "/" + fileName;
            hmArticle.Image = fileName.Remove(0, 7);
            using (var fileStream = new FileStream(str, FileMode.Create))
            {
                await imgfile.CopyToAsync(fileStream);
            }
        }
        public async static Task UploadPictureAsync(LocalStr localStr, IFormFile imgfile, HmDoctor hmDoctor)
        {
            if (imgfile == null) return;
            var fileName = ("wwwroot/uploadimages/" + DateTime.Now.ToString("MMddHHmmss") + "." + imgfile.FileName.Split('.')[1]);
            var str = localStr.UrlStr + "/" + fileName;
            hmDoctor.Image = fileName.Remove(0, 7);
            using (var fileStream = new FileStream(str, FileMode.Create))
            {
                await imgfile.CopyToAsync(fileStream);
            }
        }
        public async static Task UploadPictureAsync(LocalStr localStr, IFormFile imgfile, HmSystemfoot hmSystemfoot)
        {
            if (imgfile == null) return;
            var fileName = ("wwwroot/uploadimages/" + DateTime.Now.ToString("MMddHHmmss") + "." + imgfile.FileName.Split('.')[1]);
            var str = localStr.UrlStr + "/" + fileName;
            hmSystemfoot.Logo = fileName.Remove(0, 7);
            using (var fileStream = new FileStream(str, FileMode.Create))
            {
                await imgfile.CopyToAsync(fileStream);
            }
        }

    }
}
